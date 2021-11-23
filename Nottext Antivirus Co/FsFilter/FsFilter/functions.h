
///
/// Outras fun��es
/// 

// Obt�m a informa��o de um processo
NTSYSCALLAPI
NTSTATUS
NTAPI
ZwQueryInformationProcess(
	__in HANDLE ProcessHandle, // Pid
	__in ULONG ProcessInformationClass, // Informa��o da classe
	__out_bcount_opt(ProcessInformationLength) PVOID ProcessInformation, // Informa��o
	__in ULONG ProcessInformationLength, // Tamanho
	__out_opt PULONG ReturnLength // Retornar o tamanho
);

// Usado para pegar o nome completo de um processo
NTSTATUS
PsReferenceProcessFilePointer(
	IN  PEPROCESS Process, // O processo
	OUT PVOID* OutFileObject // Arquivo
);

/*

	Cria um arquivo indeletavel

*/
NTSTATUS LockFile(
	UNICODE_STRING Location
)
{
	OBJECT_ATTRIBUTES Attrib;
	IO_STATUS_BLOCK IoStatusBlock;

	InitializeObjectAttributes(
		&Attrib,
		&Location,
		OBJ_CASE_INSENSITIVE | OBJ_KERNEL_HANDLE,
		NULL,
		NULL
	);

	// Crie o arquivo, ou abra se j� existir
	NTSTATUS Status = ZwCreateFile(
		&LockHandle,
		FILE_GENERIC_WRITE | FILE_GENERIC_READ,
		&Attrib,
		&IoStatusBlock,
		NULL,
		FILE_ATTRIBUTE_SYSTEM,
		0,
		FILE_OPEN_IF,
		FILE_SYNCHRONOUS_IO_NONALERT,
		NULL,
		0
	);

	// retorne o status
	return Status;
}

/*

	Dorme

*/
VOID KernelSleep()
{
	// Espere
	KeDelayExecutionThread(
		KernelMode, // Kernel
		FALSE, // N�o alert�vel
		&Time // Tempo, definimos ele em DriverEntry
	);
}


/*

	Verifica se um processo pode ou n�o modificar uma pasta protegida

*/
BOOLEAN AntiRansomwareException(

	// Nome do processo
	PUNICODE_STRING NameProcess
)
{
	// N�o tente realizar nenhuma opera��o de arquivo em n�veis de IRQL mais altos.
	// Em vez disso, voc� pode usar um item de trabalho ou um thread de trabalho
	// Do sistema para executar opera��es de arquivo.
	if (KeGetCurrentIrql() != PASSIVE_LEVEL)
	{
		return FALSE; // N�o continuamos
	}

	// ULONG
	ULONG i;

	// Fa�a uma repeti��o at� 200
	for (i = 0; i < 200; i++)
	{
		// Durma
		KernelSleep();

		// Se ele n�o estiver alertando outra fun��o
		if (IsAlerting == FALSE)
		{
			// Pare o loop
			break;
		}
	}

	// Altere o valor porque estamos escrevendo em um arquivo, e previne que
	// Este arquivo seja escrevido outra vez
	IsAlerting = TRUE;

	// Nome do arquivos
	UNICODE_STRING fileName;

	// Atributos
	OBJECT_ATTRIBUTES Attribute;
	NTSTATUS Status;

	// Inicie o UNICODE
	RtlInitUnicodeString(
		&fileName, // Nome

		// Arquivo de exe��es
		ANTI_RANSOMWARE_EXCEPTION_FILE
	);

	// Inicie os atributos
	InitializeObjectAttributes(
		&Attribute, // Salve ele
		&fileName, // Nome do arquivo
		OBJ_CASE_INSENSITIVE | // Ignore mai�scula e min�sculas
		OBJ_KERNEL_HANDLE, // Objeto de kernel
		NULL,
		NULL
	);

	// Al�a do arquivo
	HANDLE fileHandle;

	// Status de bloqueio
	IO_STATUS_BLOCK ioStatusBlock;
		

	// Abra um arquivo
	Status = ZwOpenFile(
		&fileHandle, // Salve a al�a aqui
		GENERIC_READ | // Vamos abrir com permiss�o de leitura
		SYNCHRONIZE,

		&Attribute, // Atributos
		&ioStatusBlock, // Status
		FILE_SHARE_READ, // Permiss�o para ler
		FILE_NON_DIRECTORY_FILE // N�o � uma pasta
	);

	// Se conseguir
	if (NT_SUCCESS(Status))
	{
		__try {


			// Mensagem
			PUNICODE_STRING MensagemBuffer = ExAllocatePoolWithTag(PagedPool, 1024, 'ppNN');

			// Se alocar
			if (MensagemBuffer)
			{
				// Bytes
				LARGE_INTEGER byteOffset;

				// tb
				size_t cb;

				// Configure
				byteOffset.LowPart = byteOffset.HighPart = 0;

				// Leia o arquivo
				Status = ZwReadFile(
					fileHandle, // Al�a

					NULL,
					NULL,
					NULL,

					&ioStatusBlock, // Status

					// Salve o buffer
					MensagemBuffer,


					1024, // Tamanho m�ximo
					&byteOffset, // Os bytes
					NULL
				);

				// Se conseguir ler o arquivo
				if (NT_SUCCESS(Status))
				{
					// Compare
					if (strstr(
						MensagemBuffer, // Se o buffer conter o nome do processo
						NameProcess // Nome do processo que n�s foi passado como argumento
					))
					{
						// Feche o arquivo
						ZwClose(
							fileHandle // Al�a para fechar o arquivo
						);

						// J� leu o arquivo, n�o estamos alertando mais
						IsAlerting = FALSE;

						// Permita o processo
						return TRUE;
					}
				}

				// Libere
				ExFreePoolWithTag(MensagemBuffer, 'ppNN');
			}
		}
		__except (EXCEPTION_EXECUTE_HANDLER)
		{
			// Ocorreu um erro
		}

		// Processo n�o permitido, feche o arquivo
		ZwClose(
			fileHandle // Al�a
		);
	}

	// Terminado, n�o estamos mais escrevendo
	IsAlerting = FALSE;

	// Retorne falso
	return FALSE;
}

