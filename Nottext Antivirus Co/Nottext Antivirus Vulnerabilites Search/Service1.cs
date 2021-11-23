using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Threading;

namespace Nottext_Antivirus_Vulnerabilites_Search
{
    public partial class Service1 : ServiceBase
    {
        // Lista de programas indesejados
        List<string> programasLista = new List<string>();
        
        // Programas que já foram notificados
        List<string> programasNotificados = new List<string>();

        // Registros maliciosos que podem danificar a máquina
        List<string> registrosMaliciosos = new List<string>()
        {
            // HKEY_LOCAL_MACHINE
            "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\DisableTaskMgr",

            // HKEY_LOCAL_MACHINE, Image File Execution Options
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\Taskmgr.exe\\Debugger",
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\explorer.exe\\Debugger",
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\LogonUI.exe\\Debugger",
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\regedit.exe\\Debugger",
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\conhost.exe\\Debugger",
            "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\cmd.exe\\Debugger"
        };

        /// <summary>
        /// Remove os registros maliciosos, que danificar a máquina
        /// </summary>
        private void RemoverRegistrosMaliciosos()
        {
            new Thread(async () =>
            {
                while (true)
                {
                    await Task.Delay(15000);

                    // Se a proteção não estiver habilitada
                    if (ProtecaoAtivada() == false)
                    {
                        return;
                    }

                    try
                    {
                        foreach (string reg in registrosMaliciosos)
                        {
                            await Task.Delay(500);

                            try
                            {
                                
                                // Local Machina
                                RegistryKey registrykeyHKLM = Registry.LocalMachine.OpenSubKey(
                                    Path.GetDirectoryName(reg), true
                                );
                                registrykeyHKLM.DeleteValue(Path.GetFileName(reg), true);

                                // Se o código chegou até aqui, ele completou a operação
                                await Notificar("O Nottext Antiviru detectou e removeu um registro malicioso do seu computador.\r\nRegistor deletado: " + reg);

                                registrykeyHKLM.Close();
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
            }).Start();
        }

        /// <summary>
        /// Adiciona as pastas de programa potencialmente indesejados
        /// </summary>
        private void AdicionarPastas()
        {
            try
            {
                // Procure por nomes dos usuários, já que somos executados como 
                // "SISTEMA", precisamos obter todos os usuários na máquina
                foreach (string usuarios in Directory.GetDirectories("C:\\Users"))
                {
                    try
                    {
                        programasLista.Add(usuarios + "\\AppData\\Roaming\\BitTorrent");
                        programasLista.Add(usuarios + "\\AppData\\Roaming\\uTorrent");
                    } catch (Exception) { }
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Deleta uma pasta ou arquivo
        /// </summary>
        private async Task DeletarPastaOuArquivo(string local)
        {
            try
            {
                await Task.Delay(1);

                // Se for um arquivo
                if (File.Exists(local))
                {
                    // Delete o arquivo
                    File.Delete(local);
                }

                // Se for uma pasta
                else if (Directory.Exists(local))
                {
                    // Delete a pasta
                    Directory.Delete(local, true);
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Verifica se o usuário solicitou a remoção de algum programa
        /// </summary>
        private async void DeletacaoPendente()
        {
            try
            {
                // Todos os arquivos ou pastas pendentes
                string[] linhas = File.ReadAllLines(Global.Strings.deletacaoPendente);

                // Backup dos arquivos que queremos deletar, caso algum falhe na deletação
                // Vamos modificar essa váriavel pra deixar no arquivo depois
                string backupLinha = File.ReadAllText(Global.Strings.deletacaoPendente);

                // Leia linha por linha
                foreach (string linha in linhas)
                {
                    // Delete-o
                    await DeletarPastaOuArquivo(linha);
                }

                // Delete o arquivo de pastas pendentes
                File.Delete(Global.Strings.deletacaoPendente);
            } catch (Exception) { }
        }

        /// <summary>
        /// Notifica ao user-mode
        /// </summary>
        private async Task Notificar(string msg)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(3000);

                    // Se o arquivo de notificação não existir, significa que a
                    // Interface já mostrou o aviso anterior
                    if (!File.Exists(Global.Strings.notificarVulnerabilidade))
                    {
                        // Pare o loop
                        break;
                    }
                }
                
                // Notifique
                File.WriteAllText(Global.Strings.notificarVulnerabilidade, msg);
            } catch (Exception) { }
        }

        /// <summary>
        /// Executa um comando
        /// </summary>
        private async Task IniciarProcesso(string nome, string comando)
        {
            try
            {
                await Task.Delay(1);

                // Inicie o processo
                Process processo = new Process();
                processo.StartInfo.FileName = nome;
                processo.StartInfo.Arguments = comando;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processo.Start();
                processo.WaitForExit();
            } catch (Exception) { }
        }

        /// <summary>
        /// Verifica se o Windows Update está ligado ou desligado
        /// </summary>
        private async void WindowsUpdate()
        {

            try
            {
                try
                {
                    // Serviço do Windows Update
                    ServiceController sv = new ServiceController("wuauserv");

                    // Se o serviço do Windows Update estiver desabilitado
                    if (sv.StartType == ServiceStartMode.Disabled)
                    {
                        // Habilite o processo
                        await IniciarProcesso("sc.exe", "config wuauserv start= auto");

                        await Notificar("O Nottext Antivirus encontrou e corrigiu uma vulnerabilidade, o Windows Update foi restaurado ao padrão.");
                    }
                } catch (Exception) {

                    await Notificar("O Nottext Antivirus encontrou um problema relacionado ao Windows Update, estamos trabalhando para restaurar as configurações padrões, você será notificado quando a operação terminar.");

                    // Se falhar, é provavel que o serviço não existe, então,
                    // Vamos criar-lo
                    await IniciarProcesso("sc.exe", "create wuauserv start= auto binpath= " + '"' + "C:\\Windows\\system32\\svchost.exe -k netsvcs -p" + '"' + "");

                    try
                    {
                        // Agora, configurar o ServiceDLL
                        RegistryKey serviceLocation = Registry.LocalMachine.CreateSubKey("SYSTEM\\ControlSet001\\Services\\wuauserv\\Parameters");

                        // Parâmetros
                        serviceLocation.SetValue("ServiceDll", "%systemroot%\\system32\\wuaueng.dll", RegistryValueKind.ExpandString);

                        serviceLocation.SetValue("ServiceDllUnloadOnStop", 1, RegistryValueKind.DWord);

                        serviceLocation.SetValue("ServiceMain", "WUServiceMain", RegistryValueKind.String);

                    } catch (Exception) { }
                }

                try
                {
                    // Local do Windows Update
                    RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Auto Update", true);

                    // Se a atualização automática estiver desabilitada
                    if (key.GetValue("AUOptions").ToString() == "1")
                    {
                        await Notificar("O Nottext Antivirus encontrou e corrigiu uma vulnerabilidade, o Windows Update foi habilitado.");

                        // Ative o Windows Update
                        key.SetValue("AUOptions", 0, RegistryValueKind.DWord);
                        key.Close();
                    }

                } catch (Exception) { }

            } catch (Exception) {

                await Notificar("O Nottext Antivirus encontrou um erro catastrófico no seu sistema operacional, estamos trabalhando para corrigir o mais breve possível, você será notificado quando a operação terminar.");

                await IniciarProcesso("sfc.exe", "/scannow");

                await Notificar("O Nottext Antivirus terminou as operações pendentes, reinicie seu computador para aplicar as correções no seu sistema operacional");
            }
        }

        /// <summary>
        /// Verifica programas indesejados
        /// </summary>
        private void ProgramasIndesejados()
        {
            new Thread(async () =>
            {

                while (true)
                {
                    await Task.Delay(15000);

                    // Se a proteção não estiver habilitada
                    if (ProtecaoAtivada() == false)
                    {
                        return;
                    }

                    try
                    {

                        // Procure por programas indesejados
                        foreach (string programa in programasLista)
                        {
                            // 10 Segundos
                            await Task.Delay(1000);

                            // Verifique se esse programa já foi avisado de perigo
                            if (!programasNotificados.Contains(programa.ToLower()) &&
                                Directory.Exists(programa) || File.Exists(programa)
                            )
                            {
                                await Notificar("Um programa potencialmente prejudicial foi encontrado no dispositivo, recomendamos que remova o seguinte arquivo ou pasta:\r\n" + programa);

                                // Adicione o programa para não notificar de novo
                                programasNotificados.Add(programa.ToLower());
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }).Start();
        }

        /// <summary>
        /// Quando o serviço está sendo iniciado
        /// </summary>
        public Service1()
        {
            InitializeComponent();

            CanStop = false;
            CanShutdown = false;
            CanPauseAndContinue = false;
            CanHandlePowerEvent = false;
            CanHandleSessionChangeEvent = false;
        }

        /// <summary>
        /// Verifica se a proteção está ativada ou não
        /// </summary>
        /// <returns></returns>
        private bool ProtecaoAtivada()
        {
            try
            {
                // Se o arquivo existir
                if (File.Exists(Global.Strings.protecaoDeVulnerabilidade))
                {
                    // A proteção está desabilitada
                    return false;
                }
            } catch (Exception) { }

            // A proteção está habilitada
            return true;
        }

        /// <summary>
        /// Serviço iniciado
        /// </summary>
        protected override void OnStart(string[] args)
        {
            AdicionarPastas();

            try
            {
                // Programas ignorados
                string[] linhas = File.ReadAllLines(Global.Strings.listaIgnorados);

                // Adicione a lista para não notificar novamente
                foreach (string linha in linhas)
                    programasNotificados.Add(linha.ToLower());
            } catch (Exception) { }

            try
            {
                // Lista de pendentes, também será adicionado a lista para não notificar
                // Novamente
                string[] pendentes = File.ReadAllLines(Global.Strings.deletacaoPendente);

                // Adicione os pendentes para o antivírus não avisar novamente
                foreach (string linha in pendentes)
                    programasNotificados.Add(linha.ToLower());
            } catch (Exception) { }

            try
            {
                if (ProtecaoAtivada() == true)
                {
                    // Configuração do Windows Update
                    WindowsUpdate();
                }

                // Verifique se o computador possue programas que causam mal ao computador
                ProgramasIndesejados();

                // Remoção de arquivos pendentes
                DeletacaoPendente();

                // Remove registros maliciosos
                RemoverRegistrosMaliciosos();
            } catch (Exception) { }
        }

        /// <summary>
        /// Parar serviço
        /// </summary>
        protected override void OnStop()
        {
        }

    }
}
