///
/// Este arquivo cont�m todas as opera��o de IRP_MJ_SET_INFORMATION
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
	// Se estiver executando em n�vel de IRQL alto, ou o kernel que criou a opera��o
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

	// Obtenha as informa��es
	Status = FltGetFileNameInformation(
		Data, // Data
		FLT_FILE_NAME_NORMALIZED | // Nome normalizado
		FLT_FILE_NAME_QUERY_DEFAULT, // Nome padr�o
		&FileNameInfo // Salve aqui
	);

	// Valor para retornar
	NTSTATUS returnStatus = FLT_PREOP_SUCCESS_WITH_CALLBACK;

	// Se conseguir
	if (NT_SUCCESS(Status))
	{
		// Passe todas as informa��es
		Status = FltParseFileNameInformation(
			FileNameInfo
		);

		// Se conseguir
		if (NT_SUCCESS(Status))
		{
			// Se for o kernel que criou a requisi��o
			if (Data->RequestorMode == KernelMode)
			{
				// Temos que verificar se o kernel est� modificando alguma pasta com o nome
				// Nottext Antivirus, porque se n�o impedimos, o outros programas ir�o n�s
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

					// N�o passe pra outros drivers
					return FLT_PREOP_COMPLETE;
				}
				
				// Se n�o for uma pasta do Nottext Antivirus
				else {

					// Descarte o nome do arquivo
					FltReleaseFileNameInformation(FileNameInfo);

					// Se n�o for uma pasta do antiv�rus, permita o kernel completar
					// Sua opera��o
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}
			}

			// N�o precisamos mais do 'FileNameInfo'
			FltReleaseFileNameInformation(
				FileNameInfo
			);

			// Armazene o nome do disco, como C:
			PVOID DosFileName = NULL;

			// Nome do arquivo, sem o disco
			PVOID FileName = NULL;

			// Vamos configurar a inst�ncia do objeto
			PFLT_INSTANCE Instance = Objects->Instance;

			__try {

				// Obtenha o processo apartir do thread
				PEPROCESS Process = IoThreadToProcess(Data->Thread);

				// Se for um processo do antiv�rus
				if (IsNottextProcess(Process, FALSE))
				{
					// N�o continue
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
					// N�o continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Inicie o UNICODE com NULL
				RtlInitUnicodeString(&dosName, NULL);

				// Obtenha o Volume
				Status = IoVolumeDeviceToDosName(
					DiskDeviceObject, // Disk
					&dosName // Salve aqui
				);

				// M�ximo
				ULONG lengthVolume = max(
					1024, // Tamanho m�ximo do BUFFER
					DiskDeviceObject->SectorSize // Tamanho do setor
				);

				// Descarte o DiskDeviceObject
				ObDereferenceObject(
					DiskDeviceObject
				);

				// Aloque na mem�ria
				DosFileName = FltAllocatePoolAlignedWithTag(
					Instance, // A inst�ncia do objeto
					NonPagedPool, // N�o pagado, ou 0

					// M�ximo permitido
					lengthVolume,
					'nacS' // Pode ser qualquer coisa
				);

				// Se falhou ao alocar
				if (DosFileName == NULL)
				{
					// N�o podemos prosseguir
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Se n�o conseguir converter de UNICODE para CHAR
				if (!UnicodeStringToChar(
					&dosName, // Converte o nome do disco
					DosFileName // Para esse aqui
				))
				{
					// Libere o valor
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Falhou, n�o continue
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Aloque um espa�o na mem�ria
				FileName = FltAllocatePoolAlignedWithTag(

					Instance, // Nossa inst�ncia

					NonPagedPool, // N�o pagado

					lengthVolume, // Tamanho m�ximo de caracterias
					'nacS' // Pode ser qualquer coisa
				);

				//  Se n�o conseguir alocar
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

					// N�o continue
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
						// N�o queremos impedir que o kernel modifique algumas pastas
						// Necess�rias, isso causaria um BSOD
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

						// Se n�o permitir esse processo
						IsNottextProcess(Process, TRUE) == FALSE
						)
					{
						// Agora, vamos obter o nome do processo para enviar
						// O arquivo de LOG
						ANSI_STRING ProcessName;

						__try {

							// Converta pra ANSI, UNICODE � s� pra C
							Status = RtlUnicodeStringToAnsiString(
								// Salve aqui
								&ProcessName,

								// Nome do processo
								(UNICODE_STRING*)PsGetFullProcessName(Process),

								// Aloque um espa�o na mem�ria
								TRUE
							);

							// S� notifique se o antiv�rus estiver online
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

							// FLT_PREOP_COMPLETE, n�o passe para os drivers inferiores
							returnStatus = FLT_PREOP_COMPLETE;
						}
						__except (EXCEPTION_EXECUTE_HANDLER)
						{
							// Falhou
						}

						// Se o ProcessName n�o for NULO, ele foi alocado
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

	// Terminarmos, agora chame a opera��o After, ou negue o acesso, dependendo do
	// returnStatus
	return returnStatus;
}

