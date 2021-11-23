///
/// Arquivo que cont�m as opera��es de porta de comunica��o
/// Por exemplo, quando o antiv�rus se conecta a porta
///


// Porta global, para salvar a sess�o
PFLT_PORT Port;

// Porta do cliente
PFLT_PORT ClientPort;

/*

	Quando o antiv�rus se conecta � porta

*/
NTSTATUS MiniConnect(
	PFLT_PORT clientport,
	PVOID serverportcookie,
	PVOID Context,
	ULONG Size,
	PVOID Connectioncookie
)
{
	// Processo atual
	PEPROCESS Process = PsGetCurrentProcess();

	if (
		// Se for um processo do antiv�rus
		IsNottextProcess(Process, FALSE) &&

		// E se o usu�rio n�o estiver conectado
		UserIsConnected() == FALSE
	)
	{
		// Altere o ClientPort de nulo, para este valor
		// Para que as opera��es E/S come�em a avisar o antiv�rus
		ClientPort = clientport;
		selfProtectValue = TRUE;

		//DbgPrint("!!! FsFilter.sys --- connected, port=0x%p\n", ClientPort);
		return STATUS_SUCCESS;
	}

	// Pare, retorne acesso negado
	return STATUS_ACCESS_DENIED;
}

/*

	Quando o antiv�rus sair, precisamos fechar a porta de comunica��o
	Ou definir ClientPort como NULL, porque se n�o, as opera��es E/S ficar�o
	Em loop

*/
VOID MiniDisconnect(
	PVOID connectioncookie
)
{
	//DbgPrint("!!! FsFilter.sys --- disconnected, port=0x%p\n", ClientPort);

	// Feche a porta de comunica��o
	FltCloseClientPort(
		Filter, // Filtro global
		&ClientPort // O cliente
	);

	// NULL
	ClientPort = NULL;
}

