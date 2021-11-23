
///
/// Arquivo de cabeçalho global, definiremos tudo aqui
///

#define _NO_CRT_STDIO_INLINE

// Arquivos necessários
#include <fltKernel.h>
#include <dontuse.h>
#include <ntstrsafe.h>
#include "nottextfileremovedriver.h"

/*

	Código CTL para avisar ao driver que a operação pendente no minifiltro
	Possa prosseguir

*/
#define CONTINUE_OPERATION CTL_CODE(FILE_DEVICE_UNKNOWN, 0x2000, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Código CTL para bloquear um arquivo, ele abre um arquivo, mas não fecha
	Bloqueando a execução/leitura dele

*/
#define LOCK_FILE CTL_CODE(FILE_DEVICE_UNKNOWN, 0x2100, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Continue o processo atual pendente

*/
#define CONTINUE_PROCESS CTL_CODE(FILE_DEVICE_UNKNOWN, 0x3000, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Negue o processo atual pendente

*/
#define DENY_PROCESS CTL_CODE(FILE_DEVICE_UNKNOWN, 0x3100, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Ativa o status da auto-proteção

*/
#define ENABLE_SELF_PROTECT CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1000, METHOD_BUFFERED, FILE_ANY_ACCESS)
#define DISABLE_SELF_PROTECT CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1100, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Força a exclusão de um arquivo

*/
#define FORCE_DELETE_FILE CTL_CODE(FILE_DEVICE_UNKNOWN, 0x3200, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Força a finalização de um processo

*/
#define FORCE_KILL_PROCESS CTL_CODE(FILE_DEVICE_UNKNOWN, 0x4000, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Arquivo onde o antivírus vai escanear novos processos abertos

*/
#define IMAGE_LOADED_FILE L"\\??\\C:\\ProgramData\\Microsoft\\LOADED.txt"

/*

	Saber se o antivírus deseja bloquear a execução do processo atual

	0 = Não recebeu a mensagem ainda
	1 = Negue o acesso do processo
	2 = Continue
	3 = Recebendo mensagem, o antivírus ainda não respondeu

*/
ULONG CONTINUE_OR_DENY_NEW_PROCCESS = 0;

/*

	Informa se devemos proteger ou não, arquivos, registros ou processos de serem modificados

*/
BOOLEAN selfProtectValue = TRUE;

/*

	Nome dos dispositivos para conexão com o antivírus

*/
UNICODE_STRING deviceName = RTL_CONSTANT_STRING(L"\\Device\\NottextAntivirus"), SymbolickName = RTL_CONSTANT_STRING(L"\\??\\NottextAntivirus");

/*

	Usado no sistema de arquivos, para saber se deve continuar ou esperar até que o antivírus termine.

*/
BOOLEAN ContinueCurrentOperation = FALSE;

/*

	Dispositivo global, usado para criar o link simbolico e o dispositivo de mensagens

*/
PDEVICE_OBJECT GlobalDevice;

/*

	Verifica se o processo é um processo do Nottext Antivirus

*/
BOOLEAN IsNottextProcess(PEPROCESS Process, BOOLEAN AntiRansomware);

/*

	Esta função é chamada quando uma DLL, SYS ou outros são carregados no sistema

*/
PLOAD_IMAGE_NOTIFY_ROUTINE ProcessLoadImageCallback(
	PUNICODE_STRING FullImageName, // O nome completo da imagem
	HANDLE ProcessId, // ID do processo
	PIMAGE_INFO Info // Informação, o status e etc
);

/*

	Alça do arquivo bloqueado, e local do arquivo

*/
HANDLE LockHandle;

UNICODE_STRING LockLocationX86 = RTL_CONSTANT_STRING(L"\\??\\C:\\PROGRAM FILES (X86)\\NOTTEXT\\ANTIVIRUS\\system.lock");
UNICODE_STRING LockLocationX64 = RTL_CONSTANT_STRING(L"\\??\\C:\\PROGRAM FILES\\NOTTEXT\\ANTIVIRUS\\system.lock");

