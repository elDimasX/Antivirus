//////////////////////////////////////////////////////////////////////////
// © Nottext Antivirus Co - 2020 All rights reserved;
// The code as been writed by: Dimas Pereira de Souza Junior;
// My discord is: elDimas#4803;
// Thank you!;
//////////////////////////////////////////////////////////////////////////

using Nottext_Antivirus.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace Nottext_Antivirus
{
    public partial class Form1 : Form
    {
        // Todas as FORMS
        Protections form1 = new Protections();
        Quarentine form2 = new Quarentine();
        virusScan form3 = new virusScan();
        DatabaseFORM form4 = new DatabaseFORM();
        Windows form5 = new Windows();
        Settings form6 = new Settings();

        // Versão de testes
        bool versaoTeste = false;

        // Local do serviço do antivírus
        string antivirusService = Application.StartupPath + "\\Service\\NtService.exe";

        /// <summary>
        /// Importação da DLL para deixar uma FORM com foco
        /// </summary>
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        /// <summary>
        /// Remover o foco da FORM
        /// </summary>
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        /// <summary>
        /// Saber se o desligando está pendente, para que o antivírus saia, se sim
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// Configurações de FORM, verificar o status dela
        /// </summary>
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        /// <summary>
        /// Colocar uma FORM com foco
        /// </summary>
        private void BringMainWindowToFront(string nomeProcesso)
        {
            try
            {
                // Processo pra por o foco
                Process processo = Process.GetProcessesByName(nomeProcesso).FirstOrDefault();

                // Verifique se o processo está aberto
                if (processo != null)
                {
                    // Verifique se está minimizado / maximizado
                    if (processo.MainWindowHandle == IntPtr.Zero)
                    {
                        // Está minizado, ou não encontramos, vamos mostra-ló
                        ShowWindow(processo.Handle, ShowWindowEnum.Restore);
                    }

                    // Agora, coloque o foco na FORM
                    SetForegroundWindow(processo.MainWindowHandle);
                }
                else
                {
                    // Se o processo não estiver executando, execute
                    Process.Start(nomeProcesso);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Mutex para verificar se o Nottext Antivirus já está aberto
        /// </summary>
        static Mutex singleton = new Mutex(true, "Nottext Antivirus");

        /// <summary>
        /// Função que verifica se o aplicativo já está aberto
        /// </summary>
        public void Aberto()
        {
            try
            {
                // Verifique se está aberto
                if (!singleton.WaitOne(TimeSpan.Zero, true))
                {
                    // Se estiver, coloque o foco nesta FORM
                    BringMainWindowToFront("Nottext Antivirus");

                    // Saia do aplicativo
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                // Sai do aplicativo
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Verifique se o programa foi executado como administrador
        /// </summary>
        private void Administrador()
        {
            // Novo indetificador
            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            // Principal
            var wp = new WindowsPrincipal(wi);

            // Verifique se está rodando como administrador
            bool runAsAdmin = wp.IsInRole(
                WindowsBuiltInRole.Administrator // Administrador
            );

            // Se não estiver como administrador
            if (!runAsAdmin)
            {
                // Novo ProcessStartInfo
                ProcessStartInfo processInfo = new ProcessStartInfo(
                    // Local do antivírus
                    Assembly.GetExecutingAssembly().Location
                );

                // Configure para executar como administrador
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                try
                {
                    // Inicie novamente, mas com direitos adminstrativos
                    Process.Start(processInfo);

                    // Saia do aplicativo
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                    Environment.Exit(0); // Saia do aplicativo
                }

                // Saia do aplicativo
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Atualizações de banco de dados automaticamente
        /// </summary>
        private void AtualizarBancoDeDadosAutomaticamente()
        {
            // Novo thread, para ficar em um loop
            new Thread(async () =>
            {
                // Repetição infinita
                while (true)
                {
                    // 2,25 horas, tempo de espera para atualizar o banco de dados
                    int duasHoras = 8100000;

                    // Espere o tempo
                    await Task.Delay(duasHoras);

                    // Se estiver na versão de testes
                    if (versaoTeste == true)
                        return; // Não execute nada

                    // Se as atualizações automaticas estiverem ligadas
                    if (!File.Exists(Global.Strings.bancoAutomatico))
                    {
                        // Atualizações automatica ligada, depois que
                        // Esperamos o tempo, agora, atualize o banco de dados

                        // Atualize o banco de dados
                        AtualizarBanco.AtualizarBancoDeDados(false);
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Verifica a prioridade do programa
        /// </summary>
        private async void Prioridade()
        {
            // Tempo
            await Task.Delay(1);

            try
            {
                // Processo atual
                Process processo = Process.GetCurrentProcess();

                // Obtenha a prioridade, pois o driver de kernel, pode
                // Muda-ló para "Baixa prioridade"
                processo.PriorityClass = ProcessPriorityClass.RealTime;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Verifica se um arquivo está bloqueado
        /// </summary>
        private bool ArquivoBloqueado(string arquivo)
        {
            try
            {
                // Abra o arquivo
                using (Stream stream = new FileStream(arquivo, FileMode.Open))
                {
                    // Feche-o depois
                    stream.Close();

                    // O arquivo não está em uso, pois conseguimos abrir
                    return true;
                }
            }
            catch
            {
                // Arquivo em uso, falhou ao tentar abrir o arquivo
            }

            // Arquivo bloqueado, pois não conseguimos abrir
            return false;
        }

        /// <summary>
        /// Continuar a instalação de drivers de kernel após a reinicialização
        /// </summary>
        private void InstalarDrivers()
        {
            try
            {
                // Se o arquivo existir, significa que ele decidiu instalar o driver
                if (File.Exists("C:\\Windows\\RESTARTREQUIRED.set"))
                {
                    // Novo KernelModeInstaller
                    KernelModeInstaller kernelMode = new KernelModeInstaller();

                    // Mostre a FORM
                    kernelMode.ShowDialog();
                    form1.HabilitarKernel(kernelMode.installed);
                    form6.HabilitarKernel(kernelMode.installed);
                }
            }
            catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Essa função fará o trabalho de ler o que o kernel mode reportou
        /// É mostrar na mensagem para o usuário
        /// </summary>
        private async Task AntiRansomwareNotificacao()
        {
            try
            {
                // Intervalo
                await Task.Delay(1);

                // Se o arquivo existir, o driver bloqueou algum processo de modificar o arquivo
                if (File.Exists(Global.Strings.ransoBlocked))
                {
                    // Nome do processo que o driver nós passou
                    string processo = File.ReadAllText(Global.Strings.ransoBlocked);
                    string arquivo = File.ReadAllText(Global.Strings.arquivoBlocked);

                    // Mostre uma notificação
                    Global.MostrarNotificacao(
                        // Vamos mostrar esta mensagem
                        "Um processo foi impedido de modificar uma pasta protegida\r\n\r\nProcesso:" + processo + "\r\nArquivo: " + arquivo
                    );

                    try
                    {
                        // Delete o arquivo para não ficar em loop de notificação
                        File.Delete(Global.Strings.ransoBlocked);
                    }
                    catch (Exception) { }

                    try
                    {
                        // Delete o arquivo onde o local do arquivo modificado fica
                        File.Delete(Global.Strings.arquivoBlocked);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Notificações de vulnerabilidades
        /// </summary>
        private async Task VulnerabilitiesNotificacao()
        {
            try
            {
                await Task.Delay(1);

                // Se existir o arquivo de notificação
                if (File.Exists(Global.Strings.notificaoVulnerabilidade))
                {
                    // Notificação
                    string text = File.ReadAllText(Global.Strings.notificaoVulnerabilidade);

                    // Delete o arquivo depois
                    File.Delete(Global.Strings.notificaoVulnerabilidade);

                    // Mostre a notificação
                    Global.MostrarNotificacao(text);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Altera a mensagem da tela
        /// </summary>
        private void AlterarMensagemDaTela(string mensagemNova, string mensagemPrincipal, Bitmap background, string notificacaoTexto)
        {
            // Altere as mensagens
            subStatusLabel.Text = mensagemPrincipal;
            statusLabel.Text = mensagemNova;
            statusImage.BackgroundImage = background;
            notify.Text = notificacaoTexto;

            // Se for algo pra se preocupar
            if (mensagemNova == "A proteção está desabilitada" || mensagemNova == "Configuração incorreta")
            {
                // Mostre o botão
                BeginInvoke((Action)delegate ()
                {
                    resolveButton.Visible = true;
                    resolveButton.Enabled = true;

                    loading.Visible = false;
                });
            } else
            {
                // Mostre o botão
                BeginInvoke((Action)delegate ()
                {
                    resolveButton.Visible = false;
                    resolveButton.Enabled = true;

                    loading.Visible = false;
                });
            }
        }

        /// <summary>
        /// Detecta configurações que deixam o Nottext Antivirus mais fraco
        /// </summary>
        private async Task ConfiguracoesIncorretas()
        {
            await Task.Delay(1);
            var imagem = Resources.cloud_computing_warning;

            try
            {
                if (!Global.ProtecaoHabilitada())
                {
                    AlterarMensagemDaTela("A proteção está desabilitada", "A proteção está desabilitada, deixando o seu computador em risco", Resources.cloud_computing_danger, "A proteção está desabilitada.");
                }
                else if (form6.selfProtect.Enabled == true && form6.selfProtect.Checked == false)
                {
                    if (form1.StatusKernel() == true)
                    {
                        AlterarMensagemDaTela("Configuração incorreta", "A auto-proteção está desabilitada", imagem, "Algo precisa ser corrigido");
                    }
                }

                else if (form6.autoStart.Checked == false)
                {
                    AlterarMensagemDaTela("Configuração incorreta", "A execução automatica está desabilitada", imagem, "Algo precisa ser corrigido");
                }

                else if (form1.StatusLeituraProfunda() == false)
                {
                    AlterarMensagemDaTela("Configuração incorreta", "A leitura profunda está desabilitada", imagem, "Algo precisa ser corrigido");
                }

                else if (form5.vulnerability.Checked == false)
                {
                    AlterarMensagemDaTela("Configuração incorreta", "A proteção contra vulnerabilidades está desabilitada", imagem, "Algo precisa ser corrigido");
                }
                else
                {
                    AlterarMensagemDaTela("A proteção está habilitada", "O Nottext Antivirus está monitorando o seu computador", Resources.cloud_computing, "A proteção está habilitada");
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Avisa sobre as notificações
        /// </summary>
        private void Notificacoes()
        {
            // Novo thread, pra ficar em loop
            new Thread(async () =>
            {
                // Repetição infinita
                while (true)
                {
                    // Se o icone de notificação não estiver visível
                    if (notify.Visible == false)
                    {
                        // Torne-o visível
                        notify.Visible = true;
                    }

                    // Vamos verificar se há alguma notificação do anti-ransomware
                    await AntiRansomwareNotificacao();
                    await VulnerabilitiesNotificacao();

                    try
                    {
                        // Espere 10 segundos
                        await Task.Delay(10000);

                        // Texto de notificação, se falhar, vai sair desta função
                        // E não continuará (porque o arquivo não existe)
                        string txtNotification = File.ReadAllText(Global.Strings.notificar);

                        // Aumente o valor
                        Global.Strings.virusEncontrados++;

                        // Atualize a listBox da quarentena
                        form2.AtualizarQuarentena();

                        // Mostra notificação
                        Global.MostrarNotificacao(txtNotification);

                        // Agora, delete o arquivo, para não ficar em loop
                        File.Delete(Global.Strings.notificar);
                    }
                    catch (Exception) { }
                }
            }).Start();
        }

        /// <summary>
        /// Verifica infinitamente se o serviço está sendo executado, ou o status de proteção
        /// </summary>
        private void StatusProtecao()
        {
            // Novo thread em background
            new Thread(async () =>
            {
                // Novo thread em background
                Thread.CurrentThread.IsBackground = true;

                // Repetição infinita
                while (true)
                {
                    // Intervalo
                    await Task.Delay(8000);
                    await ConfiguracoesIncorretas();

                    try
                    {
                        // Se não estiver nas versões de testes
                        if (versaoTeste == false)
                        {
                            // Se o serviço não estiver executando
                            if (ArquivoBloqueado(antivirusService))
                            {
                                // Saia do aplicativo
                                Environment.Exit(0);
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }).Start();
        }

        /// <summary>
        /// Configura as FORMS
        /// </summary>
        private void ConfigurarForm()
        {
            // Protections
            form1.TopLevel = false;
            items.Controls.Add(form1);

            // Quarentine
            form2.TopLevel = false;
            items.Controls.Add(form2);

            // virusScan
            form3.TopLevel = false;
            items.Controls.Add(form3);

            // Database
            form4.TopLevel = false;
            items.Controls.Add(form4);

            // Windows protection
            form5.TopLevel = false;
            items.Controls.Add(form5);

            // Settings
            form6.TopLevel = false;
            items.Controls.Add(form6);
        }

        /// <summary>
        /// Inicia o serviço do antivírus
        /// </summary>
        private void IniciarServico()
        {
            // Tente iniciar o processo
            try
            {
                // Novo serviço
                ServiceController antivirus = new ServiceController(
                    // Serviço do antivírus
                    "Nottext Antivirus"
                );

                // Inicie o serviço do antivirus
                antivirus.Start();
            }
            // Se falhar
            catch (Exception)
            {
            }

            try
            {
                // Serviço de vulnerabilidades
                ServiceController vulnerabilidades = new ServiceController("Nottext Antivirus Vulnerabilites Search");

                vulnerabilidades.Start();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Form1()
        {
            // Permita thread acessarem outros threads
            CheckForIllegalCrossThreadCalls = false;

            // Verifique se estamos executando como administrador
            Administrador();

            // Checke as funções mais importantes
            Aberto();

            // Caso a instação seja iniciada
            InstalarDrivers();

            // Inicie as forms
            InitializeComponent();

            // Aplique sombras nas bordas
            ShadowForm.ApplyShadows(this);

            // Configure as outras FORMS
            ConfigurarForm();

            // Atualize os bancos de dados automaticamente
            AtualizarBancoDeDadosAutomaticamente();

            // Configure a priodiade do processo
            Prioridade();

            // Inicie o serviço de proteção
            IniciarServico();

            // Notificações
            Notificacoes();

            // Verifique o status das proteções
            StatusProtecao();

            // Configure o cursor nos controles
            Global.SetHandCursor(items);
            Global.SetHandCursor(header);

            // Se estiver na versão de testes
            if (versaoTeste == true)
            {
                // Altere o titulo
                title.Text = "Nottext Antivirus - Versão de TESTES";
            }
        }

        ///
        /// =======================================
        /// 
        /// Apartir daqui pra baixo, só contém função de cliques
        /// De eventos e etc
        /// 
        /// =======================================
        ///

        /// <summary>
        /// Botão de proteções
        /// </summary>
        private void protections_Click(object sender, EventArgs e)
        {
            Effects.Animate(form1, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Botão de configurações
        /// </summary>
        private void settings_Click(object sender, EventArgs e)
        {
            Effects.Animate(form6, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Botão de verificar vírus
        /// </summary>
        private void scan_Click(object sender, EventArgs e)
        {
            Effects.Animate(form3, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Botão de quarentena
        /// </summary>
        private void quarentine_Click(object sender, EventArgs e)
        {
            Effects.Animate(form2, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Botão de banco de dados
        /// </summary>
        private void database_Click(object sender, EventArgs e)
        {
            Effects.Animate(form4, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Botão do Windows
        /// </summary>
        private void windows_Click(object sender, EventArgs e)
        {
            Effects.Animate(form5, Effects.Effect.Slide, Effects.intervalo, 360);
        }

        /// <summary>
        /// Após a FORM estiver sendo fechada
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Desligando
            int SM_SHUTTINGDOWN = 0x2000;

            // Verifique se está sendo desligado
            bool bShutDownPending = GetSystemMetrics(SM_SHUTTINGDOWN) != 0;

            // Se estiver sendo desligado
            if (bShutDownPending == true)
            {
                // Saia
                Environment.Exit(0);
            }

            // Se não, oculte apenas a FORM
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Botão de sair
        /// </summary>
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifique se a senha existe
                if (File.Exists(Global.Strings.arquivoSenha))
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo sair do Nottext Antivirus?") == false)
                    {
                        // Pare
                        return;
                    }
                }

                // Inicie o processo de sair
                Process.Start(Application.StartupPath + "\\Exit.exe");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Botão de abrir
        /// </summary>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Novo thread
            new Thread(async () =>
            {
                await Global.Styles.Iniciar(this);
                Show();
            }).Start();
        }

        /// <summary>
        /// Dois click no icone
        /// </summary>
        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Novo thread
            new Thread(async () =>
            {
                await Global.Styles.Iniciar(this);
                Show();
            }).Start();
        }

        /// <summary>
        /// Habilitar proteção
        /// </summary>
        private void habilitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete o arquivo
                File.Delete(Global.Strings.protecaoTempoReal);
                form1.HabilitarTempoReal(true);
            }
            catch (Exception ex)
            {
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Desabilitar proteção
        /// </summary>
        private void desabilitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifique se a senha existe
                if (File.Exists(Global.Strings.arquivoSenha))
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo desabilitar a proteção em tempo real?") == false)
                    {
                        // Pare
                        return;
                    }
                }

                // Crie o arquivo
                File.Create(Global.Strings.protecaoTempoReal).Close();
                form1.HabilitarTempoReal(false);

            }
            catch (Exception ex)
            {
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Sobre
        /// </summary>
        private void aboutBtn_Click(object sender, EventArgs e)
        {
            // FORM de sobre
            About sobre = new About();
            sobre.ShowDialog();
        }

        /// <summary>
        /// Protocolos de conexcões
        /// </summary>
        public class Protocols
        {
            public const SecurityProtocolType
                protocol_SystemDefault = 0,
                protocol_Ssl3 = (SecurityProtocolType)48,
                protocol_Tls = (SecurityProtocolType)192,
                protocol_Tls11 = (SecurityProtocolType)768,
                protocol_Tls12 = (SecurityProtocolType)3072;
        }

        /// <summary>
        /// Após o aplicativo ser carregado
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Efeito
                await Global.Styles.Iniciar(this);

                // Configure o protocolo para fazer conecxões
                ServicePointManager.SecurityProtocol = Protocols.protocol_Tls11 | Protocols.protocol_Tls12;

                // Verifique se há novas atualizações para o aplicativo
                AtualizarBanco.AtualizarBancoDeDados(!versaoTeste);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            await Global.Styles.Fechar(this);
            Hide();
        }

        /// <summary>
        /// Botão de minimizar
        /// </summary>
        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void ativarEfeito1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Guna.UI2.WinForms.Guna2TileButton button = (Guna.UI2.WinForms.Guna2TileButton)sender;
                Global.Styles.GunaButtonEfeitoAtivar(button);
            } catch (Exception)
            {
                Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
                Global.Styles.GunaButtonEfeitoAtivar(button);
            }
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void desativarEfeito1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Guna.UI2.WinForms.Guna2TileButton button = (Guna.UI2.WinForms.Guna2TileButton)sender;
                Global.Styles.GunaButtonEfeitoDesativar(button);
            } catch (Exception)
            {
                Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
                Global.Styles.GunaButtonEfeitoDesativar(button);
            }
        }

        /// <summary>
        /// Botão de resolver as coisas
        /// </summary>
        private void resolveButton_Click(object sender, EventArgs e)
        {
            resolveButton.Enabled = false;
            loading.Visible = true;

            // Texto
            string st = subStatusLabel.Text.ToLower();

            try
            {
                // Proteção em tempo real
                if (st == "a proteção está desabilitada, deixando o seu computador em risco")
                {
                    // Delete o arquivo e habilite a checkbox
                    File.Delete(Global.Strings.configuracoes + "realTime.set");
                    form1.HabilitarTempoReal(true);
                }

                // Proteção de leitura profunda
                if (st == "a leitura profunda está desabilitada")
                {
                    // Delete o arquivo e habilite a checkbox
                    File.Delete(Global.Strings.configuracoes + "advancedScan.set");
                    form1.HabilitarLeitura(true);
                }

                // Proteção contra vulnerabilidades
                else if (st == "a proteção contra vulnerabilidades está desabilitada")
                {
                    // Marque e clique
                    form5.vulnerability.Checked = true;
                    form5.vulnerability_Click(new object(), new EventArgs());
                }

                // Proteção se auto-defesa
                else if (st == "a auto-proteção está desabilitada")
                {
                    // Marque e clique
                    form6.selfProtect.Checked = true;
                    form6.selfProtect_Click(new object(), new EventArgs());
                }

                // Auto iniciar
                else if (st == "a execução automatica está desabilitada")
                {
                    // Marque e clique
                    form6.autoStart.Checked = true;
                    form6.autoStart_Click(new object(), new EventArgs());
                }

            } catch (Exception) { }
        }
    }
}


