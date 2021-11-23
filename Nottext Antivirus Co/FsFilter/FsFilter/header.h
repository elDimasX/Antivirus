
///
/// Arquivo de cabe�alho global, definiremos tudo aqui
///

#define _NO_CRT_STDIO_INLINE

// Arquivos necess�rios
#include <fltKernel.h>
#include <dontuse.h>
#include <ntstrsafe.h>
#include "nottextfileremovedriver.h"

/*

	C�digo CTL para avisar ao driver que a opera��o pendente no minifiltro
	Possa prosseguir

*/
#define CONTINUE_OPERATION CTL_CODE(FILE_DEVICE_UNKNOWN, 0x2000, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	C�digo CTL para bloquear um arquivo, ele abre um arquivo, mas n�o fecha
	Bloqueando a execu��o/leitura dele

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

	Ativa o status da auto-prote��o

*/
#define ENABLE_SELF_PROTECT CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1000, METHOD_BUFFERED, FILE_ANY_ACCESS)
#define DISABLE_SELF_PROTECT CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1100, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	For�a a exclus�o de um arquivo

*/
#define FORCE_DELETE_FILE CTL_CODE(FILE_DEVICE_UNKNOWN, 0x3200, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	For�a a finaliza��o de um processo

*/
#define FORCE_KILL_PROCESS CTL_CODE(FILE_DEVICE_UNKNOWN, 0x4000, METHOD_BUFFERED, FILE_ANY_ACCESS)

/*

	Arquivo onde o antiv�rus vai escanear novos processos abertos

*/
#define IMAGE_LOADED_FILE L"\\??\\C:\\ProgramData\\Microsoft\\LOADED.txt"

/*

	Saber se o antiv�rus deseja bloquear a execu��o do processo atual

	0 = N�o recebeu a mensagem ainda
	1 = Negue o acesso do processo
	2 = Continue
	3 = Recebendo mensagem, o antiv�rus ainda n�o respondeu

*/
ULONG CONTINUE_OR_DENY_NEW_PROCCESS = 0;

/*

	Informa se devemos proteger ou n�o, arquivos, registros ou processos de serem modificados

*/
BOOLEAN selfProtectValue = TRUE;

/*

	Nome dos dispositivos para conex�o com o antiv�rus

*/
UNICODE_STRING deviceName = RTL_CONSTANT_STRING(L"\\Device\\NottextAntivirus"), SymbolickName = RTL_CONSTANT_STRING(L"\\??\\NottextAntivirus");

/*

	Usado no sistema de arquivos, para saber se deve continuar ou esperar at� que o antiv�rus termine.

*/
BOOLEAN ContinueCurrentOperation = FALSE;

/*

	Dispositivo global, usado para criar o link simbolico e o dispositivo de mensagens

*/
PDEVICE_OBJECT GlobalDevice;

/*

	Verifica se o processo � um processo do Nottext Antivirus

*/
BOOLEAN IsNottextProcess(PEPROCESS Process, BOOLEAN AntiRansomware);

/*

	Esta fun��o � chamada quando uma DLL, SYS ou outros s�o carregados no sistema

*/
PLOAD_IMAGE_NOTIFY_ROUTINE ProcessLoadImageCallback(
	PUNICODE_STRING FullImageName, // O nome completo da imagem
	HANDLE ProcessId, // ID do processo
	PIMAGE_INFO Info // Informa��o, o status e etc
);

/*

	Al�a do arquivo bloqueado, e local do arquivo

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

// Vai conter a string do �ltimo arquivo escaneado para n�o ficar em loop
char LastFileScanned[250];

/*

	Avisa ao antiv�rus que um processo foi impedido de modificar uma pasta
	Protegida

*/
#define ANTI_RANSOMWARE_PROCESS_LOG L"\\??\\C:\\ProgramData\\Microsoft\\BLOCKEDRNS.set"
#define ANTI_RANSOMWARE_FILE_LOG L"\\??\\C:\\ProgramData\\Microsoft\\BLOCKEDFL.set"

/*

	Exe��es para o anti-ransomware

*/
#define ANTI_RANSOMWARE_EXCEPTION_FILE L"\\??\\C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\EXECPTIONRANSO.set"

/*

	Esta fun��o ser� chamada para verificar se o processo pode ou n�o modificar
	Uma pasta protegida

*/
BOOLEAN AntiRansomwareException(
	
	// Nome do processo
	PUNICODE_STRING NameProcess
);