/*

	Cria um arquivo totalmente bloqueado para impedir de modificarem ele

*/
NTSTATUS LockFile(
	UNICODE_STRING Location
);

// Vai conter a string do último arquivo escaneado para não ficar em loop
char LastFileScanned[250];

/*

	Avisa ao antivírus que um processo foi impedido de modificar uma pasta
	Protegida

*/
#define ANTI_RANSOMWARE_PROCESS_LOG L"\\??\\C:\\ProgramData\\Microsoft\\BLOCKEDRNS.set"
#define ANTI_RANSOMWARE_FILE_LOG L"\\??\\C:\\ProgramData\\Microsoft\\BLOCKEDFL.set"

/*

	Exeções para o anti-ransomware

*/
#define ANTI_RANSOMWARE_EXCEPTION_FILE L"\\??\\C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\EXECPTIONRANSO.set"

/*

	Esta função será chamada para verificar se o processo pode ou não modificar
	Uma pasta protegida

*/
BOOLEAN AntiRansomwareException(
	
	// Nome do processo
	PUNICODE_STRING NameProcess
);

/*

	Quando um novo processo é aberto, o driver suspende a operação, e espera
	Uma resposta do antivírus, se deseja continuar, ou negar a operação, usado em
	Muitos casos em que o driver não consegue localizar o arquivo real pelo mini-filtro

*/
VOID NewProcess(

	// Processo
	PEPROCESS Process,

	// PID
	HANDLE pid,

	// Informações, como status e outros
	PPS_CREATE_NOTIFY_INFO Info
);

/*

	Função que verifica qual processo está modificando o registro
	E se for algum permitido, ele permite

*/
BOOLEAN GrantProcessRegistry(

);

/*

	Função que obtém o caminho de um registro quando o valor que queremos
	Ocorrer, muito importante para sabermos se é uma chave do Nottext Antivirus
	Que queremos proteger

*/
BOOLEAN GetRegistryObjectCompleteName(
	PUNICODE_STRING pRegistryPath,
	PVOID pRegistryObject
);

/*

	A função que protege o registro

*/
NTSTATUS RegistrerCallback(
	PVOID CallbackContext,
	PVOID Argument1, // Argumento 1, sempre tem um valor pelo registro
	PVOID Argument2 // Argumento 2, sempre tem um valor pelo registro
);

/*

	Instala a proteção de registro, usado em DriverEntry

*/
NTSTATUS InstallRegMonitor(
	PDRIVER_OBJECT DriverObject // Precisamos de informações para registrar essa proteção
);

/*

	Remova a proteção de registro

*/
VOID UnInstallRegMonitor(

);

/*

	Operações para bloquear na auto-proteção de processos

*/
#define PROCESS_TERMINATE		(0x0001)		// Processo terminado
#define PROCESS_VM_READ			(0x0010)		// Lê algumas informações
#define PROCESS_VM_WRITE		(0x0020)		// Escreve no processo
#define PROCESS_VM_OPERATION	(0x0008)		// Realiza alguma operação no processo
#define PROCESS_SUSPEND_RESUME	(0x0800)		// Suspende ou resume
#define PROCESS_SET_INFORMATION (0x0200)		// Altera alguma informação do processo
#define PROCESS_SET_PORT		(0x0800)		// Altera a porta do processo
#define PROCESS_SET_SESSIONID	(0x0004)		// Altera o ID do processo
#define PROCESS_CREATE_PROCESS	(0x0080)		// Um processo foi criado

/*

	Função vai ser chamada antes de um processo for terminado

*/
OB_PREOP_CALLBACK_STATUS BeforeKill(
	PVOID RegistrationContext, // Nosso contexto de registro

	// Informações da operação, o que a operação está fazendo
	POB_PRE_OPERATION_INFORMATION Information
);

/*

	Depois de um processo for morto

*/
VOID AfterKill(
	PVOID RegistrationContext, // Nosso contexto de registro

	// Informações
	POB_POST_OPERATION_INFORMATION Information
);

