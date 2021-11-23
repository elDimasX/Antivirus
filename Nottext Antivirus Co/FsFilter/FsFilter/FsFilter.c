///
/// Driver de kernel para o Nottext Antivirus
/// Vou reescrever este código
///

/*

	Arquivos necessários para obter as definições e etc

*/

#include "header.h"
#include "functions.h"
#include "selfProtect.h"
#include "reg.h"

#include "irpmjcreate.h"
#include "irpmjsetinformation.h"

#include "process.h"

#include "irps.h"

/*

	Quando um novo processo é aberto, usado para garantir que um processo é seguro
	Para ser aberto

*/
VOID NewProcess(
	// Processo
	PEPROCESS Process,

	// PID
	HANDLE pid,

	// Informações, como status e outros
	PPS_CREATE_NOTIFY_INFO Info
)
{
	// Novo processo aberto
	if (Info && Info->FileOpenNameAvailable == TRUE)
	{
		// Se o antivírus não estiver ligado
		if (UserIsConnected() == FALSE)
		{
			// Apenas, crie o processo
			Info->CreationStatus = STATUS_SUCCESS;

			// Pare
			return;
		}

		// Se estiver no valor 3, significa que ainda está recebendo a mensagem
		if (CONTINUE_OR_DENY_NEW_PROCCESS == 3)
		{
			// Espere 400 vezes (40 segundos)
			for (int i = 0; i < 400; i++)
			{
				// Durma
				KernelSleep();

				// Se o antivírus recebeu a mensagem, ou foi desconectado
				if (CONTINUE_OR_DENY_NEW_PROCCESS != 3 || UserIsConnected() == FALSE)
				{
					// Pare o LOOP
					break;
				}
			}
		}

		// Nome do processo
		CHAR ProcessName[300] = { 0 };

		if (
			// Se não conseguir converter pra CHAR
			!UnicodeStringToChar(PsGetFullProcessName(Process), ProcessName)
			)
		{
			// Continue o processo
			Info->CreationStatus = STATUS_SUCCESS;

			// Pare
			return;
		}

		// Se for um processo do qual não queremos rastrear
		if (CheckException(ProcessName) == TRUE)
		{
			// Continue o processo
			Info->CreationStatus = STATUS_SUCCESS;

			// Pare
			return;
		}

		// Adicione uma quebra de linha no arquivo
		strcat(
			ProcessName,
			"\r\n"
		);

		// Altere o valor, pra 3, que é "O antivírus não nós respondeu ainda"
		CONTINUE_OR_DENY_NEW_PROCCESS = 3;

		// Conseguiu converter pra CHAR, envie ao user-mode
		SendToUserMode(
			// Image
			IMAGE_LOADED_FILE,

			// Nome do processo
			ProcessName,

			// Substituia tudo
			FILE_APPEND_DATA
		);

		// Espere 100 vezes (100 segundos)
		for (int i = 0; i < 100; i++)
		{
			// Durma
			KernelSleep();

			// Se for pra negar o processo
			if (CONTINUE_OR_DENY_NEW_PROCCESS == 1)
			{
				// Negue o processo
				Info->CreationStatus = STATUS_ACCESS_DENIED;
				break;
			}

			// Se for pra continuar o processo, ou se o antivírus for desconectado
			else if (
				CONTINUE_OR_DENY_NEW_PROCCESS == 2 ||
				UserIsConnected() == FALSE
			)
			{
				// Continue o processo
				Info->CreationStatus = STATUS_SUCCESS;
				break;
			}

		}

		// Resete o valor
		CONTINUE_OR_DENY_NEW_PROCCESS = 0;
	}
}

