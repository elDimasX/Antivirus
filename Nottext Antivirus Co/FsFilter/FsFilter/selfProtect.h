///
/// Opera��es para auto-prote��o dos procesos
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

	// Informa��es
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

	// Se for uma opera��o feita pelo kernel
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

			// Configurar o acesso original que n�s foi enviado
			OriginalAccess = Information->Parameters->CreateHandleInformation.OriginalDesiredAccess;

			// Se for diferente de PROCESS_CREATE_PROCESS
			// Significa que um processo n�o est� sendo aberto
			if ((OriginalAccess & PROCESS_CREATE_PROCESS) != PROCESS_CREATE_PROCESS)
			{
				// Fa�a outras verifica��es
				if (
					// Processo est� sendo terminado
					(OriginalAccess & PROCESS_TERMINATE) == PROCESS_TERMINATE ||

					// Processo est� sendo suspenso
					(OriginalAccess & PROCESS_SUSPEND_RESUME) == PROCESS_SUSPEND_RESUME ||

					// Est� alterando alguma informa��o do processo
					(OriginalAccess & PROCESS_SET_INFORMATION) == PROCESS_SET_INFORMATION
					)
				{

					// Retorne STATUS_ABANDONED, que vai fazer com que a opera��o
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

	// Informa��es
	POB_POST_OPERATION_INFORMATION Information
)
{
}

/*

	Instala a prote��o de processo

*/
NTSTATUS InstallSelfProtect()
{
	NTSTATUS Status;

	// Vamosu usa-los para registrar as opera��es, igual o minifiltro
	OB_CALLBACK_REGISTRATION CallBackRegistry;
	OB_OPERATION_REGISTRATION CallBackOperation;

	// O nosso registro ser� feito com processos
	CallBackOperation.ObjectType = PsProcessType;

	// Vamos obter todas as requsi��es que forem criadas
	CallBackOperation.Operations = OB_OPERATION_HANDLE_CREATE;

	// Configure ap�s os eventos ocorrerem
	CallBackOperation.PostOperation = AfterKill; // Antes
	CallBackOperation.PreOperation = BeforeKill; // Depois

	// Altitude da prote��o, quando mais alta, primeiro ser� chamada
	RtlInitUnicodeString(&CallBackRegistry.Altitude, L"370000");

	// Vers�o, igual fizemos em Registration[]
	CallBackRegistry.Version = OB_FLT_REGISTRATION_VERSION;

	// Queremos registrar as opera��es
	CallBackRegistry.OperationRegistrationCount = 1;

	// Contexto
	CallBackRegistry.RegistrationContext = NULL;

	// Configure que queremos registrar
	CallBackRegistry.OperationRegistration = &CallBackOperation;

	// Registre
	Status = ObRegisterCallbacks(
		&CallBackRegistry, // Registre ele

		// Salve uma sess�o aqui, para remover a prote��o depois
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

	Remove a prote��o de processos

*/
VOID UnInstallSelfProtect()
{
	// Se RegistrationHandle for diferente de NULL
	if (RegistrationHandle)
	{
		// Remova a prote��o
		ObUnRegisterCallbacks(
			RegistrationHandle // A sess�o salva em InstallSelfProtect
		);
	}
}

