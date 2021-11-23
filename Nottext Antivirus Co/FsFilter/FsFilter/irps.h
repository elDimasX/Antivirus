///
/// Arquivos de IRP, quando o antivírus envia uma mensagem
///

/*

	Quando uma nova requisição é criada

*/
NTSTATUS Create(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;

	// Complete a requisição
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}


/*

	Quando é enviada uma mensagem do antivírus

*/
NTSTATUS Control(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// PIO
	PIO_STACK_LOCATION Io = IoGetCurrentIrpStackLocation(Irp);

	// Mensagem para retornar ao user-mode
	CHAR* StatusString = "fail!";

	// Mensagem do antivírus
	PCHAR antivirusMessage = (PCHAR)Irp->AssociatedIrp.SystemBuffer;

	// Informção pra retornar ao antivírus, necessário aumentar o valor caso
	// Queira enviar uma mensagem com mais de 10 caracterias
	Irp->IoStatus.Information = 10;

	// Só execute comando se o antivírus estiver conectado á porta
	if (UserIsConnected() == TRUE)
	{
		// Se o antivírus desejar continuar a operação pendente
		if (Io->Parameters.DeviceIoControl.IoControlCode == CONTINUE_OPERATION)
		{
			// Altere o valor
			ContinueCurrentOperation = TRUE;
			StatusString = "success!";
		}

		// Se o antivírus desejar forçar a exclusão de um arquivo
		else if (Io->Parameters.DeviceIoControl.IoControlCode == FORCE_DELETE_FILE)
		{
			if (NT_SUCCESS(ForceDeleteFile(antivirusMessage)))
			{
				StatusString = "success!";
			}
		}

		// Se o antivírus desejar bloquear o arquivo
		else if (Io->Parameters.DeviceIoControl.IoControlCode == LOCK_FILE)
		{
			// ANSI e UNICODE
			ANSI_STRING Ansi;
			UNICODE_STRING Unicode;

			// Inicie o ANSI com a mensagem do antivírus
			RtlInitAnsiString(&Ansi, antivirusMessage);

			// Inicie o UNICODE
			RtlAnsiStringToUnicodeString(
				// Unicode
				&Unicode,

				// Ansi
				&Ansi,
				
				// Aloque um espaço na memória
				TRUE
			);

			// Atributos
			OBJECT_ATTRIBUTES Attrib;

			// Inicie os atributos para detectar o arquivo
			InitializeObjectAttributes(
				&Attrib, // Atributos
				&Unicode, // Com o valor UNICODE
				OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE, // Parâmetros
				NULL,
				NULL
			);

			// Status
			NTSTATUS Status;

			// Alça do arquivo
			HANDLE fileHandle;
			IO_STATUS_BLOCK IoBlock;

			// Bloqueie o arquivo
			Status = ZwOpenFile(

				// Alça
				&fileHandle, 

				// Vamos abrir com permissão de leitura
				GENERIC_READ,

				// Atributos
				&Attrib,

				// IO
				&IoBlock,

				// Não compartilhe nenhuma permissão com o user-mode, para impedir
				// Que o arquivo seja lido ou modificado
				0,

				// Não é uma pasta
				FILE_NON_DIRECTORY_FILE
			);

			if (NT_SUCCESS(Status))
			{
				StatusString = "success!";
			}

			// Isso não vai deletar, isso apenas vai bloquear a leitura do arquivo
			//ZwDeleteFile(&Attrib);

			// Libere o UNICODE
			//ForceDeleteFile(antivirusMessage);
			RtlFreeUnicodeString(&Unicode);
		}

		// Se o antivírus desejar continuar o processo atual
		else if (Io->Parameters.DeviceIoControl.IoControlCode == CONTINUE_PROCESS)
		{
			// Altere o valor pra continuar o processo atual
			CONTINUE_OR_DENY_NEW_PROCCESS = 2;
			StatusString = "success!";
		}

		// Se o antivírus desejar negar o processo atual
		else if (Io->Parameters.DeviceIoControl.IoControlCode == DENY_PROCESS)
		{
			// Negue o processo
			CONTINUE_OR_DENY_NEW_PROCCESS = 1;
			StatusString = "success!";
		}

		// Se o antivírus desejar ativar a auto-defesa
		else if (Io->Parameters.DeviceIoControl.IoControlCode == ENABLE_SELF_PROTECT)
		{
			// Habilite a auto-proteção
			selfProtectValue = TRUE;
		}

		// Se o antivírus desejar desativar a auto-defesa
		else if (Io->Parameters.DeviceIoControl.IoControlCode == DISABLE_SELF_PROTECT) {
			// Desabilite a auto-proteção
			selfProtectValue = FALSE;
		}

		// Se o antivírus desejar 
		else if (Io->Parameters.DeviceIoControl.IoControlCode == FORCE_KILL_PROCESS)
		{
			
		}
	}

	// Porque se não haver, vai retornar "lixo", por padrão, o falor é 6, igual o 'fail!'
	int MaxLengthToCopy = 6;

	// Se for a mensagem de sucesso
	if (StatusString == "success!")
	{
		// Altere o limite máximo
		MaxLengthToCopy = 9;
	}

	// Copie a mensagem para a memória do user-mode
	RtlCopyMemory(
		// Vamos enviar pra cá
		Irp->AssociatedIrp.SystemBuffer,

		// Status
		StatusString,

		// Tamanho máximo da mensagem para retornar
		MaxLengthToCopy
	);

	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;

	// Complete a requisição
	IoCompleteRequest(Irp, IO_NO_INCREMENT);

	return STATUS_SUCCESS;
}

/*

	Quando uma requisição é terminada

*/
NTSTATUS Close(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	// Sucesso
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;

	// Complete a requisição
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}