/*

	Verifica se o processo é um processo do Nottext Antivirus ou de
	uma exeção do anti-ransomware

*/
BOOLEAN IsNottextProcess(PEPROCESS Process, BOOLEAN AntiRansomware)
{
	// Nome completo do processo
	ANSI_STRING ProcessName;

	// Valor para retornar
	BOOLEAN returnValue = FALSE;
	NTSTATUS Status;

	// Tente
	__try {

		// Converta o nome completo do processo
		Status = RtlUnicodeStringToAnsiString(

			// Guarde o nome aqui
			&ProcessName,

			// Nome do processo
			(UNICODE_STRING*)PsGetFullProcessName(Process),

			// Aloque um espaço na memória
			TRUE
		);

		// Se não conseguir alocar o nome do processo
		if (!NT_SUCCESS(Status))
		{
			// Não foi alocado, não continue
			return FALSE;
		}

		// Verifique qual processo criou a requisição
		if (

			// Se for algum processo do antivírus, deixe-o em paz
			strstr(_strupr(ProcessName.Buffer), PROGRAM_FILES_X86) ||
			strstr(_strupr(ProcessName.Buffer), PROGRAM_FILES) ||

			// Process critíco do sistema
			strstr(_strupr(ProcessName.Buffer), "C:\\WINDOWS\\SYSTEM32\\SVCHOST.EXE"
		))
		{
			// Altere o valor para retornar
			returnValue = TRUE;
		}

		// Se não for um processo dos arquivos e programas
		else {

			// Se é pra verificar a proteção do anti-ransomware
			if (AntiRansomware == TRUE)
			{
				// Se conter uma exeção no anti-ransomware
				if (AntiRansomwareException(_strupr(ProcessName.Buffer)))
				{
					// Altere pra TRUE, é uma exeção
					returnValue = TRUE;
				}
			}
		}

		// Libere o valor alocado
		RtlFreeAnsiString(&ProcessName);
	}

	// Se ocorrer qualquer erro
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
	}

	// Falhou, pare
	return returnValue;
}

/*

	Função de descarregamento do driver

*/
VOID MiniUnload(
	FLT_FILTER_UNLOAD_FLAGS UnloadFlags // Nossa bandeira para o descarregamento
)
{

	/*
	// Alertando, falso, no Windows 7, essa desgraça de OS bugado
	IsAlerting = FALSE;

	// Vamos remover a proteção de imagens
	PsRemoveLoadImageNotifyRoutine(
		ProcessLoadImageCallback
	);
	*/

	// Se o arquivo indeletavel ainda estiver aberto
	if (LockHandle != NULL)
	{
		ZwClose(LockHandle);
	}

	// Feche a porta de comunicação
	FltCloseCommunicationPort(

		// Usamos ele para abrir uma porta
		// E vamos usar para fecha-lo
		Port

	);

	// Agora, altere o cliente pra NULL, pra que as operações pendentes terminem
	ClientPort = NULL;

	// Remova as notificações
	NTSTATUS RemoveProcessNotify = PsSetCreateProcessNotifyRoutineEx(NewProcess, TRUE);

	// Delete o dispositivo
	IoDeleteDevice(GlobalDevice);

	// Delete o link simbolico
	IoDeleteSymbolicLink(&SymbolickName);

	// Remova a auto-proteção de processo
	UnInstallSelfProtect();

	// Remova a proteção de registro
	UnInstallRegMonitor();

	// Vamos remover o nosso filtro
	FltUnregisterFilter(
		Filter // Filtro usado para registrar
	);

	// Retorne um status de sucesso
	return STATUS_SUCCESS;
}