/*

	Obtem o local completo do arquivo apartir do processo

*/
PUNICODE_STRING PsGetFullProcessName(
	PEPROCESS Process // Processo
)
{
	__try {


		// Objeto
		PFILE_OBJECT FileObject = NULL;

		// Pega as informa��es do arquivo
		POBJECT_NAME_INFORMATION FileObjectInfo = NULL;

		// Caso n�o consiga pegar o nome completo do processo
		if (!NT_SUCCESS(PsReferenceProcessFilePointer(Process, &FileObject)))
		{
			// NULL
			return NULL;
		}

		// Caso n�o consiga pegar o driver do arquivo, como C://
		if (!NT_SUCCESS(IoQueryFileDosDeviceName(FileObject, &FileObjectInfo)))
		{
			// Pare
			return NULL;
		}

		// Libere o objeto
		ObDereferenceObject(FileObject);


		// Retorne o nome do arquivo
		return &(FileObjectInfo->Name);
	}
	__except (EXCEPTION_EXECUTE_HANDLER) {

	}

	// Nulo
	return NULL;
}

/*

	Verifica se o arquivo deve ser protegido contra modifica��es

*/
BOOLEAN IsProtectedFile(

	// Nome do arquivo
	PUNICODE_STRING FileNameToProtect
)
{
	
	if (
		// Verifique
		strstr(FileNameToProtect, PROGRAM_DATA) ||
		strstr(FileNameToProtect, PROGRAM_FILES) ||
		strstr(FileNameToProtect, PROGRAM_FILES_X86) ||
		strstr(FileNameToProtect, DRIVER_LOCATION)
		)
	{
		// Sim, foi encontrado em alguma v�riavel, este arquivo deve ser protegido
		return TRUE;
	}

	// Falso, n�o deve ser protegido
	return FALSE;
}

