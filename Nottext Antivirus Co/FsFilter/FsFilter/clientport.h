///
/// Arquivo que contém as operações de porta de comunicação
/// Por exemplo, quando o antivírus se conecta a porta
///


// Porta global, para salvar a sessão
PFLT_PORT Port;

// Porta do cliente
PFLT_PORT ClientPort;

/*

	Quando o antivírus se conecta á porta

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
		// Se for um processo do antivírus
		IsNottextProcess(Process, FALSE) &&

		// E se o usuário não estiver conectado
		UserIsConnected() == FALSE
	)
	{
		// Altere o ClientPort de nulo, para este valor
		// Para que as operações E/S começem a avisar o antivírus
		ClientPort = clientport;
		selfProtectValue = TRUE;

		//DbgPrint("!!! FsFilter.sys --- connected, port=0x%p\n", ClientPort);
		return STATUS_SUCCESS;
	}

	// Pare, retorne acesso negado
	return STATUS_ACCESS_DENIED;
}

/*

	Quando o antivírus sair, precisamos fechar a porta de comunicação
	Ou definir ClientPort como NULL, porque se não, as operações E/S ficarão
	Em loop

*/
VOID MiniDisconnect(
	PVOID connectioncookie
)
{
	//DbgPrint("!!! FsFilter.sys --- disconnected, port=0x%p\n", ClientPort);

	// Feche a porta de comunicação
	FltCloseClientPort(
		Filter, // Filtro global
		&ClientPort // O cliente
	);

	// NULL
	ClientPort = NULL;
}