/*

	Função que carrega o nosso driver

*/
NTSTATUS DriverEntry(
	PDRIVER_OBJECT DriverObject,
	PUNICODE_STRING RegistryPath
)
{
	// Inicie um valor em LastFileScanned
	strcpy(LastFileScanned, "C:\\e:Thisisaexemple");

	// Vamos iniciar o tempo de intervalo
	//Time.QuadPart = -10 * 1000 * 1000; // 1 Segundos
	Time.QuadPart = -10 * 1000 * 100; // 100 milisegundos

	NTSTATUS Status;

	// Tente criar um arquivo indeletavel para bloquear a pasta de ser deletada
	Status = LockFile(LockLocationX86);

	if (!NT_SUCCESS(Status))
	{
		// Bloqueie em outro local
		Status = LockFile(LockLocationX64);
	}

	// Registre o filtro para capturar as operações de arquivos
	Status = FltRegisterFilter(
		DriverObject, // DriverObject
		&Registration, // Configurações para o registro
		&Filter // Salve uma sessão aqui
	);

	// Se conseguir registrar
	if (NT_SUCCESS(Status))
	{
		// Inicie a filtrar as operações
		Status = FltStartFiltering(
			Filter // Usamos ele para registrar o filtro
		);

		// Se falhar ao iniciar o filtro
		if (!NT_SUCCESS(Status))
		{
			// Saia
			goto exit;
		}

		// PSECURITY_DESCRIPTOR
		PSECURITY_DESCRIPTOR Sd;

		// Atributos
		OBJECT_ATTRIBUTES Attributes;

		// Cria um descritor de segurança padrão para uso com FltCreateCommunicationPort
		Status = FltBuildDefaultSecurityDescriptor(
			&Sd, // Vamos salvar essa configurações aqui
			FLT_PORT_ALL_ACCESS // Acesso total á porta de comunicação
		);

		// Se conseguir
		if (NT_SUCCESS(Status))
		{
			// Inicie os atributos
			InitializeObjectAttributes(
				&Attributes, // Salve os atributos aqui
				&PortName, // Nome da nossa porta de comunicação
				OBJ_KERNEL_HANDLE | // Objeto de kernel
				OBJ_CASE_INSENSITIVE, // Ignore maiúscula e minúsculas
				NULL,
				Sd // Descritor de segurança
			);

			// Crie a porta de comunicação
			Status = FltCreateCommunicationPort(
				Filter, // Vamos usar este filtro para criar a porta de comunicação
				&Port, // Vamos salvar a porta aqui, para remove-la depois

				// Os atributos, onde usamos o nome da porta e descritor
				// De segurança
				&Attributes,

				NULL,
				MiniConnect, // Após o antivírus se conectar á porta
				MiniDisconnect, // Após o processo do antivírus sair
				NULL, // Após o antivírus enviar uma mensagem, mas não queremos isso
				1 // A gente vai permitir no máximo 1 conexão por vez
			);

			// Terminamos com o descritor de segurança, libere-o
			FltFreeSecurityDescriptor(
				Sd
			);

			// Verifica se ocorreu algum erro
			if (!NT_SUCCESS(Status))
			{
				// Feche a porta de comunicação
				FltCloseCommunicationPort(
					Port
				);

				// Remova o nosso filtro
				FltUnregisterFilter(
					Filter
				);

				// Não podemos continuar
				return Status;
			}

			// Agora, instale a proteção de processos, para impedir que o antivírus
			// Seja finalizado
			InstallSelfProtect();

			// Instale a proteção de registro, para impedir malwares de modificarem
			// Os valores no registro
			InstallRegMonitor(
				DriverObject
			);
		}
	}
	
	/*
	// Configure pra receber notificações quando uma imagem é carregada no sistema
	// Por exemplo, quando uma DLL é carregada

	******************************************
	* 
	* Eu removi esta função porque o Windows7 não suporta PsRemoveImageNotifyRoutine
	* E o driver não pode ser descarregado....
	* 
	* ****************************************

	Status = PsSetLoadImageNotifyRoutine(
		ProcessLoadImageCallback
	);
	*/

	// Se falhar
	if (!NT_SUCCESS(Status))
	{
		// Saia
		goto exit;
	}

	// Crie o dispositivo para as comunicações
	Status = IoCreateDevice(
		DriverObject, // DriverObject
		0,
		&deviceName, // Nome do dispositivo
		FILE_DEVICE_UNKNOWN,
		NULL,
		TRUE, // Somente uma conexão por vez
		&GlobalDevice // Salve a sessão aqui
	);

	// Se não conseguir
	if (!NT_SUCCESS(Status))
	{
		// Função de sair
		goto exit;
	}

	// Crie o link simbolico
	Status = IoCreateSymbolicLink(&SymbolickName, &deviceName);

	// Se falhar
	if (!NT_SUCCESS(Status))
	{
		// Saia
		goto exit;
	}

	// Configure para receber mensagens
	DriverObject->MajorFunction[IRP_MJ_CREATE] = Create;
	DriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = Control;
	DriverObject->MajorFunction[IRP_MJ_CLOSE] = Close;

	// Obtenha os novos processos abertos
	NTSTATUS CreateProcessNotify = PsSetCreateProcessNotifyRoutineEx(NewProcess, FALSE);

	// Retorne o status
	return Status;

// Funçaõ de sair
exit:
	
	// Se a porta já estiver sido criada
	if (Port != NULL)
	{
		// Feche a porta de comunicação
		FltCloseCommunicationPort(
			Port
		);
	}

	// Se o filtro não estiver NULO
	if (Filter != NULL)
	{
		// Remova o filtro
		FltUnregisterFilter(
			Filter
		);
	}

	// Se o GlobalDevice não for NULL
	if (GlobalDevice != NULL)
	{
		// Delete o dispositivo
		IoDeleteDevice(GlobalDevice);
	}

	// Retorne um status de erro
	return Status;
}