/*

	Fun��o que ser� chamada em IRPMJCREATE.H, porque podemos ignorar
	Algumas arquivos em escaneamento para agilizar o processo

*/
BOOLEAN CheckException(

	// Nome do processo
	PUNICODE_STRING FileToCheck
)
{
	if (
		// Arquivo de notifica��o, que o kernel envia ao antiv�rus
		// Para escanear os arquivos
		strstr(FileToCheck, "C:\\PROGRAMDATA\\MICROSOFT\\FILE.TXT") ||

		// Procesos conhecidos
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\CONHOST.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\SCHTASKS.EXE") ||

		// Arquivos que n�o queremos escanear
		strstr(FileToCheck, "C:\\WINDOWS\\EXPLORER.EXE") ||
		strstr(FileToCheck, "C:\\PROGRAMDATA\\MICROSOFT\\WINDOWS\\SQM\\UPLOAD") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\CONSENT.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\WINIT.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\CSRSS.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\DWM.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\WINLOGON.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\SVCHOST.EXE") ||
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\WFS.EXE") ||

		// Se for um arquivo que j� foi escaneado
		!_stricmp(FileToCheck, LastFileScanned) ||

		// Arquivos do antiv�rus
		strstr(FileToCheck, PROGRAM_DATA) ||
		strstr(FileToCheck, PROGRAM_FILES) ||
		strstr(FileToCheck, PROGRAM_FILES_X86) ||

		// DLL Host
		strstr(FileToCheck, "C:\\WINDOWS\\SYSTEM32\\DLLHOST.EXE")
		)
	{
		// Encontrado, n�o continue
		return TRUE;
	}

	// Falso
	return FALSE;
}

/*

	Cria um arquivo para notificar o antiv�rus

*/
VOID SendToUserMode(
	PUNICODE_STRING FileToLog, // Local do arquivo
	PUNICODE_STRING Message, // Mensagem

	// Adicionar ou escrever sobre tudo
	ACCESS_MASK Mask
)
{
	// Verifique o n�vel de IRQ
	if (KeGetCurrentIrql() != PASSIVE_LEVEL)
	{
		// Pare
		return;
	}

	// i
	ULONG i;

	// Repeti��o at� 200
	for (i = 0; i < 200; i++)
	{
		// Durma
		KernelSleep();

		// Se n�o estiver alertando outro evento
		if (IsAlerting == FALSE)
		{
			// Pare
			break;
		}
	}

	// Ok, estamos alertando um evento
	IsAlerting = TRUE;

	// Nome do arquivo
	UNICODE_STRING FileName;

	// Atributos
	OBJECT_ATTRIBUTES Attributes;

	// Inicie o UNICODE
	RtlInitUnicodeString(
		&FileName, // Salve o UNICODE
		FileToLog // Nome do arquivo
	);

	// Inicie os atributos
	InitializeObjectAttributes(
		&Attributes, // Atributos
		&FileName, // Nome do arquivo
		OBJ_KERNEL_HANDLE | // Objeto como kernel
		OBJ_CASE_INSENSITIVE, // Ignore mai�scula e min�sculas
		NULL,
		NULL
	);

	// Al�a do arquivo
	NTSTATUS Status;
	HANDLE FileHandle;

	// IO
	IO_STATUS_BLOCK Block;

	// Abra o arquivo, ou crie se n�o existir
	Status = ZwCreateFile(
		&FileHandle, // Salve a al�a
		Mask | SYNCHRONIZE, // Se � pra susbsituir ou adicionar � algum existente
		&Attributes, // Atriburos

		&Block, // N�o sei pra que serve
		NULL,

		// Atributos normal
		FILE_ATTRIBUTE_NORMAL,
		0,

		// Se ele existir, abra, se n�o, crie
		FILE_OPEN_IF,

		// Todas as opera��es no arquivo s�o de forma s�ncrona. 
		FILE_SYNCHRONOUS_IO_NONALERT | FILE_NON_DIRECTORY_FILE,
		NULL,
		0
	);

	// Se conseguir
	if (NT_SUCCESS(Status))
	{
		// Buffer
		PUNICODE_STRING MensagemBuffer = (PUNICODE_STRING)ExAllocatePoolWithTag(PagedPool, 1024, 'bffw');

		// Se conseguir alocar
		if (MensagemBuffer)
		{
			// Copie para o Buffer
			sprintf(
				MensagemBuffer,
				"%s",
				Message
			);

			// Se obter sucesso
			if (NT_SUCCESS(Status))
			{
				// Escreva no arquivo
				Status = ZwWriteFile(
					FileHandle, // Al�a aberta j�
					NULL,
					NULL,
					NULL,

					&Block, // IO

					// Buffer
					MensagemBuffer,
					strlen(MensagemBuffer),
					NULL,
					NULL
				);
			}

			// Libere
			ExFreePoolWithTag(MensagemBuffer, 'bffw');
		}

		// Terminamos, feche o arquivo
		Status = ZwClose(
			FileHandle
		);
	}

	// Terminamos de alertar, deixe outra opera��o pendente terminar
	IsAlerting = FALSE;
}

