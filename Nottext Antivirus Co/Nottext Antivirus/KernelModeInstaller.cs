///
/// Instalação dos drivers de kernel
///

using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class KernelModeInstaller : Form
    {
        // Novo do driver de kernel
        string kernelDriverName = "Nottext Antivirus Driver Kernel";

        /// <summary>
        /// Inicia
        /// </summary>
        public KernelModeInstaller()
        {
            InitializeComponent();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);
        }

        /// <summary>
        /// Matar os processos
        /// </summary>
        private async Task MatarProcessos(string processos)
        {
            try
            {
                // Procure todos os processo com o nome que nós foi passado
                foreach (Process processo in Process.GetProcessesByName(processos))
                {
                    try
                    {
                        // Mate o processo
                        processo.Kill();

                        // Aguarde
                        await Task.Delay(100);
                    } catch (Exception) { }
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Altera as checkboxs da FORM FileProtections quando o driver
        /// De kernel é instalado
        /// </summary>
        public void ChangeCheckbox(FileProtection fl)
        {
            // Altere o checkbox
            fl.kernelProtection.Checked = true;
            fl.restart.Visible = false;
            fl.label6.Visible = false;
            fl.antiRansomware.Enabled = true;
        }

        // Bool para saber se a instalação foi concluida no Form1.cs
        public bool installed = false;

        /// <summary>
        /// Botão de instalar
        /// </summary>
        private async void install_Click(object sender, EventArgs e)
        {
            try
            {
                // Mate o processo de serviço, porque ele precisará ser reiniciado
                // Para se conectar ao driver
                await MatarProcessos("Nottext Antivirus");

                // Pasta de instalação
                string pastaInstalar = Application.StartupPath + "\\INF\\";

                await Task.Delay(1); // Intervalo

                // Arquivo de INF, onde os drivers estão
                string arquivoInf = pastaInstalar + "X86\\Mini-Filter.inf";

                // Se for 64bits
                if (Environment.Is64BitProcess)
                {
                    // Altere o local
                    arquivoInf = pastaInstalar + "X64\\Mini-Filter.inf";
                }

                // Instale o driver de kernel
                await Global.IniciarProcesso(
                    "cmd.exe", // CMD

                    // Instale
                    "/c C:\\Windows\\System32\\InfDefaultInstall.exe " +'"' + arquivoInf + '"'
                );

                // Altere o valor para o Form1.cs saber que o driver foi instalado
                installed = true;

                // Delete o arquivo, para que a FORM não apareca toda vez que
                // O antivírus é iniciado
                File.Delete("C:\\Windows\\RESTARTREQUIRED.set");

                // Driver de kernel
                ServiceController filter = new ServiceController(kernelDriverName);

                // Inicie o driver
                filter.Start();

                // Feche a FORM
                Close();

                // Reinicie o aplicativo e sai
                Application.Restart();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                // FORM de erro
                Alert form = new Alert(ex.Message, false);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void ativarEfeito1_MouseHover(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            Global.Styles.GunaButtonEfeitoAtivar(button);
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void desativarEfeito1_MouseLeave(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            Global.Styles.GunaButtonEfeitoDesativar(button);
        }
    }
}
