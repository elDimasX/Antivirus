/*

Site original:

https://blog.csdn.net/zy_strive_2012/article/details/78337637
https://www.itread01.com/content/1541838031.html

// ANTIGO CODIGO, LINK ACIMA

*/

///
/// Código obtido aqui:
/// https://github.com/ExpLife0011/DeleteFileByCreateIrp/blob/master/DeleteFile/Windows%20Driver1/Windows%20Driver1.c
/// 

/// <summary>
/// Completa a função
/// </summary>
NTSTATUS CompleteFunction(PDEVICE_OBJECT DeviceObject, PIRP Irp, PVOID Context)
{
	// Novo evento
	PKEVENT Event = (PKEVENT)Context;

	// Se tiver um evento
	if (Event)
	{
		KeSetEvent(Event, 0, 0);
	}

	// Libere o IRP
	IoFreeIrp(Irp);

	// Retorne o valor
	return STATUS_MORE_PROCESSING_REQUIRED;
}

/// <summary>
/// Completa a rotina de remover o somente-leitura
/// </summary>
NTSTATUS CompleteAttribute(PDEVICE_OBJECT DeviceObject, PIRP Irp, PVOID Context)
{
	// Configure o STATUS
	*Irp->UserIosb = Irp->IoStatus;

	// Se houver algum evento
	if (Irp->UserEvent)
	{
		// Sete o evento
		KeSetEvent(Irp->UserEvent, IO_NO_INCREMENT, 0);
	}

	// Libere o IRP
	IoFreeIrp(Irp);

	// Status
	return STATUS_MORE_PROCESSING_REQUIRED;
}

/// <summary>
/// Seta atributos em um arquivo, serve pra remover o somente-leitura
/// </summary>
NTSTATUS IrpSetFileAttributes(PFILE_OBJECT FileObject, PIO_STATUS_BLOCK IoStatusBlock, PVOID FileInformation, ULONG FileInformationLength, FILE_INFORMATION_CLASS FileInformationClass
)
{
	// Status
	NTSTATUS Status = STATUS_SUCCESS;

	// Objeto
	PDEVICE_OBJECT DeviceObject;

	// IRP
	PIRP Irp;

	// Evento
	KEVENT SycEvent;

	// IRP
	PIO_STACK_LOCATION irpSp;

	if (
		// Verifique se os valores estao nulos
		FileObject == NULL ||
		IoStatusBlock == NULL ||
		FileInformation == NULL ||
		FileInformationLength <= 0
		)
	{
		// Valores nulo, nao podemos prosseguir
		return STATUS_INVALID_PARAMETER;
	}

	// Pegue o dispositivo
	DeviceObject = IoGetRelatedDeviceObject(FileObject);

	// Se os valores estiverem nulos
	if (DeviceObject == NULL || DeviceObject->StackSize <= 0)
	{
		// Falha
		return STATUS_UNSUCCESSFUL;
	}

	// Aloque o IRP
	Irp = IoAllocateIrp(DeviceObject->StackSize, TRUE);

	// Se nao conseguir
	if (Irp == NULL)
	{
		// Libere o objeto
		//ObDereferenceObject(FileObject);

		// Falha
		return STATUS_UNSUCCESSFUL;
	}

	// Inicie o evento
	KeInitializeEvent(&SycEvent, SynchronizationEvent, FALSE);

	// Altere os valores do IRP
	Irp->AssociatedIrp.SystemBuffer = FileInformation;
	Irp->UserEvent = &SycEvent;
	Irp->UserIosb = IoStatusBlock;
	Irp->Tail.Overlay.OriginalFileObject = FileObject;
	Irp->Tail.Overlay.Thread = (PETHREAD)KeGetCurrentThread();
	Irp->RequestorMode = KernelMode;

	// Aloque e configure tudo
	irpSp = IoGetNextIrpStackLocation(Irp);
	irpSp->MajorFunction = IRP_MJ_SET_INFORMATION;
	irpSp->DeviceObject = DeviceObject;
	irpSp->FileObject = FileObject;

	// Substitua os atributos se existir
	irpSp->Parameters.SetFile.ReplaceIfExists = TRUE;
	irpSp->Parameters.SetFile.Length = FileInformationLength;
	irpSp->Parameters.SetFile.FileInformationClass = FileInformationClass;
	irpSp->Parameters.SetFile.FileObject = FileObject;

	// Complete a rotina
	IoSetCompletionRoutine(Irp, CompleteAttribute, NULL, TRUE, TRUE, TRUE);

	// Chame a funcao
	Status = IoCallDriver(DeviceObject, Irp);

	// Se estiver pendente
	if (Status == STATUS_PENDING)
	{
		// Espere a operacao
		KeWaitForSingleObject(&SycEvent, Executive, KernelMode, TRUE, NULL);
	}

	// Status
	Status = IoStatusBlock->Status;
	//ObDereferenceObject(pFileObject);

	// Retorne o status
	return Status;

}

