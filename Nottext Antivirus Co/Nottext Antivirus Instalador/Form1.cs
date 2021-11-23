///
/// Instalador para o Nottext Antivirus
///

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shell32;
using static System.Environment;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace Nottext_Antivirus_Instalador
{
    public partial class Form1 : Form
    {
        // Pasta onde ficará os arquivos
        string pastaInstalar = GetFolderPath(SpecialFolder.ProgramFilesX86) + "\\Nottext\\Antivirus";

        // Indica se instalação foi tentada, e se falhou
        bool jaTentou = false;

        /// <summary>
        /// Checkar se o aplicativo já esta aberto
        /// </summary>
        static Mutex singleton = new Mutex(true, "Nottext Antivirus Instalador");
        public void Aberto()
        {
            try
            {
                if (!singleton.WaitOne(TimeSpan.Zero, true))
                {
                    // O aplicativo com o nome '3D AntiVirus' já esta aberto
                    MessageBox.Show("O aplicativo já está em execução, para iniciar um novo, por favor, feche o primeiro aplicativo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); // Mensagem

                    // Sai do aplicativo
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
        /// Checkar se o programa foi executado como administrador
        /// </summary>
        private void Administrador()
        {
            // Novo indetificador
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            // Checkar se está rodando como administrador
            bool runAsAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);

            // Se não estiver como administrador
            if (!runAsAdmin)
            {
                // Processo
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().Location);

                // Execute como administrador
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                try
                {
                    // Inicie novamente, mas com direitos adminstrativos
                    Process.Start(processInfo);
                    Environment.Exit(0); // Sai do aplicativo
                }
                catch (Exception)
                {
                    Environment.Exit(0); // Sai do aplicativo
                }

                // Saia do aplicativo
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Auto se copia para iniciar na proxima reinicialização
        /// </summary>
        private async void AutoCopiar()
        {
            // Pasta
            string instalador = Path.GetTempPath() + "Nottext AntiVirus";

            // Caso o aplicativo não esteja no computador do usuário
            if (Application.StartupPath != instalador)
            {
                try
                {
                    // Deleta a pasta
                    Directory.Delete(instalador, true);
                    await Task.Delay(2000);
                }
                catch (Exception) { }

                // Informações das pastas
                DirectoryInfo diretorio = new DirectoryInfo(Application.StartupPath);
                DirectoryInfo diretorioColar = new DirectoryInfo(instalador);

                // Copie as pastas
                CopiarPasta(diretorio, diretorioColar, true);
            }
        }

        /// <summary>
        /// Instala uma fonte
        /// </summary>
        private static void InstalarFonte(string fonte)
        {
            try
            {
                string Win = GetFolderPath(SpecialFolder.Windows) + "\\";

                // Se o arquivo não existir
                if (!File.Exists(Win + "Fonts\\" + Path.GetFileName(fonte)))
                {
                    // Shell32
                    var Shell32 = Type.GetTypeFromProgID("Shell.Application");

                    // Nova instancia
                    var oShell = Activator.CreateInstance(Shell32);

                    // Pasta de destino
                    var destinationFolder = (Folder)Shell32.InvokeMember("NameSpace",
                        BindingFlags.InvokeMethod,
                        null, oShell,
                        new object[] { "C:\\Windows\\Fonts" }
                    );

                    // Copie os arquivos
                    destinationFolder.CopyHere(fonte, 4 | 16);
                }
            } catch (Exception ex)
            {
                // MessageBox
                MessageBox.Show("Ocorreu um erro na instalação das fontes do programa, sentimos muito, mas não podemos continuar com a operação...\r\n\r\nErro:" + ex.Message, "Nottext Antivirus", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Exit(0);
            }
        }

        /// <summary>
        /// Instala a fonte para que a interface funcione como deveria
        /// </summary>
        private static void InstalarFontes()
        {
            // Procure todas as fontes
            foreach (string fonte in Directory.GetFiles(Application.StartupPath + "\\Fonts"))
            {
                // Instale as fontes
                InstalarFonte(fonte);

                // Se a fonte não for instalada
                if (!File.Exists(Application.StartupPath + "\\Fonts\\" + Path.GetFileName(fonte)))
                {
                    // MessageBox
                    MessageBox.Show("Ocorreu um erro na instalação das fontes do programa, sentimos muito, mas não podemos continuar com a operação.", "Nottext Antivirus", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Exit(0);
                }
            }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        private async void IniciarTudo()
        {
            Administrador();
            InstalarFontes();
            AutoCopiar();


            InitializeComponent();
            Opacity = 0;
            ShadowForm.ApplyShadows(this);

            DllCursor.SetHandCursor(this);

            try
            {
                if (Process.GetProcessesByName("NottextAnt").Length > 0)
                {
                    await Task.Delay(100);

                    try
                    {
                        Hide();
                    } catch (Exception) { }
                    
                    await InstalarNottextAntivirus();
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Form1()
        {
            IniciarTudo();
        }

        /// <summary>
        /// Copiar pastas
        /// </summary>
        public void CopiarPasta(DirectoryInfo pasta, DirectoryInfo copiarPara, bool permitir)
        {
            try
            {
                // Pega as outras pastas
                foreach (DirectoryInfo dir in pasta.GetDirectories())
                {
                    // Copia novamente
                    CopiarPasta(dir, copiarPara.CreateSubdirectory(dir.Name), false);
                }

                // Pega os arquivos
                foreach (FileInfo file in pasta.GetFiles())
                {
                    // Só copie os arquivos, caso ele seja diferente dessas váriarentes
                    string nome = Path.GetFileName(file.FullName);

                    string instalador = Path.GetFileName(Application.ExecutablePath);

                    if (permitir == false)
                    {
                        // Checke o nome de todos os arquivos antes de copiar
                        if (file.Name != instalador && nome != "database.ini" && nome != "Nottext.pfx")
                        {
                            // Copia os arquivos
                            file.CopyTo(Path.Combine(copiarPara.FullName, file.Name));
                        }
                    } else
                    {
                        // Copia os arquivos
                        file.CopyTo(Path.Combine(copiarPara.FullName, file.Name));
                    }
                }
            } catch (Exception){ }
        }

        /// <summary>
        /// Copiar arquivo
        /// </summary>
        private void CopiarArquivo(string arquivo, string local)
        {
            try
            {
                // Copia o arquivo
                File.Copy(arquivo, local);
            } catch (Exception) { }
        }

        /// <summary>
        /// Inicia um processo
        /// </summary>
        private async Task IniciarProcesso(string arquivo, string argumento, bool hide)
        {
            try
            {
                // Inicia um processo
                Process processo = new Process();
                processo.StartInfo.FileName = arquivo;
                processo.StartInfo.Arguments = argumento;

                await Task.Delay(1); // Intervalo

                // Inicia o processo em segundo plano
                processo.StartInfo.Verb = "runas";

                // Se for para ocultar
                if (hide == true)
                {
                    processo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }

                processo.Start();
                processo.WaitForExit();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Instala o serviço
        /// </summary>
        private async Task InstalarServicoNome(string nome, string local, string iniciar, string tipo)
        {
            // Cria o serviço
            await IniciarProcesso("sc.exe", "create " + '"' + nome + '"' + " binPath= " + '"' + local + '"' + " start= " + iniciar + " type= " + tipo, true);
        }

        /// <summary>
        /// Cria uma pasta usando Try Catch
        /// </summary>
        private void CriarPasta(string folder)
        {
            try
            {
                Directory.CreateDirectory(folder);
            } catch (Exception) { }
        }

        /// <summary>
        /// Instala os serviços
        /// </summary>
        private async Task ConfigurarServicos()
        {
            // Cria o serviço do antivírus
            await InstalarServicoNome("Nottext Antivirus", pastaInstalar + "\\Service\\NtService.exe", "auto", "own");

            // Cria o serviço do antivírus
            await InstalarServicoNome("Nottext Antivirus Vulnerabilites Search", pastaInstalar + "\\Service\\NtVulnerabilities.exe", "demand", "own");
        }

        /// <summary>
        /// Instala o certificado
        /// </summary>
        private async Task InstalarCertificado()
        {
            try
            {
                await Task.Delay(1); // Intervalo

                try
                {
                    // Local do certificato
                    X509Certificate2 certificato = new X509Certificate2(Application.StartupPath + "\\Nottext.pfx");

                    // Instala o certificato
                    using (X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine))
                    {
                        store.Open(OpenFlags.ReadWrite);
                        store.Add(certificato); // Adiciona o certificato
                        store.Close(); // Fecha o certificato
                    }
                }
                catch (Exception) { }

                try
                {
                    // Deleta o arquivo
                    File.Delete(pastaInstalar + "\\Nottext.pfx");
                }
                catch (Exception) { }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Mata todos os processos
        /// </summary>
        public async Task MatarProcessos(string nome)
        {
            try
            {
                await Task.Delay(100);

                // Procure todos os processos do antivírus
                foreach (Process processo in Process.GetProcessesByName(nome))
                {
                    try
                    {
                        // Mate
                        processo.Kill();
                    }
                    catch (Exception) { }
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Checke se a proteção de kernel está instalado
        /// </summary>
        private bool ProtecaoKernel()
        {
            // Retorne true se o serviço estiver instalado
            return ServiceController.GetDevices().Any(serviceController => serviceController.ServiceName.Equals("Nottext Antivirus Driver Kernel"));
        }

        /// <summary>
        /// Botão de instalar
        /// </summary>
        private async Task instalarInf()
        {
            try
            {
                // Pasta de instalação
                string pastaInstalar = Application.StartupPath + "\\INF\\";

                await Task.Delay(1); // Intervalo
                string arquivoInf = pastaInstalar + "X86\\Mini-Filter.inf";

                // Se for X64
                if (Environment.Is64BitProcess)
                {
                    arquivoInf = pastaInstalar + "X64\\Mini-Filter.inf"; // Arquivo de X64
                }

                // Instala o serviço
                await IniciarProcesso("cmd.exe", "/c C:\\Windows\\System32\\InfDefaultInstall.exe " + '"' + arquivoInf + '"', true);

                try
                {
                    // Deleta o arquivo
                    File.Delete("C:\\Windows\\RESTARTREQUIRED.set");
                } catch (Exception) { }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Checke se o driver de kernel está instalado para atualizar depois
        /// </summary>
        private async Task InstalarDriver()
        {
            try
            {
                // Se a proteção estiver instalado
                if (ProtecaoKernel() == true)
                {
                    // Instale o arquivo
                    await instalarInf();
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Instala o Nottext Antivirus
        /// </summary>
        private async Task InstalarNottextAntivirus()
        {
            try
            {
                // Desabilite os botões
                close.Enabled = false;
                close.Visible = false;
                instalar.Enabled = false;

                // Se o arquivo existir
                if (File.Exists("C:\\Windows\\UNREBOOT.set"))
                {
                    // Se o usuário não deseja continuar
                    if (MessageBox.Show("Você acabou de desinstalar o Nottext Antivirus, você deve reiniciar o computador para remover a proteção completamente, ou você deseja instalar agora mesmo?", "Nottext Antivirus", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        // Pare
                        return;
                    }
                }

                try
                {
                    try
                    {
                        // Nome do driver de kernel
                        ServiceController kernel = new ServiceController(
                            "Nottext Antivirus Driver Kernel" // Driver de kernel
                        );

                        kernel.Stop(); // Pare o kernel
                    }
                    catch (Exception) { }

                    // 1 Segundo
                    await Task.Delay(1000);

                    // Mata os processos para permitir
                    // Que copie os novos arquivos pra lá
                    await MatarProcessos("NottextAnt");
                    await MatarProcessos("Nottext Antivirus");
                    await MatarProcessos("NtService");
                    await MatarProcessos("NtVulnerabilities");

                    // 3 segundos
                    await Task.Delay(2000);
                    await MatarProcessos("NottextAnt");
                    await MatarProcessos("Nottext Antivirus");
                    await MatarProcessos("NtService");
                    await MatarProcessos("NtVulnerabilities");
                }
                catch (Exception) { }

                // Delete os serviços anteriores
                await IniciarProcesso("sc.exe", "delete " + '"' + "Nottext Antivirus" + '"', true);
                await IniciarProcesso("sc.exe", "delete " + '"' + "Nottext Antivirus Vulnerabilites Search" + '"', true);

                // Local do arquivo pra bloquear, por algum motivo, ele fica
                // Iniciando sozinho com o antivírus
                RegistryKey block = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\Fondue.exe", true);

                // Bloqueie no registro
                block.SetValue("Debugger", "C:\\", RegistryValueKind.String);

                // Adicione um valor no registro para mostrar os icones (erro que ocorre no Windows 10)
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true);
                key.SetValue("EnableBalloonTips", 1, RegistryValueKind.DWord);

                // Nome do instalador sem o local
                string instalador = Path.GetFileName(Application.ExecutablePath);
                string link = Application.StartupPath + "\\x86.lnk"; // Link para área de trabalho

                // 2 segundos
                await Task.Delay(2000);

                // Vamos tentar novamente apagar o diretorio
                // Para copiar os novos arquivos pra lá 
                if (Directory.Exists(pastaInstalar))
                {
                    try
                    {
                        // Deleta todos os arquivos
                        Directory.Delete(pastaInstalar, true);
                    }
                    catch (Exception) { }
                }

                try
                {
                    // Delete o arquivo de database antigo
                    File.Delete("C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\database.ini");
                }
                catch (Exception) { }

                // Crie a pasta
                CriarPasta("C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs");
                CriarPasta("C:\\ProgramData\\Nottext\\Nottext Antivirus\\Quarentine");

                // Copia o arquivo de dados para a pasta
                CopiarArquivo(Application.StartupPath + "\\database.ini", "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\database.ini");

                // Crie a pasta no local
                CriarPasta(pastaInstalar);

                try
                {
                    // Novas informações da pasta, vamos usar
                    // Para copiar todos os itens pra pasta de local
                    DirectoryInfo dir1 = new DirectoryInfo(Application.StartupPath);
                    DirectoryInfo dir2 = new DirectoryInfo(pastaInstalar);

                    // Copie a nossa pasta pra pasta de instalação
                    CopiarPasta(dir1, dir2, false);

                    // Chama o arquivo para configurar o registro
                    Adicionais adicionais = new Adicionais();
                    await adicionais.Registro();

                    // Criar o link na área de trabalho
                    CopiarArquivo(link, "C:\\Users\\Public\\Desktop\\Nottext Antivirus.lnk");

                    // Criar o link para o menu iniciar
                    CopiarArquivo(link, "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Nottext Antivirus.lnk");

                    // Chama o arquivo para configurar o agendador de tarefas
                    await adicionais.Agendador();

                    // Configurar os serviços
                    await ConfigurarServicos();

                    // Instala o certificado
                    await InstalarCertificado();

                    // Configurar os serviços
                    await ConfigurarServicos();

                    try
                    {
                        // Chave do regedit
                        RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                        // Deleta o valor
                        rkApp.DeleteValue("Nottext AntiVirus Instalador");
                    }
                    catch (Exception) { }

                    await InstalarDriver();

                    // Inicia o AntiVirus
                    Process.Start(pastaInstalar + "\\Nottex Antivirus News.exe");

                    // Fechar
                    await Styles.Fechar(this);

                    // Sai do aplicativo
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    // Se algo der errado, mostre a mensagem
                    MessageBox.Show(

                        // Mensagem
                        ex.Message + "\r\nNúmero do erro: 01",

                        // Titulo
                        "Nottext Antivirus - Instalador",

                        // Botões
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    await Styles.Iniciar(this);
                    Exit(0); // Sai do aplicativo
                }
            }
            catch (Exception ex)
            {
                if (jaTentou == true)
                {
                    // Se algo der errado, mostre a mensagem
                    MessageBox.Show(
                        // Mensagem
                        ex.Message + "\r\nTente desinstalar a versão atual, é depois instalar esta versão.\r\nNúmero do erro: 02",

                        // Titulo
                        "Nottext Antivirus - Instalador",

                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    await Styles.Iniciar(this);
                    Exit(0); // Sai do aplicativo
                } else
                {
                    jaTentou = true;
                    await InstalarNottextAntivirus();
                }
            }
        }

        /// <summary>
        /// Botão de instalar
        /// </summary>
        private async void instalar_Click(object sender, EventArgs e)
        {
            await InstalarNottextAntivirus();
        }

        /// <summary>
        /// Quando a FORM estiver fechando
        /// </summary>
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se a instalação estiver em andamento
            if (close.Enabled == false)
            {
                e.Cancel = true; // Não feche a FORM
                await IniciarProcesso("shutdown.exe", "-a", true);
            }
        }

        /// <summary>
        /// Botão de minimizar
        /// </summary>
        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Botão de sair
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            await Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Operações de estilo
        /// </summary>
        public class Styles
        {
            /// <summary>
            /// Quando a FORM for fechada
            /// </summary>
            public static async Task Fechar(Form form)
            {
                // Opacidade
                form.Opacity = 1;

                // Repetição
                while (true)
                {
                    // Se for menor ou igual á 0
                    if (form.Opacity <= 0)
                    {
                        break;
                    }

                    // Aguarde
                    await Task.Delay(10);
                    form.Opacity -= .1;
                }
            }

            /// <summary>
            /// Quando a FORM for aberta
            /// </summary>
            public static async Task Iniciar(Form form)
            {
                // Opacidade
                form.Opacity = 0;

                // Repetição infinita
                while (true)
                {
                    // Se for maior ou igual á 1
                    if (form.Opacity >= 1)
                    {
                        break;
                    }

                    // Espere
                    await Task.Delay(10);
                    form.Opacity += .1;
                }
            }
        }

        /// <summary>
        /// Quando carregar
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            await Styles.Iniciar(this);
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void ativarEfeito1_MouseHover(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            button.ShadowDecoration.Enabled = true;
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void desativarEfeito1_MouseLeave(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            button.ShadowDecoration.Enabled = false;
        }
    }
}