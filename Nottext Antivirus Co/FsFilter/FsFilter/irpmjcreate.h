///
/// Este arquivo contera todas as operacoes de IRP_MJ_CREATE
/// 

/*

	Depois que IRP_MJ_CREATE tiver sido completo

*/
FLT_POSTOP_CALLBACK_STATUS CreateIrpAfter(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
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

	C�digo copiado do AVSCAN da Microsoft

*/
DEFINE_GUID(GUID_ECP_CSV_DOWN_LEVEL_OPEN, 0x4248be44, 0x647f, 0x488f, 0x8b, 0xe5, 0xa0, 0x8a, 0xaf, 0x70, 0xf0, 0x28);

/*

	C�digo copiado do AVSCAN da Microsoft

*/
BOOLEAN
AvIsCsvDlEcpPresent(
	PFLT_FILTER Filter,
	PFLT_CALLBACK_DATA Data
)
{
	NTSTATUS status;
	PECP_LIST ecpList;
	PVOID ecpContext;

	PAGED_CODE();

	status = FltGetEcpListFromCallbackData(Filter, Data, &ecpList);

	if (NT_SUCCESS(status) && (ecpList != NULL)) {

		status = FltFindExtraCreateParameter(Filter,
			ecpList,
			&GUID_ECP_CSV_DOWN_LEVEL_OPEN,
			&ecpContext,
			NULL
		);

		if (NT_SUCCESS(status)) {

			return TRUE;
		}
	}

	return FALSE;
}

/*

	Depois que uma opera��o IRP_MJ_CREATE tiver sido criado

*/
FLT_PREOP_CALLBACK_STATUS CreateIrpBefore(
	PFLT_CALLBACK_DATA Data, // Data
	PCFLT_RELATED_OBJECTS Objects, // Objetos
	PVOID* Context // Contexto
)
{
	// Se estiver executando em n�vel de IRQL alto
	if (KeGetCurrentIrql() != PASSIVE_LEVEL ||

		// Requisi��o criada pelo kernel, n�o queremos interromper c�digo kernel
		Data->RequestorMode == KernelMode
		)
	{
		// Pare
		return FLT_PREOP_SUCCESS_WITH_CALLBACK;
	}

	// FileObject
	PFILE_OBJECT FileObject = Data->Iopb->TargetFileObject;

	// Valores
	ULONG_PTR stackLow;
	ULONG_PTR stackHigh;

	// A IoGetStackLimits rotina retorna os limites do quadro de pilha do segmento atual. 
	IoGetStackLimits(
		&stackLow, // O min�mo
		&stackHigh // O m�ximo
	);

	// Verifique alguns par�metros que queremos ignorar
	if (

		// Verifique o limite de quadro de pilha
		((ULONG_PTR)FileObject > stackLow) &&
		((ULONG_PTR)FileObject < stackHigh) ||

		// Abrindo uma pasta
		FlagOn(Data->Iopb->OperationFlags, SL_OPEN_TARGET_DIRECTORY) ||

		// C�digo paginado
		FlagOn(Data->Iopb->OperationFlags, SL_OPEN_PAGING_FILE) ||

		// Volume
		FlagOn(Objects->FileObject->Flags, FO_VOLUME_OPEN) ||

		// � uma pasta
		FlagOn(Data->Iopb->Parameters.Create.Options, FILE_DIRECTORY_FILE) ||

		// Outros valores que queremos ignorar
		FlagOn(Objects->FileObject->Flags, IRP_MJ_DEVICE_CONTROL) ||
		FlagOn(Objects->FileObject->Flags, IRP_MJ_FILE_SYSTEM_CONTROL) ||
		FlagOn(Objects->FileObject->Flags, IRP_MJ_INTERNAL_DEVICE_CONTROL) ||

		// Arquivo sendo aberto pelo ID, o que n�o n�s interessa
		FlagOn(Data->Iopb->Parameters.Create.Options & 0xFFFFFF, FILE_OPEN_BY_FILE_ID) ||

		// IRP_PAGING_IO
		FlagOn(Data->Iopb->IrpFlags, IRP_PAGING_IO) ||

		// Algum arquivo est� sendo escrito
		Data->Iopb->MajorFunction == IRP_MJ_WRITE ||

		// Ignora E / S gerada por minifiltro
		FlagOn(Data->Flags, FLTFL_CALLBACK_DATA_GENERATED_IO)
		|| FLT_IS_FS_FILTER_OPERATION(Data)
		|| FLT_IS_REISSUED_IO(Data) ||

		// Ignora a verifica��o de todos os arquivos abertos por CSVFS para seu n�vel inferior
		// em processamento. Isso inclui filtros na pilha NTFS oculta e
		// para filtros anexados ao MUP
		AvIsCsvDlEcpPresent(Objects->Filter, Data))
		
	{
		// Pare
		return FLT_PREOP_SUCCESS_WITH_CALLBACK;
	}

	NTSTATUS Status;

	// Informa��es
	PFLT_FILE_NAME_INFORMATION FileNameInfo;

	// Obtenha as informa��es para configurar o FileNameInfo
	Status = FltGetFileNameInformation(
		Data, // Data

		FLT_FILE_NAME_NORMALIZED | // Nome normalizado
		FLT_FILE_NAME_QUERY_DEFAULT, // Nome padr�o

		&FileNameInfo // Salve o resultado aqui
	);

	// Valor para retornar
	NTSTATUS returnStatus = FLT_PREOP_SUCCESS_WITH_CALLBACK;

	// Se conseguir
	if (NT_SUCCESS(Status))
	{
		// Retorne tudo do FileNameInfo
		Status = FltParseFileNameInformation(
			FileNameInfo
		);

		// Se conseguir
		if (NT_SUCCESS(Status))
		{
			// Descarte o FileNameInfo, n�o precisaremos mais dele
			// S� fizemos esta verifica��o para ter certeza que o nome do arquivo
			// Vai ser retornado para o antiv�rus
			FltReleaseFileNameInformation(
				FileNameInfo
			);

			// Vamos configurar a inst�ncia do objeto
			PFLT_INSTANCE Instance = Objects->Instance;

			// Armazene o nome do disco, como C:
			PVOID DosFileName = NULL;

			// Nome do arquivo, sem o disco
			PVOID FileName = NULL;

			__try {

				// Nome do arquivo veio corretamente, vamos partir pra outras
				// Verifica��es

				// Obtenha o processo apartir do thread
				PEPROCESS Process = IoThreadToProcess(Data->Thread);

				// Obt�m o nome do processo
				//CHAR* ProcessName = PsGetProcessImageFileName(Process);

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
					// Libere o valor
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Pare
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Se n�o conseguir converter o valor
				if (!UnicodeStringToChar(

					// Nome do arquivo em UNICODE
					&FileObject->FileName,

					// Salve em CHAR aqui
					FileName
				))
				{
					// Libere os valores
					FltFreePoolAlignedWithTag(Instance, FileName, 'nacS');
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');

					// Pare
					return FLT_PREOP_SUCCESS_WITH_CALLBACK;
				}

				// Copie um \R\N para adicionar ao arquivo de LOG e
				// Para que o antiv�rus possa localizar este arquivo
				// E escanear
				strcat(
					FileName, // Nome do arquivo
					"\r\n" // Quebra de linha
				);

				// Adicione o nome do dispositivo ao nome do arquivo
				// Para ficar correto, como C:\file.txt
				strcat(
					DosFileName, // Nome do DOS, tipo, C:
					FileName // Nome do arquivo
				);

				// Se for algum arquivo que queremos proteger
				if (

					// Se o arquivo estiver sendo deletado
					FlagOn(Data->Iopb->Parameters.Create.Options, FILE_DELETE_ON_CLOSE) ||

					// OU modificando o arquivo
					(Data->Iopb->Parameters.Create.SecurityContext->DesiredAccess &
						(FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA |
							FILE_APPEND_DATA | DELETE | WRITE_DAC | WRITE_OWNER))
					)
				{
					// Se for um arquivo para ser protegido
					if (selfProtectValue == TRUE && IsProtectedFile(DosFileName) == TRUE)
					{
						// Configure um atributo de somente-leitura
						Data->Iopb->Parameters.Create.FileAttributes |= FILE_ATTRIBUTE_READONLY;

						// Acesso negado
						Data->IoStatus.Status = STATUS_ACCESS_DENIED;
						Data->IoStatus.Information = 0;

						// Altere o valor de retorno
						returnStatus = FLT_PREOP_COMPLETE;
					}

					// Se n�o for isso, verifique a prote��o
					// Anti-ransomware
					else if (

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
						// Estas pastas devem ser protegidas
						// Agora, fa�a outra verifica�a�
						if (
							// Se a prote��o anti-ransomware estiver habilitado
							FileExists(ANTI_RANSOMWARE_FILE) &&

							// E esse processo n�o poder acessar esta pasta
							IsNottextProcess(Process, TRUE) == FALSE
							)
						{
							// Agora, vamos obter o nome do processo para enviar
							// O arquivo de LOG
							ANSI_STRING ProcessName;

							__try {

								// Converta pra ANSI, UNICODE � s� pra C
								Status = RtlUnicodeStringToAnsiString(
									//  Salve aqui
									&ProcessName,

									// Nome do processo
									(UNICODE_STRING*)PsGetFullProcessName(Process),

									// Aloque um espa�o na mem�ria
									TRUE
								);

								// S� notifique no arquivo caso o antiv�rus esteja
								// Online
								if (UserIsConnected() == TRUE)
								{
									// Arquivo
									SendToUserMode(

										// Arquivo de LOG
										ANTI_RANSOMWARE_FILE_LOG,

										// Nome do arquivo
										DosFileName,
										FILE_GENERIC_WRITE
									);

									// Processo
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

								// Altere o valor de retorno
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

				// Verifique algums par�metros antes de continuar para o escaneamento
				else if (

					// Verifique se desejamos ou n�o escanar este arquivo
					CheckException(DosFileName) == FALSE &&

					// E se o antiv�rus estiver conectado � porta
					UserIsConnected() == TRUE &&

					// Se o arquivo n�o estiver sendo deletado
					!FlagOn(Data->Iopb->Parameters.Create.Options, FILE_DELETE_ON_CLOSE) &&

					// Se n�o for essas opera��es
					Data->Iopb->MajorFunction != IRP_MJ_SET_INFORMATION &&
					Data->Iopb->MajorFunction != IRP_MJ_SET_SECURITY &&

					// Se for algumas das extens�es abaixo
					strstr(DosFileName, ".EXE") ||
					strstr(DosFileName, ".VBS") ||
					strstr(DosFileName, ".CMD") ||
					strstr(DosFileName, ".BAT")
				)
				{
					// Altere o valor, porque o antiv�rus ainda n�o recebeu a mensagem
					// E vamos ficar em loop at� ele n�s responder
					ContinueCurrentOperation = FALSE;

					// Envie ao antiv�rus
					SendToUserMode(

						// Anotamos nesse arquivo aqui
						FILE_TO_SEND,

						// Nome completo do arquivo
						DosFileName,

						// N�o substitua o que exisitir, apenas adicione
						FILE_APPEND_DATA
					);

					// Este arquivo j� foi enviado ao antiv�rus, n�o queremos
					// Escanear ele novamente
					strcpy(
						LastFileScanned, // �ltimo arquivo escaneado
						DosFileName // Nome do arquivo recente
					);

					// Fa�a um loop de 400 vezes (40 segundos)
					for (int i = 0; i < 400; i++)
					{
						//DbgPrint("Process %s acessing file: %s", ProcessName, DosFileName);

						// Durma
						KernelSleep();

						// Se o antiv�rus n�o estiver conectado � porta
						// Ou o antiv�rus autorizou a continua��o pendente
						if (UserIsConnected() == FALSE || ContinueCurrentOperation == TRUE)
						{
							// Pare
							break;
						}
					}
				}


			}
			__finally {

				// Libere os valores alocados
				if (FileName != NULL) {
					FltFreePoolAlignedWithTag(Instance, FileName, 'nacS');
				}

				// Verifique se j� foi alocado
				if (DosFileName != NULL)
				{
					FltFreePoolAlignedWithTag(Instance, DosFileName, 'nacS');
				}
			}
		}
	}

	// Terminarmos, agora, passe a opera��o para os outros drivers.
	return returnStatus;
}


