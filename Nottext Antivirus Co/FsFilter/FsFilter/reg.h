///
/// Prote��o do registro, eu n�o entendi este c�digo
/// Mas tentei comenta-l�
/// 

/*

	Cookie, usado para guardar a sess�o e remover a prote��o quando o driver
	For descarregado

*/
LARGE_INTEGER Cookie = { 0 };

/*

	Usado para obter o nome de uma chave

*/
NTKERNELAPI NTSTATUS ObQueryNameString
(
	IN  PVOID Object,
	OUT POBJECT_NAME_INFORMATION ObjectNameInfo,
	IN  ULONG Length,
	OUT PULONG ReturnLength
);

/*

	Tag, usado para alocar buffers

*/
#define REGISTRY_POOL_TAG 'pRE'


/*

	Verifica quais processos bloquear, e quais permitir pra modificar o registro

*/
BOOLEAN GrantProcessRegistry(

)
{
	// Processo
	PEPROCESS Process = PsGetCurrentProcess();

	// Verifique quais processo est�o modificando o registro
	if (IsNottextProcess(Process, FALSE))
	{
		// Verdadeiro, este � um processo do antiv�rus
		return TRUE;
	}

	// N�o encontrado, n�o permita os processos
	return FALSE;
}

/*

	Vamos configurar a fun��o que obt�m o caminho completo no registro

*/
BOOLEAN GetRegistryObjectCompleteName(
	PUNICODE_STRING pRegistryPath,
	PVOID pRegistryObject
)
{
	// BOOLEAN
	BOOLEAN Name = FALSE;
	BOOLEAN Partial = FALSE;

	// Se n�o um endere�o de mem�ria v�lido
	if (!MmIsAddressValid(pRegistryObject) || (pRegistryObject) == NULL)
	{
		// Falso
		return FALSE;
	}

	/*
	// Se pPartialRegistryPath for diferente de NULL
	if (pPartialRegistryPath != NULL)
	{
		if (
			
			// Verifica os valores do Buffer
			(((pPartialRegistryPath->Buffer[0] == '\\') ||
			(pPartialRegistryPath->Buffer[0] == '%')) ||
			((pPartialRegistryPath->Buffer[0] == 'T') &&
			(pPartialRegistryPath->Buffer[1] == 'R') &&
			(pPartialRegistryPath->Buffer[2] == 'Y') &&
			(pPartialRegistryPath->Buffer[3] == '\\')))

			)
		{
			// Copie o valor para o pRegistryPath
			RtlCopyUnicodeString(pRegistryPath, pPartialRegistryPath);

			// Altere os valores
			Partial = TRUE;
			Name = TRUE;
		}
	}
	*/

	// Se o Name for falso
	if (!Name)
	{
		// Status
		NTSTATUS Status;

		// Tamanho pra retornar
		ULONG returnLen;

		// ObjectName
		PUNICODE_STRING ObjectName = NULL;

		// Obtenha o nome do registro
		Status = ObQueryNameString(
			// Objeto do registro
			pRegistryObject,

			// Nome
			(POBJECT_NAME_INFORMATION)ObjectName,

			0,
			&returnLen // ReturnLength
		);

		// Se obter um erro
		if (Status == STATUS_INFO_LENGTH_MISMATCH)
		{
			// Aloque um espa�o na mem�ria
			ObjectName = ExAllocatePoolWithTag(NonPagedPool, returnLen, REGISTRY_POOL_TAG);

			// Obtenha o nome de novo, mais com mais precis�o
			Status = ObQueryNameString(pRegistryObject, (POBJECT_NAME_INFORMATION)ObjectName, returnLen, &returnLen);

			// Se conseguir
			if (NT_SUCCESS(Status))
			{
				// Copie o valor para o que n�s foi passado, assim, a fun��o
				// Que chamou a gente vai obter o valor
				RtlCopyUnicodeString(pRegistryPath, ObjectName);
				
				// Conseguimos
				Name = TRUE;
			}

			// Libere o valor
			ExFreePoolWithTag(ObjectName, REGISTRY_POOL_TAG);
		}
	}

	// Retorne o BOOLEANO
	return Name;
}

