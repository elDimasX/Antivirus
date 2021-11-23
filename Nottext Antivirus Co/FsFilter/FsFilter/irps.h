///
/// Arquivos de IRP, quando o antiv�rus envia uma mensagem
///

/*

	Quando uma nova requisi��o � criada

*/
NTSTATUS Create(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;

	// Complete a requisi��o
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}


/*

	Quando � enviada uma mensagem do antiv�rus

*/
NTSTATUS Control(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// PIO
	PIO_STACK_LOCATION Io = IoGetCurrentIrpStackLocation(Irp);

	// Mensagem para retornar ao user-mode
	CHAR* StatusString = "fail!";

	// Mensagem do antiv�rus
	PCHAR antivirusMessage = (PCHAR)Irp->AssociatedIrp.SystemBuffer;

	// Inform��o pra retornar ao antiv�rus, necess�rio aumentar o valor caso
	// Queira enviar uma mensagem com mais de 10 caracterias
	Irp->IoStatus.Information = 10;

	// S� execute comando se o antiv�rus estiver conectado � porta
	if (UserIsConnected() == TRUE)
	{
		// Se o antiv�rus desejar continuar a opera��o pendente
		if (Io->Parameters.DeviceIoControl.IoControlCode == CONTINUE_OPERATION)
		{
			// Altere o valor
			ContinueCurrentOperation = TRUE;
			StatusString = "success!";
		}

		// Se o antiv�rus desejar for�ar a exclus�o de um arquivo
		else if (Io->Parameters.DeviceIoControl.IoControlCode == FORCE_DELETE_FILE)
		{
			if (NT_SUCCESS(ForceDeleteFile(antivirusMessage)))
			{
				StatusString = "success!";
			}
		}

		// Se o antiv�rus desejar bloquear o arquivo
		else if (Io->Parameters.DeviceIoControl.IoControlCode == LOCK_FILE)
		{
			// ANSI e UNICODE
			ANSI_STRING Ansi;
			UNICODE_STRING Unicode;

			// Inicie o ANSI com a mensagem do antiv�rus
			RtlInitAnsiString(&Ansi, antivirusMessage);

			// Inicie o UNICODE
			RtlAnsiStringToUnicodeString(
				// Unicode
				&Unicode,

				// Ansi
				&Ansi,
				
				// Aloque um espa�o na mem�ria
				TRUE
			);

			// Atributos
			OBJECT_ATTRIBUTES Attrib;

			// Inicie os atributos para detectar o arquivo
			InitializeObjectAttributes(
				&Attrib, // Atributos
				&Unicode, // Com o valor UNICODE
				OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE, // Par�metros
				NULL,
				NULL
			);

			// Status
			NTSTATUS Status;

			// Al�a do arquivo
			HANDLE fileHandle;
			IO_STATUS_BLOCK IoBlock;

			// Bloqueie o arquivo
			Status = ZwOpenFile(

				// Al�a
				&fileHandle, 

				// Vamos abrir com permiss�o de leitura
				GENERIC_READ,

				// Atributos
				&Attrib,

				// IO
				&IoBlock,

				// N�o compartilhe nenhuma permiss�o com o user-mode, para impedir
				// Que o arquivo seja lido ou modificado
				0,

				// N�o � uma pasta
				FILE_NON_DIRECTORY_FILE
			);

			if (NT_SUCCESS(Status))
			{
				StatusString = "success!";
			}

			// Isso n�o vai deletar, isso apenas vai bloquear a leitura do arquivo
			//ZwDeleteFile(&Attrib);

			// Libere o UNICODE
			//ForceDeleteFile(antivirusMessage);
			RtlFreeUnicodeString(&Unicode);
		}

		// Se o antiv�rus desejar continuar o processo atual
		else if (Io->Parameters.DeviceIoControl.IoControlCode == CONTINUE_PROCESS)
		{
			// Altere o valor pra continuar o processo atual
			CONTINUE_OR_DENY_NEW_PROCCESS = 2;
			StatusString = "success!";
		}

		// Se o antiv�rus desejar negar o processo atual
		else if (Io->Parameters.DeviceIoControl.IoControlCode == DENY_PROCESS)
		{
			// Negue o processo
			CONTINUE_OR_DENY_NEW_PROCCESS = 1;
			StatusString = "success!";
		}

		// Se o antiv�rus desejar ativar a auto-defesa
		else if (Io->Parameters.DeviceIoControl.IoControlCode == ENABLE_SELF_PROTECT)
		{
			// Habilite a auto-prote��o
			selfProtectValue = TRUE;
		}

		// Se o antiv�rus desejar desativar a auto-defesa
		else if (Io->Parameters.DeviceIoControl.IoControlCode == DISABLE_SELF_PROTECT) {
			// Desabilite a auto-prote��o
			selfProtectValue = FALSE;
		}

		// Se o antiv�rus desejar 
		else if (Io->Parameters.DeviceIoControl.IoControlCode == FORCE_KILL_PROCESS)
		{
			
		}
	}

	// Porque se n�o haver, vai retornar "lixo", por padr�o, o falor � 6, igual o 'fail!'
	int MaxLengthToCopy = 6;

	// Se for a mensagem de sucesso
	if (StatusString == "success!")
	{
		// Altere o limite m�ximo
		MaxLengthToCopy = 9;
	}

	// Copie a mensagem para a mem�ria do user-mode
	RtlCopyMemory(
		// Vamos enviar pra c�
		Irp->AssociatedIrp.SystemBuffer,

		// Status
		StatusString,

		// Tamanho m�ximo da mensagem para retornar
		MaxLengthToCopy
	);

	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;

	// Complete a requisi��o
	IoCompleteRequest(Irp, IO_NO_INCREMENT);

	return STATUS_SUCCESS;
}

/*

	Quando uma requisi��o � terminada

*/
NTSTATUS Close(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;

	// Complete a requisi��o
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}