/// <summary>
/// Deleta o arquivo desejado
/// </summary>
NTSTATUS ForceDeleteFile(PUNICODE_STRING FileName)
{
	// Atributos
	OBJECT_ATTRIBUTES ObjectAttributes;

	// Nome do arquivo em UNICODE
	UNICODE_STRING u_FileName;

	// Objeto
	PFILE_OBJECT FileObject = NULL;

	// Dispositivo
	PDEVICE_OBJECT DeviceObject;

	// Alca
	HANDLE FileHandle = NULL;

	// Status
	NTSTATUS Status;

	// IRP
	PIRP Irp;

	// CurrentLocation
	PIO_STACK_LOCATION CurrentLocation;

	// Evento
	KEVENT Event;
	IO_STATUS_BLOCK	IoBlock;

	// Valor desconhecido
	ULONG UnknownValue;

	// ANSI_STRING para converter depois
	ANSI_STRING AS;

	// Nome do arquivo para deletar em UNICODE
	UNICODE_STRING FileToDelete;

	// Vamos iniciar o ANSI_STRING com o nome do arquivo
	// Que nos foi passado
	RtlInitAnsiString(
		&AS, // Vamos salvar aqui
		FileName // O que nos foi passado
	);

	// Vamos converter o ANSI_STRING para UNICODE para podermos
	// Deletar o arquivo
	RtlAnsiStringToUnicodeString(
		&u_FileName, // Vamos iniciar este UNICODE
		&AS, // Lembra que iniciamos ele anteriormente
		TRUE // Vamos alocar o UNICODE
	);

	// Inicie os atributos
	InitializeObjectAttributes(
		&ObjectAttributes, // Vamos salvar os atributos aqui
		&u_FileName, // Arquivo para deletar
		OBJ_CASE_INSENSITIVE | OBJ_KERNEL_HANDLE, // Ignore maiuscula de minuscula
		NULL,
		NULL
	);

	/*

		A IoCreateFileSpecifyDeviceObjectHint rotina é usada por drivers de filtro do
		sistema de arquivos para enviar uma solicitação de criação apenas para os
		filtros abaixo de um objeto de dispositivo especificado e para o sistema de arquivos.

	*/

	Status = IoCreateFileSpecifyDeviceObjectHint(

		// Salve a alça aqui, para fechar o arquivo depois
		&FileHandle,

		// Permissões que vamos usar ao abrir o arquivo
		SYNCHRONIZE | FILE_WRITE_ATTRIBUTES | FILE_READ_ATTRIBUTES | FILE_READ_DATA,

		// Os atributos que inicializamos
		&ObjectAttributes,
		&IoBlock,
		NULL,

		// Normal
		FILE_ATTRIBUTE_NORMAL,

		// Compartilhe as permissões de deletar
		FILE_SHARE_DELETE,

		// Não é uma pasta
		FILE_OPEN,
		FILE_NON_DIRECTORY_FILE | FILE_SYNCHRONOUS_IO_NONALERT,
		NULL,
		NULL,

		// Não sei pra que serve
		CreateFileTypeNone,
		NULL,

		// Ignore os atributos
		IO_IGNORE_SHARE_ACCESS_CHECK | IO_IGNORE_READONLY_ATTRIBUTE,
		NULL
	);

	// Se falhar ao abrir o arquivo
	if (!NT_SUCCESS(Status))
	{
		// Tenta abrir o arquivo com permissão de leitura
		Status = ZwOpenFile(
			&FileHandle,
			GENERIC_READ,
			&ObjectAttributes,
			&IoBlock,
			FILE_SHARE_VALID_FLAGS,
			FILE_SYNCHRONOUS_IO_NONALERT | FILE_NON_DIRECTORY_FILE
		);

		// Se falhar
		if (!NT_SUCCESS(Status))
		{
			//DbgPrint("Falha ao abrir o arquivo: %x\n", Status);

			// Saia
			goto exit;
		}
	}

	// Obtenha o objeto do arquivo
	Status = ObReferenceObjectByHandle(
		// Indentificador do arquivo (alça)
		FileHandle,

		// Deletar
		DELETE,

		// Não sei pra que serve
		*IoFileObjectType,
		0,

		// Object
		(PVOID*)&FileObject,
		0
	);

	// Se falhar
	if (!NT_SUCCESS(Status))
	{
		//DbgPrint("Falha ao obter o objeto do arquivo: %x\n", Status);

		// Saia
		goto exit;
	}

	// Altere o valor para permitir deletar arquivos em uso
	FileObject->SectionObjectPointer->ImageSectionObject = 0;

	// Acesso de deletar
	FileObject->DeleteAccess = 1;

	// Informações para um arquivo
	FILE_BASIC_INFORMATION FileInformationAttribute;

	// Copie as informações para os atributos
	memset(&FileInformationAttribute, 0, sizeof(FILE_BASIC_INFORMATION));

	// Atributo de arquivo normal
	FileInformationAttribute.FileAttributes = FILE_ATTRIBUTE_NORMAL;

	// Limpe os atributos para deletar um arquivo marcado
	// Como somente-leitura, para permitir a exclusão do arquivo
	Status = IrpSetFileAttributes(
		FileObject, // Objeto do arquivo
		&IoBlock, // Status de bloqueio
		&FileInformationAttribute, // Atributos
		sizeof(FILE_BASIC_INFORMATION), // Atributos normal
		FileBasicInformation // Básico de atributos
	);

	// Obtenha o objeto de dispositivo
	DeviceObject = IoGetBaseFileSystemDeviceObject(FileObject);

	// Se falhar
	if (DeviceObject == NULL)
	{
		//DbgPrint("Falha ao obter objeto de dispositivo！\n");

		// Saia
		goto exit;
	}

	// A IoAllocateIrp rotina nao associa o IRP a um thread.
	// O driver de alocação deve liberar o IRP em vez de completá-lo de volta
	// para o gerenciador de E / S. 
	Irp = IoAllocateIrp(DeviceObject->StackSize, 1);

	// Se falhar
	if (Irp == NULL)
	{
		// Saia
		goto exit;
	}

	// Inicia o evento
	KeInitializeEvent(&Event, SynchronizationEvent, 0);

	// Valor
	UnknownValue = 1;

	// Altere os valores de IRP
	Irp->AssociatedIrp.SystemBuffer = &UnknownValue;
	Irp->UserEvent = &Event;
	Irp->UserIosb = &IoBlock;
	Irp->Tail.Overlay.OriginalFileObject = FileObject;
	Irp->Tail.Overlay.Thread = KeGetCurrentThread();

	// CurrentLocation
	CurrentLocation = Irp->Tail.Overlay.CurrentStackLocation;

	// Request de KernelMode
	Irp->RequestorMode = KernelMode;

	// Diminuia o valor
	--CurrentLocation;

	// Alte os valores
	CurrentLocation->MajorFunction = IRP_MJ_SET_INFORMATION;
	CurrentLocation->DeviceObject = DeviceObject;
	CurrentLocation->FileObject = FileObject;
	CurrentLocation->Parameters.SetFile.FileInformationClass = FileDispositionInformation;
	CurrentLocation->Parameters.SetFile.FileObject = FileObject;
	CurrentLocation->Parameters.SetFile.Length = sizeof(FILE_DISPOSITION_INFORMATION);

	// Complete a funcao
	CurrentLocation->CompletionRoutine = CompleteFunction;
	CurrentLocation->Context = &Event;
	CurrentLocation->Control = 0xE0;

	// Chame a função
	// A IoCallDriver rotina envolve IofCallDriver que envia um IRP ao driver
	// associado a um objeto de dispositivo especificado.
	// Para obter mais informações, consulte IofCallDriver. 
	Status = IoCallDriver(DeviceObject, Irp);

	// Se falhar
	if (!NT_SUCCESS(Status))
	{
		//DbgPrint("Falha ao enviar para o dispositivo %x\n", Status);

		// Saia
		goto exit;
	}

	// Espera a operação
	KeWaitForSingleObject(&Event, 0, KernelMode, FALSE, NULL);

	// Status
	//KdPrint(("Irp.status is %x\n", Irp->IoStatus.Status));

	// Saia
	goto exit;

	// Função que sai
exit:

	// Se o FileObject foi usado
	if (FileObject != NULL)
	{
		// Libere-o
		ObfDereferenceObject(FileObject);
	}

	// Se o arquivo estiver aberto
	if (FileHandle != NULL)
	{
		// Feche-o
		ZwClose(FileHandle);
	}

	// Retorne o status
	return Status;
}


