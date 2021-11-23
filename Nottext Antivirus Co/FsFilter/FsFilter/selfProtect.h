///
/// Operações para auto-proteção dos procesos
/// 

/*

	Registro para registrar

*/
PVOID RegistrationHandle = NULL;

/*

	Tipo de processo

*/
HANDLE	ProcessType = NULL;

/*

	Antes do processo terminar

*/
OB_PREOP_CALLBACK_STATUS BeforeKill(
	PVOID RegistrationContext, // Contexto

	// Informações
	POB_PRE_OPERATION_INFORMATION Information
)
{
	// Nome do processo
	LPSTR ProcssName;

	// Acesso para retornar, como ACESSO_NEGADO ou STATUS_SUCESSO
	PACCESS_MASK DesiredAccess = NULL;

	// STATUS original criado
	ACCESS_MASK OriginalAccess = 0;

	// PID
	HANDLE ProcessPidForThread;

	// Verifique se o objeto e o mesmo que PsProcessType
	if (Information->ObjectType == *PsProcessType)
	{
		// Se for o processo de auto-modificado
		if (Information->Object == PsGetCurrentProcess())
		{
			// Sucesso
			return OB_PREOP_SUCCESS;
		}
	}

	// Verifique o Objeto novamente, mas no Thread
	else if (Information->ObjectType == *PsThreadType)
	{
		// Obtenha o Processo pelo PID
		ProcessPidForThread = PsGetThreadProcessId(
			// PID
			(PETHREAD)Information->Object
		);

		// Se o thread for do nosso processo
		if (ProcessPidForThread == PsGetCurrentProcessId())
		{
			// Sucesso
			return OB_PREOP_SUCCESS;
		}
	}

	// Nada
	else
	{
		// Continue
		return OB_PREOP_SUCCESS;
	}

	// Se for uma operação feita pelo kernel
	if (Information->KernelHandle == 1)
	{
		// Pare e retorne um valor de sucesso
		return OB_PREOP_SUCCESS;
	}

	// Nome do processo completo
	char FullProcessName[260];

	if (
		// Converta pra CHAR
		UnicodeStringToChar(

			// Obtenha o nome completo do processo
			PsGetFullProcessName((PEPROCESS)Information->Object),

			// E copie para o FullProcessName
			FullProcessName

		))
	{
		// Se o processo sendo finalizado estiver localizado nas pastas
		// Protegidas
		if (selfProtectValue == TRUE && IsProtectedFile(FullProcessName) == TRUE)
		{
			// Configura o acesso que vamos retornar
			DesiredAccess = &Information->Parameters->CreateHandleInformation.DesiredAccess;

			// Configurar o acesso original que nós foi enviado
			OriginalAccess = Information->Parameters->CreateHandleInformation.OriginalDesiredAccess;

			// Se for diferente de PROCESS_CREATE_PROCESS
			// Significa que um processo não está sendo aberto
			if ((OriginalAccess & PROCESS_CREATE_PROCESS) != PROCESS_CREATE_PROCESS)
			{
				// Faça outras verificações
				if (
					// Processo está sendo terminado
					(OriginalAccess & PROCESS_TERMINATE) == PROCESS_TERMINATE ||

					// Processo está sendo suspenso
					(OriginalAccess & PROCESS_SUSPEND_RESUME) == PROCESS_SUSPEND_RESUME ||

					// Está alterando alguma informação do processo
					(OriginalAccess & PROCESS_SET_INFORMATION) == PROCESS_SET_INFORMATION
					)
				{

					// Retorne STATUS_ABANDONED, que vai fazer com que a operação
					// Seja cancelada
					*DesiredAccess &= STATUS_ABANDONED;
				}
			}
		}
	}

	// Completo
	return OB_PREOP_SUCCESS;
}

/*

	Depois do processo for finalizado

*/
VOID AfterKill(
	PVOID RegistrationContext, // Contexto

	// Informações
	POB_POST_OPERATION_INFORMATION Information
)
{
}

/*

	Instala a proteção de processo

*/
NTSTATUS InstallSelfProtect()
{
	NTSTATUS Status;

	// Vamosu usa-los para registrar as operações, igual o minifiltro
	OB_CALLBACK_REGISTRATION CallBackRegistry;
	OB_OPERATION_REGISTRATION CallBackOperation;

	// O nosso registro será feito com processos
	CallBackOperation.ObjectType = PsProcessType;

	// Vamos obter todas as requsições que forem criadas
	CallBackOperation.Operations = OB_OPERATION_HANDLE_CREATE;

	// Configure após os eventos ocorrerem
	CallBackOperation.PostOperation = AfterKill; // Antes
	CallBackOperation.PreOperation = BeforeKill; // Depois

	// Altitude da proteção, quando mais alta, primeiro será chamada
	RtlInitUnicodeString(&CallBackRegistry.Altitude, L"370000");

	// Versão, igual fizemos em Registration[]
	CallBackRegistry.Version = OB_FLT_REGISTRATION_VERSION;

	// Queremos registrar as operações
	CallBackRegistry.OperationRegistrationCount = 1;

	// Contexto
	CallBackRegistry.RegistrationContext = NULL;

	// Configure que queremos registrar
	CallBackRegistry.OperationRegistration = &CallBackOperation;

	// Registre
	Status = ObRegisterCallbacks(
		&CallBackRegistry, // Registre ele

		// Salve uma sessão aqui, para remover a proteção depois
		&RegistrationHandle
	);

	// Se falhar
	if (!NT_SUCCESS(Status))
	{
		// Retorne o erro
		return Status;
	}

	// Sucesso
	return STATUS_SUCCESS;
}

/*

	Remove a proteção de processos

*/
VOID UnInstallSelfProtect()
{
	// Se RegistrationHandle for diferente de NULL
	if (RegistrationHandle)
	{
		// Remova a proteção
		ObUnRegisterCallbacks(
			RegistrationHandle // A sessão salva em InstallSelfProtect
		);
	}
}