/*

	Verifica se um arquvivo existe

*/
BOOLEAN FileExists(

	// Nome do arquivo
	PUNICODE_STRING File
)
{
	// Nome do arquivo
	UNICODE_STRING fileName;

	// Atriburos
	OBJECT_ATTRIBUTES Attribute;
	NTSTATUS Status;

	// Inicie o nome do arquivo em UNICODE
	RtlInitUnicodeString(&fileName, File);

	// Inicie os atributos
	InitializeObjectAttributes(
		&Attribute, // Atributos
		&fileName, // Nome do arquivo
		OBJ_CASE_INSENSITIVE | // Ignore mai�scula e min�sculas
		OBJ_KERNEL_HANDLE, // Objeto de kernel
		NULL,
		NULL
	);

	// AL�A
	HANDLE fileHandle;
	
	// Status
	IO_STATUS_BLOCK ioStatusBlock;

	// IRQL alto
	if (KeGetCurrentIrql() != PASSIVE_LEVEL)
	{
		return STATUS_INVALID_DEVICE_STATE;
	}

	// Abre o arquivo
	Status = ZwOpenFile(
		&fileHandle, // Al�a

		GENERIC_READ | // Vamos abrir com permiss�o de leitura
		SYNCHRONIZE, // Vamos sincronizar

		&Attribute, // Atributos

		// IO
		&ioStatusBlock,
		FILE_SHARE_READ, // Ler
		FILE_NON_DIRECTORY_FILE // N�o � uma pasta
	);

	// Se conseguir abrir o arquivo, significa que ele existe
	if (NT_SUCCESS(Status))
	{
		// Feche o arquivo
		ZwClose(fileHandle);
		return TRUE;
	}

	// N�o conseguiu abrir o arquivo
	return FALSE;
}

/*

	Converte UNICODE para CHAR

*/
BOOLEAN UnicodeStringToChar(
	PUNICODE_STRING OldName, // UNICODE
	char NewName[] // CHAR
)
{
	__try {

		// ANSI
		ANSI_STRING AnsiString;
		NTSTATUS Status;

		// Nome convertido
		char* NameConverted;

		// Inicie de UNICODE para ANSI
		Status = RtlUnicodeStringToAnsiString(
			&AnsiString, // ANSI

			OldName, // UNICODE

			// Aloque um espa�o para ele
			TRUE
		);

		// Se n�o ocorrer falha
		if (NT_SUCCESS(Status) &&

			// Se for menor que 760
			AnsiString.Length < 760
		)
		{
			// Obtenha o CHAR
			NameConverted = (PCHAR)AnsiString.Buffer;

			// Agora, copie a string para o CHAR que n�s foi passado como argumento
			strcpy(

				// Copie para essa string, que n�s foi passado
				NewName, 

				// Essa fun��o serve para deixar todas as letras em mai�sculas
				_strupr(

					// Copie o nome convertido
					NameConverted 
				)
			);
		}

		// Se ocorrer um erro
		else {
			return FALSE;
		}

		// Libere o ANSI_STRING alocado
		RtlFreeAnsiString(
			&AnsiString
		);
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		// Retorne FALSE
		return FALSE;
	}

	// SUCESSO!
	return TRUE;
}

/*

	Verifica se o antiv�rus est� conectado � porta (ou online)

*/
BOOLEAN UserIsConnected(

)
{
	// Se for diferente de nulo
	if (ClientPort != NULL)
	{
		// Conectado
		return TRUE;
	}

	// N�o conectado
	return FALSE;
}