/*

	Quando um novo processo � aberto, o driver suspende a opera��o, e espera
	Uma resposta do antiv�rus, se deseja continuar, ou negar a opera��o, usado em
	Muitos casos em que o driver n�o consegue localizar o arquivo real pelo mini-filtro

*/
VOID NewProcess(

	// Processo
	PEPROCESS Process,

	// PID
	HANDLE pid,

	// Informa��es, como status e outros
	PPS_CREATE_NOTIFY_INFO Info
);

/*

	Fun��o que verifica qual processo est� modificando o registro
	E se for algum permitido, ele permite

*/
BOOLEAN GrantProcessRegistry(

);

/*

	Fun��o que obt�m o caminho de um registro quando o valor que queremos
	Ocorrer, muito importante para sabermos se � uma chave do Nottext Antivirus
	Que queremos proteger

*/
BOOLEAN GetRegistryObjectCompleteName(
	PUNICODE_STRING pRegistryPath,
	PVOID pRegistryObject
);

/*

	A fun��o que protege o registro

*/
NTSTATUS RegistrerCallback(
	PVOID CallbackContext,
	PVOID Argument1, // Argumento 1, sempre tem um valor pelo registro
	PVOID Argument2 // Argumento 2, sempre tem um valor pelo registro
);

/*

	Instala a prote��o de registro, usado em DriverEntry

*/
NTSTATUS InstallRegMonitor(
	PDRIVER_OBJECT DriverObject // Precisamos de informa��es para registrar essa prote��o
);

/*

	Remova a prote��o de registro

*/
VOID UnInstallRegMonitor(

);

/*

	Opera��es para bloquear na auto-prote��o de processos

*/
#define PROCESS_TERMINATE		(0x0001)		// Processo terminado
#define PROCESS_VM_READ			(0x0010)		// L� algumas informa��es
#define PROCESS_VM_WRITE		(0x0020)		// Escreve no processo
#define PROCESS_VM_OPERATION	(0x0008)		// Realiza alguma opera��o no processo
#define PROCESS_SUSPEND_RESUME	(0x0800)		// Suspende ou resume
#define PROCESS_SET_INFORMATION (0x0200)		// Altera alguma informa��o do processo
#define PROCESS_SET_PORT		(0x0800)		// Altera a porta do processo
#define PROCESS_SET_SESSIONID	(0x0004)		// Altera o ID do processo
#define PROCESS_CREATE_PROCESS	(0x0080)		// Um processo foi criado

/*

	Fun��o vai ser chamada antes de um processo for terminado

*/
OB_PREOP_CALLBACK_STATUS BeforeKill(
	PVOID RegistrationContext, // Nosso contexto de registro

	// Informa��es da opera��o, o que a opera��o est� fazendo
	POB_PRE_OPERATION_INFORMATION Information
);

/*

	Depois de um processo for morto

*/
VOID AfterKill(
	PVOID RegistrationContext, // Nosso contexto de registro

	// Informa��es
	POB_POST_OPERATION_INFORMATION Information
);

/*

	Instala a prote��o de processos

*/
NTSTATUS InstallSelfProtect();

/*

	Remove a prote��o de processos

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

	Locais do antiv�rus, usado para proteger contra modifica��es

*/
#define PROGRAM_FILES_X86	"C:\\PROGRAM FILES (X86)\\NOTTEXT\\ANTIVIRUS"
#define PROGRAM_FILES		"C:\\PROGRAM FILES\\NOTTEXT\\ANTIVIRUS"
#define DRIVER_LOCATION		"C:\\WINDOWS\\SYSTEM32\\DRIVERS\\FSFILTER.SYS"

/*

	ProgramData do antiv�rus

*/
#define PROGRAM_DATA "C:\\PROGRAMDATA\\NOTTEXT\\NOTTEXT ANTIVIRUS"

/*

	Saber se a prote��o anti-ransomware est� ou n�o habilitada

*/
#define ANTI_RANSOMWARE_FILE L"\\??\\C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\antiRansomware.set"

/*

	Verifica se um arquivo deve ou n�o ser protegido contra modifica��o

*/
BOOLEAN IsProtectedFile(

	// Nome do arquivo
	PUNICODE_STRING FileNameToProtect
);

/*

	Nome da porta de comunica��o

*/
UNICODE_STRING PortName = RTL_CONSTANT_STRING(L"\\FsFilterport");

/*

	Verifica se o antiv�rus est� conectado � porta

*/
BOOLEAN UserIsConnected(

);

/*

	Verifica se � um arquivo que queremos ou n�o escanear

*/
BOOLEAN CheckException(

	// Nome do arquivo
	PUNICODE_STRING FileToCheck 
);

