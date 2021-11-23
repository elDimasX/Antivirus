///
/// Este arquivo contém todas as operação de IRP_MJ_SET_INFORMATION
/// 

/*

	Depois que IRP_MJ_SET_INFORMATION estiver completado

*/
FLT_POSTOP_CALLBACK_STATUS SetInformationAfter(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objeto
	PVOID* Context, // Contexto
	FLT_POST_OPERATION_FLAGS Flags // Bandeira
)
{
	// Podemos apenas printar o que foi concluido
	//DbgPrint("Operation ended!");

	// Continue processando para os outros drivers
	return FLT_POSTOP_FINISHED_PROCESSING;
}

/*

	Quando IRP_MJ_SET_INFORMATION for chamado

*/
FLT_PREOP_CALLBACK_STATUS SetInformationBefore(
	PFLT_CALLBACK_DATA  Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objeto
	PVOID* Context // Contexto
)
{
	// Se estiver executando em nível de IRQL alto, ou o kernel que criou a operação
	if (KeGetCurrentIrql() != PASSIVE_LEVEL)
	{
		// Pare
		return FLT_PREOP_SUCCESS_WITH_CALLBACK;
	}

	// Valores
	ULONG_PTR stackLow;
	ULONG_PTR stackHigh;

	// FileObject
	PFILE_OBJECT FileObject = Data->Iopb->TargetFileObject;

	NTSTATUS Status;

	// PFLT_FILE_NAME_INFORMATION
	PFLT_FILE_NAME_INFORMATION FileNameInfo;

	// Obtenha as informações
	Status = FltGetFileNameInformation(
		Data, // Data
		FLT_FILE_NAME_NORMALIZED | // Nome normalizado
		FLT_FILE_NAME_QUERY_DEFAULT, // Nome padrão
		&FileNameInfo // Salve aqui
	);

	// Valor para retornar
	NTSTATUS returnStatus = FLT_PREOP_SUCCESS_WITH_CALLBACK;

	// Se conseguir
	if (NT_SUCCESS(Status))
	{
		// Passe todas as informações
		Status = FltParseFileNameInformation(
			FileNameInfo
		);

		// Se conseguir
		if (NT_SUCCESS(Status))
		{
			// Se for o kernel que criou a requisição
			if (Data->RequestorMode == KernelMode)
			{
				// Temos que verificar se o kernel está modificando alguma pasta com o nome
				// Nottext Antivirus, porque se não impedimos, o outros programas irão nós
				// Deletar da pasta
				if (
					selfProtectValue == TRUE &&

					wcsstr( FileNameInfo->Name.Buffer, 
						L"\\ProgramData\\Nottext\\Nottext Antivirus") ||

					wcsstr(FileNameInfo->Name.Buffer,
						L"\\Windows\\System32\\drivers\\FsFilter.sys") ||

					wcsstr(FileNameInfo->Name.Buffer,
						L"\\Windows\\SysWOW64\\drivers\\FsFilter.sys") ||

					wcsstr(FileNameInfo->Name.Buffer,
						L"\\Program Files\\Nottext\\Antivirus") ||

					wcsstr(FileNameInfo->Name.Buffer,
						L"\\Program Files (x86)\\Nottext\\Antivirus"
				))
				{
					FltReleaseFileNameInformation(FileNameInfo);

					// Acesso negado
					Data->IoStatus.Status = STATUS_ACCESS_DENIED;
					Data->IoStatus.Information = 0;

					// Não passe pra outros drivers
					return FLT_PREOP_COMPLETE;
				}
				
				// Se não for uma pasta do Nottext Antivirus
				else {

					// Descarte o nome do arquivo
					FltReleaseFileNameInformation(FileNameInfo);

					// Se não for uma pasta do antivírus, permita o kernel completar
					// Sua operação
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}
			}

			// Não precisamos mais do 'FileNameInfo'
			FltReleaseFileNameInformation(
				FileNameInfo
			);

			// Armazene o nome do disco, como C:
			PVOID DosFileName = NULL;

			// Nome do arquivo, sem o disco
			PVOID FileName = NULL;

			// Vamos configurar a instância do objeto
			PFLT_INSTANCE Instance = Objects->Instance;

			__try {

				// Obtenha o processo apartir do thread
				PEPROCESS Process = IoThreadToProcess(Data->Thread);

				// Se for um processo do antivírus
				if (IsNottextProcess(Process, FALSE))
				{
					// Não continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Volume
				PFLT_VOLUME volume;

				// Objeto de dispositivo
				PDEVICE_OBJECT DiskDeviceObject;

				// Nome do DOS
				UNICODE_STRING dosName;

				// Configure o Volume
				volume = Objects->Volume;

				// Pegue o disco do dispositivo de objeto
				Status = FltGetDiskDeviceObject(
					volume, // Volume
					&DiskDeviceObject // Salve aqui
				);

				// Se falhar
				if (!NT_SUCCESS(Status))
				{
					// Não continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Inicie o UNICODE com NULL
				RtlInitUnicodeString(&dosName, NULL);

				// Obtenha o Volume
				Status = IoVolumeDeviceToDosName(
					DiskDeviceObject, // Disk
					&dosName // Salve aqui
				);

				// Máximo
				ULONG lengthVolume = max(
					1024, // Tamanho máximo do BUFFER
					DiskDeviceObject->SectorSize // Tamanho do setor
				);

				// Descarte o DiskDeviceObject
				ObDereferenceObject(
					DiskDeviceObject
				);

				// Aloque na memória
				DosFileName = FltAllocatePoolAlignedWithTag(
					Instance, // A instância do objeto
					NonPagedPool, // Não pagado, ou 0

					// Máximo permitido
					lengthVolume,
					'nacS' // Pode ser qualquer coisa
				);

				// Se falhou ao alocar
				if (DosFileName == NULL)
				{
					// Não podemos prosseguir
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Se não conseguir converter de UNICODE para CHAR
				if (!UnicodeStringToChar(
					&dosName, // Converte o nome do disco
					DosFileName // Para esse aqui
				))
				{
					// Libere o valor
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Falhou, não continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Aloque um espaço na memória
				FileName = FltAllocatePoolAlignedWithTag(

					Instance, // Nossa instância

					NonPagedPool, // Não pagado

					lengthVolume, // Tamanho máximo de caracterias
					'nacS' // Pode ser qualquer coisa
				);

				//  Se não conseguir alocar
				if (FileName == NULL)
				{
					// Libere o valor do C:
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Pare
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Se falhar ao converter UNICODE para CHAR com o nome do arquivo
				if (!UnicodeStringToChar(

					// UNICODE
					&FileObject->FileName,

					// CHAR
					FileName
				))
				{
					// Libere os valores
					FltFreePoolAlignedWithTag(Instance, FileName, 'nacS');
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Não continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Quebra de linha
				strcat(
					FileName,
					"\r\n"
				);

				// Adicione o nome do disco com o nome do arquivo
				strcat(
					DosFileName, // Nome do DOS, tipo, C:
					FileName // Nome do arquivo
				);

				// Se for um arquivo protegido
				if (selfProtectValue == TRUE && IsProtectedFile(DosFileName) == TRUE)
				{
					// Negue o acesso
					Data->IoStatus.Status = STATUS_ACCESS_DENIED;
					Data->IoStatus.Information = 0;

					// Para retornar acesso negado
					returnStatus = FLT_PREOP_COMPLETE;
				}

				// Se for uma pasta do anti-ransomwares
				else if
					(
						// Não queremos impedir que o kernel modifique algumas pastas
						// Necessárias, isso causaria um BSOD
						Data->RequestorMode != KernelMode &&

						// Pastas que devem ser protegidas
						strstr(DosFileName, "\\DESKTOP") ||
						strstr(DosFileName, "\\DOWNLOADS") ||
						strstr(DosFileName, "\\IMAGES") ||
						strstr(DosFileName, "\\MUSICS") ||
						strstr(DosFileName, "\\ONEDRIVE") ||
						strstr(DosFileName, "\\PICTURES") ||
						strstr(DosFileName, "\\VIDEOS") ||
						strstr(DosFileName, "\\DOCUMENTS")
						)
				{
					// As pastam que devem ser protegidas
					if (
						// Se o anti-ransomware estiver habilitado
						FileExists(ANTI_RANSOMWARE_FILE) &&

						// Se não permitir esse processo
						IsNottextProcess(Process, TRUE) == FALSE
						)
					{
						// Agora, vamos obter o nome do processo para enviar
						// O arquivo de LOG
						ANSI_STRING ProcessName;

						__try {

							// Converta pra ANSI, UNICODE é só pra C
							Status = RtlUnicodeStringToAnsiString(
								// Salve aqui
								&ProcessName,

								// Nome do processo
								(UNICODE_STRING*)PsGetFullProcessName(Process),

								// Aloque um espaço na memória
								TRUE
							);

							// Só notifique se o antivírus estiver online
							if (UserIsConnected() == TRUE)
							{
								// Notifique ao qual arquivo foi modificado
								SendToUserMode(

									// Arquivo de LOG
									ANTI_RANSOMWARE_FILE_LOG,

									// Nome do processo
									DosFileName,
									FILE_GENERIC_WRITE
								);

								// Notifique ao user-mode qual processo o modificou
								SendToUserMode(

									// Arquivo de LOG
									ANTI_RANSOMWARE_PROCESS_LOG,

									// Nome do processo
									ProcessName.Buffer,
									FILE_GENERIC_WRITE
								);
							}

							// Acesso negado
							Data->IoStatus.Status = STATUS_ACCESS_DENIED;
							Data->IoStatus.Information = 0;

							// FLT_PREOP_COMPLETE, não passe para os drivers inferiores
							returnStatus = FLT_PREOP_COMPLETE;
						}
						__except (EXCEPTION_EXECUTE_HANDLER)
						{
							// Falhou
						}

						// Se o ProcessName não for NULO, ele foi alocado
						if (ProcessName.Buffer != NULL) {

							// Libere o valor alocado
							RtlFreeAnsiString(&ProcessName);
						}
					}
				}
			}

			__finally {

				if (FileName != NULL) {
					FltFreePoolAlignedWithTag(Instance, FileName, 'nacS');
				}

				if (DosFileName != NULL)
				{
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');
				}
			}
		}
	}

	// Terminarmos, agora chame a operação After, ou negue o acesso, dependendo do
	// returnStatus
	return returnStatus;
}