/*

	Fun��o usada para proteger chaves no registro

*/
NTSTATUS RegistrerCallback(
	PVOID CallbackContext,
	PVOID Argument1, // Argumento 1, sempre tem um valor pelo registro
	PVOID Argument2 // Argumento 2, sempre tem um valor pelo registro
)
{
	// REG_NOTIFY_CLASS
	REG_NOTIFY_CLASS Class = (REG_NOTIFY_CLASS)(ULONG_PTR)Argument1;

	// Local do registro, onde ficar� armazenado o valor
	UNICODE_STRING RegistryPath;

	// Tamanho 0
	RegistryPath.Length = 0;

	// Tamanho m�ximo para RegistryPath
	RegistryPath.MaximumLength = 1024 * sizeof(WCHAR);

	// Aloque um espa�o na mem�ria para o RegistryPath
	RegistryPath.Buffer = ExAllocatePoolWithTag(NonPagedPool, RegistryPath.MaximumLength, REGISTRY_POOL_TAG);

	// Vamos verificar se � uma opera��o que queremos rastrear
	if (

		// Deletando um valor de uma chave
		RegNtPreDeleteValueKey == Class ||

		// Deletando uma chave (pasta)
		RegNtPreDeleteKey == Class ||

		// Setando um valor em uma chave
		RegNtPreSetValueKey == Class ||

		// Nova chave criada
		RegNtPreCreateKey == Class ||

		// Renomeando uma chave
		RegNtPreRenameKey == Class

		)
	{
		// Obtenha o nome do registro que est� sendo modificaod
		GetRegistryObjectCompleteName(
			// Copie o valor de volta pra esse carinha
			&RegistryPath,

			// Argumento
			((PREG_SET_VALUE_KEY_INFORMATION)Argument2)->Object
		);

		if (RegistryPath.Buffer != NULL)
		{
			// Se conter o nome Nottext Antivirus
			if (
				wcsstr(RegistryPath.Buffer, L"Nottext Antivirus") != NULL &&

				// Se n�o for um processo permitido, negue a a��o
				GrantProcessRegistry() == FALSE &&
				selfProtectValue == TRUE
			)
			{
				// Se for diferente de NULL
				if (RegistryPath.Buffer != NULL)
				{
					// Libere o valor
					ExFreePoolWithTag(RegistryPath.Buffer, REGISTRY_POOL_TAG);
				}

				// Acesso negado
				return STATUS_ACCESS_DENIED;
			}
		}
	}

	// Se estiver diferente de NULL, o valor foi alocado
	if (RegistryPath.Buffer != NULL)
	{
		// Libere o valor que alocamos
		ExFreePoolWithTag(RegistryPath.Buffer, REGISTRY_POOL_TAG);
	}

	// Status de sucesso
	return STATUS_SUCCESS;
}

/*

	Instala a prote��o de registro

*/
NTSTATUS InstallRegMonitor(
	PDRIVER_OBJECT DriverObject // DriverObject
)
{
	// Status
	NTSTATUS Status = STATUS_SUCCESS;

	// Altitude da prote��o de registro
	UNICODE_STRING Altitude = RTL_CONSTANT_STRING("375500");

	// Registre a prote��o
	Status = CmRegisterCallbackEx(
		RegistrerCallback, // Fun��o de prote��o
		&Altitude, // Altitude
		DriverObject,
		NULL,
		&Cookie, // Salve a sess�o
		NULL
	);

	// Status
	return Status;
}

/*

	Remove a prote��o de registro

*/
VOID UnInstallRegMonitor()
{
	// Remova a prote��o de registro
	CmUnRegisterCallback(Cookie);
	
	// Sucesso
	return STATUS_SUCCESS;
}