/*

	Esta função é chamada quando uma DLL, SYS ou outros são carregados no sistema

*/
PLOAD_IMAGE_NOTIFY_ROUTINE ProcessLoadImageCallback(
	PUNICODE_STRING FullImageName, // O nome completo da imagem
	HANDLE ProcessId, // ID do processo
	PIMAGE_INFO Info // Informação, o status e etc
)
{
	// Se obter as informações
	if (Info)
	{
		// Se o antivírus não estiver conectado ao driver
		if (UserIsConnected() == FALSE)
		{
			// Pare
			return STATUS_SUCCESS;
		}

		/*
		PFILE_OBJECT pFileObject = NULL;
		pFileObject = CONTAINING_RECORD(FullImageName, FILE_OBJECT, FileName);
		*/

		//DbgPrint("%wZ", &pFileObject->FileName);

		// Nome da imagem
		PVOID ImageName;

		// Vamos configurar o máximo de caracteres que pode ser copiado
		// Para o PVOID
		ULONG lengthVolume = max(
			1024, // 1024
			Info->ImageSize // Tamanho
		);

		// Aloque o PVOID
		ImageName = ExAllocatePoolWithTag(
			NonPagedPool, // Não pagado
			lengthVolume, // Tamanho máximo que definimos
			'nacS' // Pode ser qualquer coisa
		);

		// Verifique se foi alocado
		if (ImageName == NULL)
		{
			// Não alocado
			return STATUS_SUCCESS;
		}

		// Converta o UNICODE para CHAR
		if (UnicodeStringToChar(
			FullImageName, // Nome da DLL
			ImageName //´Para esse var
		))
		{
			if (
				// Se não for nenhuma destas imagens
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\SENDMAIL.DLL") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\MYDOCS.DLL") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\AUDIODEV.DLL") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\WPDSHEXT.DLL") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\WMASF.DLL") &&
				!strstr(ImageName, "\\WINDOWS\\SYSTEM32\\WMVCORE.DLL") &&
				!strstr(ImageName, "\\SYSTEMROOT\\") &&

				// Se não for um arquivo que não queremos escanear
				!CheckException(ImageName)
			)
			{
				// Adicione uma linha
				strcat(ImageName, "\r\n");

				// Envie ao antivírus
				SendToUserMode(
					// LOG de dlls
					IMAGE_LOADED_FILE,

					// Nome da imagem
					ImageName,

					// Adicione sem remover
					FILE_APPEND_DATA
				);
			}
		}

		// Terminado, libere o PVOID alocado
		ExFreePoolWithTag(
			ImageName,
			'nacS' // Mesmo valor que anteriormente
		);
	}

	// Sucesso
	return STATUS_SUCCESS;
}

