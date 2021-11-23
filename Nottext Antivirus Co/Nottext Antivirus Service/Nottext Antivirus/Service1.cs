//////////////////////////////////////////////////////////////////////////
// © Nottext Antivirus - 2021 All rights reserved;
// The code as been writed by: Dimas Pereira de Souza Junior;
// My discord is: elDimas#4803;
// Thank you!;
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Nottext_Antivirus
{
    public partial class Service1 : ServiceBase
    {
        // Nome do driver de kernel (serviço)
        string kernelDriverNome = "Nottext Antivirus Driver Kernel";

        /// <summary>
        /// Analisa os processos abertos
        /// </summary>
        private async void VerificarTodosOsProcessos(int intervalo)
        {
            try
            {
                await Task.Delay(1); // Intervalo

                // Só continue caso a proteção esteja habilitada
                if (global.Strings.ProtecaoHabilita == false)
                {
                    // Proteção desabilitada, não execute mais nada
                    return;
                }

                // Procura de processo em processo
                foreach (Process processo in Process.GetProcesses())
                {
                    try
                    {
                        // Escaneie o processo
                        Escanear.EscanearProcesso(processo, intervalo);

                        // Intervalo
                        await Task.Delay(intervalo);
                    } catch (Exception) { }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Adiciona a pasta para ser monitorada, para realizar escaneamentos, e para a proteção
        /// Em user-mode
        /// </summary>
        private void AdicionarPastaMonitorar(string pasta)
        {
            try
            {
                // FileSystemWatcher
                FileSystemWatcher monitorar = new FileSystemWatcher();

                // Monitore a pasta
                monitorar.Path = pasta;

                // Configurações adicionais
                monitorar.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess |
                                         NotifyFilters.DirectoryName | NotifyFilters.FileName |
                                         NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                         NotifyFilters.Security | NotifyFilters.Size;

                monitorar.Filter = "*.*"; // Monitora tudo

                // Habiltia em subdiretorios
                monitorar.IncludeSubdirectories = true;
                monitorar.Changed += new FileSystemEventHandler(ArquivoModificado);
                monitorar.Created += new FileSystemEventHandler(ArquivoCriado);

                // Habilitar a monitoração
                monitorar.EnableRaisingEvents = true;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Monitoração de arquivo
        /// </summary>
        private void ProtecaoArquivo()
        {
            // Adicionas pastas a serem monitoradas
            //AdicionarPastaMonitorar("C:\\Users\\" + Environment.UserName + "\\Downloads");
            //AdicionarPastaMonitorar("A:\\"); // 1
            //AdicionarPastaMonitorar("B:\\"); // 2

            // Se a proteção de kernel não estiver habilitada
            if (global.Strings.kernelAtivado == false)
            {
                // Monitore tudo
                AdicionarPastaMonitorar("C:\\"); // 3
            }

            // Se a proteção de kernel estiver habilitada
            else
            {
                // Monitre somente a pasta do antivírus
                AdicionarPastaMonitorar("C:\\ProgramData\\Nottext\\Nottext Antivirus\\");
            }
            
            //AdicionarPastaMonitorar("D:\\"); // 4
            //AdicionarPastaMonitorar("E:\\"); // 5
            //AdicionarPastaMonitorar("F:\\"); // 6
            //AdicionarPastaMonitorar("G:\\"); // 7
            //AdicionarPastaMonitorar("H:\\"); // 8
            //AdicionarPastaMonitorar("I:\\"); // 9
            //AdicionarPastaMonitorar("J:\\"); // 10
            //AdicionarPastaMonitorar("K:\\"); // 11
            //AdicionarPastaMonitorar("L:\\"); // 12
            //AdicionarPastaMonitorar("M:\\"); // 13
            //AdicionarPastaMonitorar("N:\\"); // 14
            //AdicionarPastaMonitorar("O:\\"); // 15
            //AdicionarPastaMonitorar("P:\\"); // 16
            //AdicionarPastaMonitorar("Q:\\"); // 17
            //AdicionarPastaMonitorar("R:\\"); // 18
            //AdicionarPastaMonitorar("S:\\"); // 19
            //AdicionarPastaMonitorar("T:\\"); // 20
            //AdicionarPastaMonitorar("U:\\"); // 21
            //AdicionarPastaMonitorar("V:\\"); // 22
            //AdicionarPastaMonitorar("W:\\"); // 23
            //AdicionarPastaMonitorar("X:\\"); // 24
            //AdicionarPastaMonitorar("Y:\\"); // 25
            //AdicionarPastaMonitorar("Z:\\"); // 26
        }

        /// <summary>
        /// Lê as linhas de um arquivo e escaneia as linhas lidas
        /// </summary>
        private async void EscaneamentoManual(string arquivo)
        {
            try
            {
                // Lê todas as linhas
                string[] linhas = File.ReadAllLines(arquivo);

                // Lê linha por linha
                foreach (string linha in linhas)
                {
                    try
                    {
                        // Se for uma pasta
                        if (Directory.Exists(linha))
                        {
                            // Novo DirectoryInfo
                            DirectoryInfo info = new DirectoryInfo(linha);

                            // Escaneia uma pasta, mas não é verificação
                            await Escanear.EscanearPasta(info, "", false);
                        }

                        // Se for um arquivo
                        else if (File.Exists(linha))
                        {
                            // Escaneia o arquivo
                            Escanear.EscanearArquivo(linha, true, 0);
                        }
                    } catch (Exception) { }
                }

                // Deleta os arquivos
                File.Delete(global.Strings.arquivoEscan);
            } catch (Exception) { }
        }

        /// <summary>
        /// Um loop infinito procurando saber se o antivírus deseja realizar algum
        /// Escaneamento, e também verifica o status da proteção em tempo real
        private void LoopEscaneamento()
        {
            // Um novo thread
            new Thread(async() =>
            {
                // Repetição infinita
                while (true)
                {
                    // 5 Segundos
                    await Task.Delay(5000);

                    // Se o arquivo for para verificação rápida
                    if (File.Exists(global.Strings.verRapida))
                    {
                        // Escaneie todos os processos
                        VerificarTodosOsProcessos(5);

                        // DirectoryInfo, System32
                        DirectoryInfo system32 = new DirectoryInfo("C:\\Windows\\System32");

                        // Pasta do ProgramData
                        DirectoryInfo programData = new DirectoryInfo("C:\\ProgramData");

                        // Escaneia as pastas
                        await Escanear.EscanearPasta(system32, global.Strings.verRapida, true);

                        // Depois, escaneie a ProgramData
                        await Escanear.EscanearPasta(programData, global.Strings.verRapida, true);

                        // Depois, delete o arquivo para o user-mode
                        // Poder criar outro
                        File.Delete(global.Strings.verRapida);
                    }

                    // Se o arquivo for para verificação completa
                    else if (File.Exists(global.Strings.verCompleta))
                    {
                        // Escaneie todos os processos
                        VerificarTodosOsProcessos(5);

                        // C:\\
                        DirectoryInfo C = new DirectoryInfo("C:\\");

                        // Escaneia a pasta
                        await Escanear.EscanearPasta(C, global.Strings.verCompleta, true);

                        // Depois, delete o arquivo para o user-mode
                        // Poder criar outro
                        File.Delete(global.Strings.verCompleta);
                    }

                    // Se for o escaneamento manual
                    if (File.Exists(global.Strings.arquivoEscan))
                    {
                        // Faça um escan manual
                        EscaneamentoManual(global.Strings.arquivoEscan);
                    }
                }
            }).Start();

            // Mais um thread, que vai verificar o status da proteção em tempo real
            new Thread(async () =>
            {
                while (true)
                {
                    // 5 Segundos
                    await Task.Delay(5000);

                    // Se o arquivo de proteção em tempo real existir
                    if (File.Exists(global.Strings.protecaoTempoRealArquivo))
                    {
                        // Se a proteção estiver desabilitada
                        if (global.Strings.ProtecaoHabilita == true)
                        {
                            // Vamos desabilitar a proteção
                            global.Strings.ProtecaoHabilita = false;
                        }
                    }

                    // Se não existir
                    else
                    {
                        // Se a proteção em tempo real estiver desabilitada
                        if (global.Strings.ProtecaoHabilita == false)
                        {
                            // Habilite a proteção
                            global.Strings.ProtecaoHabilita = true;

                            // Verifique todos os processos
                            VerificarTodosOsProcessos(0);
                        }
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Após algum arquivo for criado
        /// </summary>
        private void ArquivoCriado(object s, FileSystemEventArgs e)
        {
            // Novo thread
            new Thread(async () =>
            {
                try
                {
                    // Segundo plano
                    Thread.CurrentThread.IsBackground = true;

                    // Nome do arquivo
                    string arquivo = e.FullPath;

                    await Task.Delay(1); // Intervalo

                    if (
                        // Se for um executável ou .BAT
                        Path.GetExtension(arquivo).ToLower() == ".exe" ||
                        Path.GetExtension(arquivo).ToLower() == ".bat"
                        &&

                        // E se a proteção de kernel não estiver ativada
                        global.Strings.kernelAtivado == false &&

                        // E se a proteção estiver habilitada
                        global.Strings.ProtecaoHabilita == true
                    )
                    {
                        // Escaneie o arquivo
                        Escanear.EscanearArquivo(arquivo, true, 0);
                    }
                } catch (Exception) { }
            }).Start();
        }

        /// <summary>
        /// Após algum arquivo for modificado
        /// </summary>
        private void ArquivoModificado(object s, FileSystemEventArgs e)
        {
            // Novo thread
            new Thread(async () =>
            {
                try
                {
                    // Nome do arquivo
                    string arquivo = e.FullPath;
                    await Task.Delay(1);

                    // Se for o arquido de DB, o antivírus atualizou o banco de dados
                    if (arquivo == global.Strings.arquivoDBUpdate)
                    {
                        // Atualize o banco de dados
                        AtualizarDb();
                    }

                    else if (

                        // Se for um executável ou .BAT
                        Path.GetExtension(arquivo).ToLower() == ".exe" ||
                        Path.GetExtension(arquivo).ToLower() == ".bat"
                        &&

                        // E se a proteção de kernel não estiver ativada
                        global.Strings.kernelAtivado == false &&

                        // E se a proteção estiver habilitada
                        global.Strings.ProtecaoHabilita == true
                    )
                    {
                        // Escaneie o arquivo
                        Escanear.EscanearArquivo(arquivo, true, 0);
                    }
                }
                catch (Exception) { }
            }).Start();
        }

        /// <summary>
        /// Verifica se um arquivo está bloqueado
        /// </summary>
        private bool ArquivoBloqueado(string arquivo)
        {
            try
            {
                // Se abrir o arquivo
                using (Stream stream = new FileStream(arquivo, FileMode.Open))
                {
                    // Tudo OK, agore feche
                    stream.Close();

                    // Retorne true, pois conseguimos abrir o arquivo
                    // O arquivo não está bloqueado
                    return true;
                }
            }
            catch
            {
                // Arquivo em uso
            }

            // Falso, não conseguimos abrir o arquivo
            // O arquivo está bloqueado
            return false;
        }

        /// <summary>
        /// Iniciar uma FORM
        /// </summary>
        private async Task IniciarForm(string arquivo)
        {
            try
            {
                // Abra a FORM
                await Task.Delay(1);

                // Novo Info
                ApplicationLoader.PROCESS_INFORMATION procInfo;
                
                // Inicie o processo como sysstem
                ApplicationLoader.StartProcessAndBypassUAC(arquivo, out procInfo);
            }
            catch (Exception) { }
        }

        // Checkar o numero de repetições
        int tentativas = 0;

        /// <summary>
        /// Iniciar o Nottext Antivirus sempre que for finalizado
        /// </summary>
        private void VerificarAberto()
        {
            // Novo thread
            new Thread(async () =>
            {
                // Segundo plano
                Thread.CurrentThread.IsBackground = true;

                // Repetição infinita
                while (true)
                {
                    try
                    {
                        // Nottext Antivirus
                        string antivirus = global.Strings.interfaceAntivirus;

                        // Se o Nottext Antivirus não estiver executando
                        if (ArquivoBloqueado(antivirus))
                        {
                            // Se a tentativas passar de 3
                            if (tentativas > 3)
                            {
                                // Inicie a FORM como System, pois não conseguimos
                                // Iniciar ele através do agendador
                                await IniciarForm(antivirus);
                            }

                            tentativas++; // Aumente o valor das tentativas

                            // Inicia a UI pelo agendador de tarefas
                            await Processos.IniciarProcesso("schtasks.exe", "/run /tn Nottext");
                        }
                    }
                    catch (Exception)
                    {
                        await Task.Delay(8000); // Intervalo
                    }

                    await Task.Delay(8000); // Intervalo
                }
            }).Start();
        }

        /// <summary>
        /// Processo aberto detectado
        /// </summary>
        private void NovoProcesso(object sender, EventArrivedEventArgs e)
        {
            new Thread(() =>
            {
                // Segundo plano
                Thread.CurrentThread.IsBackground = true;

                // Vamos verificar se a proteção está desabilitada
                if (global.Strings.ProtecaoHabilita == false)
                    return; // Não execute mais nada

                try
                {
                    // Pegar o nome do processo
                    string nome = e.NewEvent.Properties["ProcessName"].Value.ToString();

                    // Pega o pid
                    string pidStr = Convert.ToInt32(
                        // PID
                        e.NewEvent.Properties["ProcessID"].Value
                    ).ToString(); // Para string

                    // Converte para INT
                    int pid = Int32.Parse(pidStr); 

                    // Pegue o processo pelo PID
                    Process processo = Process.GetProcessById(pid);
                    Escanear.EscanearProcesso(processo, 100);
                }
                catch (Exception) { }
            }).Start();
        }

        // Novo ManagementEventWatcher
        ManagementEventWatcher processoIniciado = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));

        /// <summary>
        /// Configura a proteção em tempo real
        /// </summary>
        private void ConfigurarProtecao()
        {
            // Após um processo ser iniciado
            processoIniciado.EventArrived += new EventArrivedEventHandler(NovoProcesso);
            processoIniciado.Start();
        }

        /// <summary>
        /// Impedir o serviço de parar
        /// </summary>
        private void AutoProtecaoServico()
        {
            // Não pode parar
            CanStop = false;
            
            // Não pode desligar
            CanShutdown = false;

            // Não pode pausar ou continuar
            CanPauseAndContinue = false;

            // Não pode alterar eventos
            CanHandlePowerEvent = false;

            // Não pode alterar sessões
            CanHandleSessionChangeEvent = false;
        }

        /// <summary>
        /// Tipos de eventos
        /// </summary>
        public enum EventType
        {
            BeginNestedSystemChange = 0x66,
            BeginSystemChange = 0x64,
            EndNestedSystemChange = 0x67,
            EndSystemChange = 0x65
        }

        /// <summary>
        /// Tipos de restaurações
        /// </summary>
        public enum RestorePointType
        {
            ApplicationInstall = 0x0,
            ApplicationUninstall = 0x1,
            CancelledOperation = 0xd,
            DeviceDriverInstall = 0xa,
            ModifySettings = 0xc
        }

        /// <summary>
        /// Cria o ponto de restauração
        /// </summary>
        public static void CriarPontoDeRestauracao(
            string description, // Descrição do ponto de restauração
            EventType eventType, // Tipo de evento, após instalar um aplicativo ou algo do tipo
            RestorePointType restorePointType // Point
        )
        {
            try
            {
                // Pega o local dos pontos de restauração
                var mScope = new ManagementScope("\\\\localhost\\root\\default");
                var mPath = new ManagementPath("SystemRestore");

                // Pega as opções
                var options = new ObjectGetOptions();

                // Cria um parametro para o ponto de restauração
                using (var mClass = new ManagementClass(mScope, mPath, options))
                using (var parameters = mClass.GetMethodParameters("CreateRestorePoint"))
                {
                    // Descrição
                    parameters["Description"] = description;
                    parameters["EventType"] = (int)eventType; // Os tipos de eventos
                    parameters["RestorePointType"] = (int)restorePointType; // Ponto de restauração
                    mClass.InvokeMethod("CreateRestorePoint", parameters, null); // Cria
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Proteção para Windows
        /// </summary>
        private void ProtecaoWindows()
        {
            try
            {
                // Se o arquivo existir, significa que a proteção de Windows
                // Foi desativada
                if (File.Exists(global.Strings.pontoDeRestauracao))
                {
                    // Pare
                    return;
                }

                // Se o arquivo não existir
                if (!File.Exists(global.Strings.escritoRestauracao))
                    File.Create(global.Strings.escritoRestauracao).Close(); // Crie o arquivo

                // Pega a data
                string data = DateTime.Now.ToString("dd/MM/yyyy");

                // Ler o arquivo
                string leu = File.ReadAllText(global.Strings.escritoRestauracao);

                // Se a data for igual ao arquivo
                if (leu == data)
                {
                    // Não execute nada
                    return;
                }

                try
                {
                    // Novo thread
                    new Thread(() =>
                    {
                            // Segundo plano
                            Thread.CurrentThread.IsBackground = true;

                            // Cria o ponto de restauração
                            CriarPontoDeRestauracao(
                            "NottextAntivirusBackup", // Descrição
                            EventType.BeginSystemChange, // Após o sistema mudar

                            // Quando um aplicativo for instalado
                            RestorePointType.ApplicationInstall
                        );

                            // Criou um ponto de restauração
                            File.WriteAllText(global.Strings.escritoRestauracao, data);

                            // Crie um arquivo, para que o antivírus mostre uma janela
                            // De notificação, mostrando que criou um ponto de 
                            // Restauração
                            File.WriteAllText(global.Strings.pontoCriado, "");
                    }).Start();
                }
                catch (Exception)
                {
                }

            }
            catch (Exception) { }
        }

        /// <summary>
        /// Todas as chamadas
        /// </summary>
        public Service1()
        {
            InitializeComponent();
            AutoProtecaoServico();
        }

        /// <summary>
        /// Tratar todos os erros
        /// </summary>
        private void Catch(object sender, UnhandledExceptionEventArgs e)
        {
            // Apenas ignore os erros
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifique se a proteção de kernel está instalado
        /// </summary>
        private bool ProtecaoKernel()
        {
            // Retorne true se o serviço estiver instalado
            return ServiceController.GetDevices().Any(serviceController => serviceController.ServiceName.Equals(kernelDriverNome));
        }

        /// <summary>
        /// Delete os arquivos que foram copiados para a pasta
        /// </summary>
        private void DeletarCopiados()
        {
            try
            {
                // Pegue os arquivos na pasta
                foreach (string arquivo in Directory.GetFiles("C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\COPIED"))
                {
                    try
                    {
                        // Delete o arquivo
                        File.Delete(arquivo);
                    } catch (Exception) { }
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Atualiza o banco de dados
        /// </summary>
        private void AtualizarDb()
        {
            try
            {
                // Atualize a string de DB
                global.Strings.dbUpdate = File.ReadAllText(global.Strings.arquivoDBUpdate);
            } catch (Exception) { }
        }

        /// <summary>
        /// Quando o serviço iniciar
        /// </summary>
        protected override void OnStart(string[] args)
        {
            // Vamos alterar a propriedade do processo
            // Para que não ficarmos muito lento ao escanear um processo
            Process ps = Process.GetCurrentProcess();
            ps.PriorityClass = ProcessPriorityClass.Normal;

            // Vamos verificar se o arquivo existe para ver o status da 
            // Proteção em tempo real
            if (!File.Exists(global.Strings.protecaoTempoRealArquivo))
            {
                // Arquivo não existe, proteção habilitada
                global.Strings.ProtecaoHabilita = true;
            }

            // Delete os arquivos copiados
            DeletarCopiados();

            // Declarar uma ação, quando ocorrer um erro inesperado
            AppDomain.CurrentDomain.UnhandledException += Catch;

            // Verificação rápida
            if (File.Exists(global.Strings.verRapida))
            {
                // Deleta o arquivo
                File.Delete(global.Strings.verRapida);
            }

            // Verificação completa
            if (File.Exists(global.Strings.verCompleta))
            {
                // Deleta o arquivo
                File.Delete(global.Strings.verCompleta);
            }
                
            // Verifique a proteção de kernel
            global.Strings.kernelAtivado = ProtecaoKernel();

            // Leia o arquivo baixado pelo user-mode
            AtualizarDb();

            // Cria um ponto de restauração
            ProtecaoWindows();

            // Verifique se o Nottext Antivirus está em aberto
            VerificarAberto();

            // Funções das proteções
            ProtecaoArquivo();

            // Verifique todos os processos
            VerificarTodosOsProcessos(0);

            // Se a proteção de kernel estiver ativada
            if (global.Strings.kernelAtivado == true)
            {
                // Ativa a proteção de kernel
                Kernel.ConectarAoKernel();

                // Proteção de filtro, analisa os arquivos
                Kernel.ProtecaoFiltro();

                // Proteção de processos, analisa os novos processos abertos
                Kernel.ProtecaoProcessos();
                //KernelScan.ProtecaoImagem();
            } else
            {
                // Proteção de kernel não está ativada

                // Proteção após um novo processo for aberto
                ConfigurarProtecao();
            }

            // Verifica escaneamentos, ou o status da proteção em tempo real
            LoopEscaneamento();
        }

        /// <summary>
        /// Quando o serviço fechar
        /// </summary>
        protected override void OnStop()
        {
            //
        }

    }
}