/*

	Quando o antiv�rus se conecta a porta

*/
NTSTATUS MiniConnect(
	PFLT_PORT clientport, // Cliente
	PVOID serverportcookie, // Cookie
	PVOID Context, // Contexto
	ULONG Size, // Tamanho
	PVOID Connectioncookie // Cookie de conex�o
);

/*

	Desconecta o antiv�rus da porta

*/
VOID MiniDisconnect(
	PVOID connectioncookie // Cookie para desconectar
);

// Vamos usar esse carinha para marcar o nosso tempo de intervalo
LARGE_INTEGER Time;

/*

	Arquivo onde o IRPMJCREATE vai registrar as opera��es E/S
	E o antiv�rus vai ler e escanear os arquivos, deve ser o mesmo no servi�o
	Do antiv�rus

*/
#define FILE_TO_SEND L"\\??\\C:\\ProgramData\\Microsoft\\file.txt"

/*

	Processo do antiv�rus

*/
#define PROCESS_USER "Nottext Antivi"

// Essa fun��o nos serve para pegar o nome de um processo
const char* PsGetProcessImageFileName(PEPROCESS Process);


/*

	BOOLEANO para saber se j� estamos esperando uma resposta do antiv�rus
	Porque se n�o, poderemos continuar as opera��es, pois o antiv�rus
	J� escaneou os arquivos

*/
BOOLEAN IsAlerting = FALSE;

/*

	Filtro global

*/
PFLT_FILTER Filter;

// Opera��es de porta
#include "clientport.h"

/*

	Quando uma opera��o E/S for criada, ela que vai notificar ao
	Antiv�rus e saber se deve continuar ou n�o

*/
FLT_PREOP_CALLBACK_STATUS CreateIrpBefore(
	PFLT_CALLBACK_DATA  Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context // Contexto
);

/*

	Quando uma opera��o E/S for completada, fun��o que n�o n�s serve
	Por enquanto

*/
FLT_POSTOP_CALLBACK_STATUS CreateIrpAfter(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context, // Contexto
	FLT_POST_OPERATION_FLAGS Flags // Bandeira
);

/*

	Quando uma fun��o IRP_MJ_SET_INFORMATION for criada, usado para
	Proteger contra programas como IOBitUnlocker

*/
FLT_PREOP_CALLBACK_STATUS SetInformationBefore(
	PFLT_CALLBACK_DATA  Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context // Contexto
);

/*

	Quando uma fun��o IRP_MJ_SET_INFORMATION for completada

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

	// Usado para saber se devemos substituir todo o cont�udo j� existente
	// No arquivo, ou apenas adicionar
	ACCESS_MASK Mask
);

/*

	Verifica se o arquivo existe ou n�o

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

	Configura as opera��es que queremos registrar

*/
const FLT_OPERATION_REGISTRATION Callbacks[] =
{
	
	{
		// Quando o arquivo � lido, aberto, criado, escrito e etc
		// Usado para auto-prote��o de arquivo e para escanear os arquivos
		IRP_MJ_CREATE,
		0, // N�o precisamos

		CreateIrpBefore, // ANTES de concluir a opera��o
		CreateIrpAfter // DEPOIS de concluir a opera��o
	},

	// Vamos registrar outros eventos para a auto-prote��o de arquivo

	{
		// Quando altera a ACL de um arquivo
		IRP_MJ_SET_SECURITY,
		0, // N�o precisamos

		SetInformationBefore, // ANTES de concluir a opera��o
		SetInformationAfter // DEPOIS de concluir a opera��o
	},

	/*
	{
		// Quando um arquivo for escrito
		IRP_MJ_WRITE,
		0, // N�o precisamos

		SetInformationBefore, // ANTES de concluir a opera��o
		SetInformationAfter // DEPOIS de concluir a opera��o
	},

	*/
	{
		// Quando um arquivo � modificado, usado para impedir que programas como
		// IOBITUnlocker deletem nossos arquivos
		IRP_MJ_SET_INFORMATION,
		0, // N�o precisamos

		SetInformationBefore, // ANTES de concluir a opera��o
		SetInformationAfter // DEPOIS de concluir a opera��o
	},

	{IRP_MJ_OPERATION_END} // Fim

};

/*

	Configura a registra��o para filtrar as opera��es

*/
const FLT_REGISTRATION Registration =
{
	sizeof(FLT_REGISTRATION), // Tamanho do nosso registro
	FLT_REGISTRATION_VERSION, // Nossa vers�o do registro
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