/*

	Instala a proteção de processos

*/
NTSTATUS InstallSelfProtect();

/*

	Remove a proteção de processos

*/
VOID UnInstallSelfProtect();

/*

	Extensoes para serem escaneadas

*/
static UNICODE_STRING exeExtension = RTL_CONSTANT_STRING(L"EXE");
static UNICODE_STRING sysExtension = RTL_CONSTANT_STRING(L"SYS");
static UNICODE_STRING dllExtension = RTL_CONSTANT_STRING(L"DLL");
static UNICODE_STRING batExtension = RTL_CONSTANT_STRING(L"BAT");

/*

	Pega o nome completo do arquivo apartir do processo

*/
PUNICODE_STRING PsGetFullProcessName(
	PEPROCESS Process // Processo
);

/*

	Locais do antivírus, usado para proteger contra modificações

*/
#define PROGRAM_FILES_X86	"C:\\PROGRAM FILES (X86)\\NOTTEXT\\ANTIVIRUS"
#define PROGRAM_FILES		"C:\\PROGRAM FILES\\NOTTEXT\\ANTIVIRUS"
#define DRIVER_LOCATION		"C:\\WINDOWS\\SYSTEM32\\DRIVERS\\FSFILTER.SYS"

/*

	ProgramData do antivírus

*/
#define PROGRAM_DATA "C:\\PROGRAMDATA\\NOTTEXT\\NOTTEXT ANTIVIRUS"

/*

	Saber se a proteção anti-ransomware está ou não habilitada

*/
#define ANTI_RANSOMWARE_FILE L"\\??\\C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\antiRansomware.set"

/*

	Verifica se um arquivo deve ou não ser protegido contra modificação

*/
BOOLEAN IsProtectedFile(

	// Nome do arquivo
	PUNICODE_STRING FileNameToProtect
);

/*

	Nome da porta de comunicação

*/
UNICODE_STRING PortName = RTL_CONSTANT_STRING(L"\\FsFilterport");

/*

	Verifica se o antivírus está conectado á porta

*/
BOOLEAN UserIsConnected(

);

/*

	Verifica se é um arquivo que queremos ou não escanear

*/
BOOLEAN CheckException(

	// Nome do arquivo
	PUNICODE_STRING FileToCheck 
);

/*

	Quando o antivírus se conecta a porta

*/
NTSTATUS MiniConnect(
	PFLT_PORT clientport, // Cliente
	PVOID serverportcookie, // Cookie
	PVOID Context, // Contexto
	ULONG Size, // Tamanho
	PVOID Connectioncookie // Cookie de conexão
);

/*

	Desconecta o antivírus da porta

*/
VOID MiniDisconnect(
	PVOID connectioncookie // Cookie para desconectar
);

// Vamos usar esse carinha para marcar o nosso tempo de intervalo
LARGE_INTEGER Time;

/*

	Arquivo onde o IRPMJCREATE vai registrar as operações E/S
	E o antivírus vai ler e escanear os arquivos, deve ser o mesmo no serviço
	Do antivírus

*/
#define FILE_TO_SEND L"\\??\\C:\\ProgramData\\Microsoft\\file.txt"

/*

	Processo do antivírus

*/
#define PROCESS_USER "Nottext Antivi"

// Essa função nos serve para pegar o nome de um processo
const char* PsGetProcessImageFileName(PEPROCESS Process);


/*

	BOOLEANO para saber se já estamos esperando uma resposta do antivírus
	Porque se não, poderemos continuar as operações, pois o antivírus
	Já escaneou os arquivos

*/
BOOLEAN IsAlerting = FALSE;

/*

	Filtro global

*/
PFLT_FILTER Filter;

// Operações de porta
#include "clientport.h"

/*

	Quando uma operação E/S for criada, ela que vai notificar ao
	Antivírus e saber se deve continuar ou não

*/
FLT_PREOP_CALLBACK_STATUS CreateIrpBefore(
	PFLT_CALLBACK_DATA  Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context // Contexto
);

