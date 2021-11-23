using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus_Uinstall
{
    public partial class Form1 : Form
    {
        // Arquivo de senha
        string passwordFile = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\password.set";

        // Senha usada na função de criptografia
        static string passwordForEncrypt = "cryptographyForPassword";
        bool senhaRequerida = false;

        /// <summary>
        /// Verifique se o programa foi executado como administrador
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
        /// Inicia tudo
        /// </summary>
        public Form1()
        {
            // Verifique se é administrador
            Administrador();

            InitializeComponent();

            if (File.Exists(passwordFile))
            {
                passwordText.Visible = true;
                senhaRequerida = true;
                label3.Text += "\r\nDigite sua senha para continuar:";
            }

            ShadowForm.ApplyShadows(this);

            DllCursor.SetHandCursor(panel1);
        }

        /// <summary>
        /// Inicia um processo
        /// </summary>
        private async Task IniciarProcesso(string arquivo, string argumento, bool esperarSair)
        {
            try
            {
                // Inicia um processo
                await Task.Delay(1); // Intervalo
                Process processo = new Process();
                processo.StartInfo.FileName = arquivo;
                processo.StartInfo.Arguments = argumento;

                // Inicia o processo em segundo plano
                processo.StartInfo.Verb = "runas";
                processo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processo.Start();

                // Só espere sair se for para sair
                if (esperarSair == true)
                    processo.WaitForExit(); // Espere sair
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Para o serviço de proteção
        /// </summary>
        private async Task DeletarServico()
        {
            try
            {
                try
                {
                    // Filtro
                    ServiceController kernel = new ServiceController("Nottext Antivirus Driver Kernel");

                    // Pare o filtro
                    kernel.Stop();
                } catch (Exception) { }
            
                // Deleta o serviço
                await IniciarProcesso("sc.exe", "delete " + '"' + "Nottext Antivirus" + '"', true);
                await IniciarProcesso("sc.exe", "delete " + '"' + "Nottext Antivirus Vulnerabilites Search" + '"', true);
                await IniciarProcesso("sc.exe", "delete " + '"' + "Nottext Antivirus Driver Kernel" + '"', true);

                // Parar o serviço
                File.Delete("C:\\Windows\\System32\\Drivers\\FsFilter.sys");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Flags
        /// </summary>
        [Flags]
        internal enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 1,
            MOVEFILE_COPY_ALLOWED = 2,
            MOVEFILE_DELAY_UNTIL_REBOOT = 4,
            MOVEFILE_WRITE_THROUGH = 8
        }

        /// <summary>
        /// Configurções para deletar após reinicialização
        /// </summary>
        [DllImport("kernel32.dll", EntryPoint = "MoveFileEx")]
        internal static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName,
        MoveFileFlags dwFlags);

        /// <summary>
        /// Deleta os arquivos na reinicialização
        /// </summary>
        private async Task DeletarReiniciar(string pasta)
        {
            try
            {
                MoveFileEx("C:\\Windows\\System32\\Drivers\\FsFilter.sys", null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                await Task.Delay(1); // Intervalo

                // Deleta os arquivos na reinicialização
                string[] pastas = Directory.GetFiles(pasta, "*.*", SearchOption.AllDirectories);

                // Procura todos os arquivos
                foreach (string arquivos in pastas)
                {
                    try
                    {
                        // Deleta os arquivos na proxima reinicialização
                        MoveFileEx(arquivos, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
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
        /// Botão de desinstalar
        /// </summary>
        private async Task remover()
        {
            try
            {
                // Verifica se o processo foi finalizado
                if (Process.GetProcessesByName("Nottext Antivirus").Length > 0 || Process.GetProcessesByName("NottextAnt").Length > 0)
                {
                    // Mensagem
                    MessageBox.Show("Você precisa sair do Nottext Antivirus antes de desinstalar o mesmo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Sai do aplicativo
                    Environment.Exit(0);
                }

                // Deleta o serviço de proteção é as chaves no registro
                await DeletarServico();

                // Necessários
                Necessarios necessarios = new Necessarios();
                await necessarios.DesinstalarRegistro();

                // Deleta o agendador de tarefas
                await IniciarProcesso("schtasks.exe", "/delete /tn Nottext /f", true);

                // Deleta os arquivos
                await DeletarReiniciar(Application.StartupPath);
                await DeletarReiniciar("C:\\Windows\\UNREBOOT.set");

                try
                {
                    Directory.Delete("C:\\ProgramData\\Nottext\\Nottext Antivirus", true);
                } catch (Exception) { }

                // Deleta o serviço
                await DeletarServico();

                // Saia do modo de teste
                await IniciarProcesso("bcdedit.exe", "/set testsigning off", true);
            }
            catch (Exception ex)
            {
                // Mensagem de erro
                MessageBox.Show(ex.Message, "Desinstalador", MessageBoxButtons.OK, MessageBoxIcon.Error);

                await Styles.Fechar(this);
                Environment.Exit(0); // Sai do aplicativo
            }
        }

        /// <summary>
        /// Verifica se a senha está correta
        /// </summary>
        private bool SenhaCerta(string text)
        {
            try
            {
                // Senha criptografado
                string criptografado = EncryptString(text);

                // Se for o mesmo texto do arquivo
                if (criptografado == File.ReadAllText(passwordFile))
                {
                    // Verdade
                    return true;
                }
            }
            catch (Exception) { }

            // Falso
            return false;
        }

        /// <summary>
        /// Criptografa uma string
        /// </summary>
        private string EncryptString(string text)
        {
            // Texto criptografado
            string encryptedText = "";

            // Procure todas as caracterias na senha
            foreach (char chars in passwordForEncrypt)
            {
                // Letra
                string c = chars.ToString();

                // Faça uma "criptografia" básicona
                encryptedText +=

                    // Substitua algumas caracterias
                    text.Replace(c, "Aa9d3i" + c + "3t4s22F" + c + "21dF3g4" + c + c)

                   // Outras caracterias
                   .Replace("a", "32FK3").Replace("1", "3radf4").Replace("d", "asd32rjiID")

                   // Outras caracterias
                   .Replace("e", "fgk3J").Replace("3", "f239kf3").Replace("5", "6lhg4")

                   // Outras caracterias
                   .Replace("g", "fk5k3").Replace("i", "lkh4").Replace("u", "u2").Replace("o", "5");
            }

            // Retorne a string criptografada
            return encryptedText;
        }

        /// <summary>
        /// Botão de desinstalar
        /// </summary>
        private async void desinstalar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se for necessário uma senha
                if (senhaRequerida == true)
                {
                    // Se falhar
                    if (SenhaCerta(passwordText.Text) == false)
                    {
                        MessageBox.Show("Senha incorreta, tente novamente.", "Nottext Antivirus", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Saia
                        Environment.Exit(0);
                    }
                }

                // Desabilite os botões, e desabilite o antivírus
                close.Enabled = false;
                close.Visible = false;

                desinstalar.Enabled = false;

                // Remova o aplicativo
                await remover();

                try
                {
                    // Crie o arquivo que é necessário reiniciar para que o programa
                    // Não seja instalado novamente
                    File.Create("C:\\Windows\\UNREBOOT.set");

                    // Delete após a reinicialização
                    MoveFileEx("C:\\Windows\\UNREBOOT.set", null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                catch (Exception) { }

                // Saia do modo de testes
                await IniciarProcesso("bcdedit.exe", "-set testsigning off", true);

                
            } catch (Exception ex)
            {
                // Se falhar
                MessageBox.Show(ex.Message, "Nottext Antivirus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await Styles.Fechar(this);

            // Saia
            Environment.Exit(0);
        }

        /// <summary>
        /// Quando estiver fechando
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se o botão estiver desabilitado
            if (close.Enabled == false)
                e.Cancel = true; // Não feche a FORM
        }

        /// <summary>
        /// Feche a FORM
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            await Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Botão de minimizar
        /// </summary>
        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Iniciado
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