/*

	Quando uma operação E/S for completada, função que não nós serve
	Por enquanto

*/
FLT_POSTOP_CALLBACK_STATUS CreateIrpAfter(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context, // Contexto
	FLT_POST_OPERATION_FLAGS Flags // Bandeira
);

/*

	Quando uma função IRP_MJ_SET_INFORMATION for criada, usado para
	Proteger contra programas como IOBitUnlocker

*/
FLT_PREOP_CALLBACK_STATUS SetInformationBefore(
	PFLT_CALLBACK_DATA  Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context // Contexto
);

/*

	Quando uma função IRP_MJ_SET_INFORMATION for completada

*/
FLT_POSTOP_CALLBACK_STATUS SetInformationAfter(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context, // Contexto
	FLT_POST_OPERATION_FLAGS Flags // Bandeira
);

/*

	Quando o driver for descarregado

*/
VOID MiniUnload(
	FLT_FILTER_UNLOAD_FLAGS UnloadFlags // Bandeira
);

/*

	Envia algo para o user-mode

*/
VOID SendToUserMode(
	PUNICODE_STRING FileToLog, // Arquivo para escrever
	PUNICODE_STRING Message, // Mensagem para ser escrita no arquivo

	// Usado para saber se devemos substituir todo o contéudo já existente
	// No arquivo, ou apenas adicionar
	ACCESS_MASK Mask
);

/*

	Verifica se o arquivo existe ou não

*/
BOOLEAN FileExists(
	PUNICODE_STRING File // Nome do arquivo
);

/*

	Converte de UNICODE para CHAR, usado para enviar ao user-mode

*/
BOOLEAN UnicodeStringToChar(
	PUNICODE_STRING OldName, // O nome antigo, ou seja, o UNICODE
	char NewName[] // Local onde devemos copiar o novo CHAR
);

/*

	Configura as operações que queremos registrar

*/
const FLT_OPERATION_REGISTRATION Callbacks[] =
{
	
	{
		// Quando o arquivo é lido, aberto, criado, escrito e etc
		// Usado para auto-proteção de arquivo e para escanear os arquivos
		IRP_MJ_CREATE,
		0, // Não precisamos

		CreateIrpBefore, // ANTES de concluir a operação
		CreateIrpAfter // DEPOIS de concluir a operação
	},

	// Vamos registrar outros eventos para a auto-proteção de arquivo

	{
		// Quando altera a ACL de um arquivo
		IRP_MJ_SET_SECURITY,
		0, // Não precisamos

		SetInformationBefore, // ANTES de concluir a operação
		SetInformationAfter // DEPOIS de concluir a operação
	},

	/*
	{
		// Quando um arquivo for escrito
		IRP_MJ_WRITE,
		0, // Não precisamos

		SetInformationBefore, // ANTES de concluir a operação
		SetInformationAfter // DEPOIS de concluir a operação
	},

	*/
	{
		// Quando um arquivo é modificado, usado para impedir que programas como
		// IOBITUnlocker deletem nossos arquivos
		IRP_MJ_SET_INFORMATION,
		0, // Não precisamos

		SetInformationBefore, // ANTES de concluir a operação
		SetInformationAfter // DEPOIS de concluir a operação
	},

	{IRP_MJ_OPERATION_END} // Fim

};

/*

	Configura a registração para filtrar as operações

*/
const FLT_REGISTRATION Registration =
{
	sizeof(FLT_REGISTRATION), // Tamanho do nosso registro
	FLT_REGISTRATION_VERSION, // Nossa versão do registro
	0,
	NULL,
	Callbacks,	// Chamadas
	MiniUnload, // Permite que o driver seja descarregado
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL
};

/*

	Inicia o driver

*/
NTSTATUS DriverEntry(
	PDRIVER_OBJECT DriverObject,
	PUNICODE_STRING RegistryPath
);

/*

	Dorme

*/
VOID KernelSleep(

);
