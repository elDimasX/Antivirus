using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Exit.KeyboardHook;

namespace Exit
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Váriaveis para colocar a FORM sempre no topo
        /// </summary>
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        /// <summary>
        /// Coloca a FORM no topo
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Inicializar a FORM
        /// </summary>
        private async Task Inicializar()
        {
            await Task.Delay(500); // Intervalo

            // Mostrar a FORM
            Confirm form = new Confirm();
            form.ShowDialog();
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
        /// Função que procura todos os processos filhos de um processo
        /// </summary>
        private static int ProcessAndChildren(
            int pid // PID do processo para escanear
        )
        {
            // Valor para retornar
            int child = 0;

            // Obtenha todos os processos do PID
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                // Código
                "Select * From Win32_Process Where ParentProcessID=" + pid
            );

            // Obtenha todos os valores
            ManagementObjectCollection moc = searcher.Get();

            // Agora, procure valor por valor
            foreach (ManagementObject mo in moc)
            {
                // Processo filho dele
                child = Convert.ToInt32(mo["ProcessID"]);
            }

            // Retorne o processo filho
            return child;
        }

        /// <summary>
        /// Verifica se foi iniciado pelo NottextAnt.exe
        /// </summary>
        private static void StartedFromNottext()
        {
            try
            {
                // Processo do explorer
                Process nottext = Process.GetProcessesByName("NottextAnt")[0];

                string local = nottext.MainModule.FileName.ToLower();

                // ProgramFiles
                string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).ToLower();

                // Se não for o arquivo original
                if (
                    local == programFiles + "\\Nottext\\Antivirus\\NottextAnt.exe".ToLower() ||
                    local == "C:\\Windows\\System32\\NottextAnt.exe".ToLower()
                )
                {
                    // Agora, verifique os processo filhos dele
                    int pid = ProcessAndChildren(nottext.Id);

                    // Se o processo filho for diferente do processo atual
                    if (pid != Process.GetCurrentProcess().Id)
                    {
                        Environment.Exit(0);
                    }
                } else
                {
                    // Saia
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                // Saia
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        private async void iniciarTudo()
        {
            // Administrador
            Administrador();

            // Verifique se foi iniciado pelo Nottext
            StartedFromNottext();

            // Inicia a FORM
            InitializeComponent();

            // Sete a FORM no topo
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            // Gancho
            SetHook(_proc);

            // Espere
            await Inicializar();

            // Saia, a FORM já foi fechada
            Environment.Exit(0);
        }

        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            iniciarTudo();
        }

        /// <summary>
        /// Esta função vai criar um sombreamento nas bordas da
        /// FORM
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                // Sombra
                const int CP = 0x00020000;

                // A FORM
                CreateParams cp = base.CreateParams;

                // Configure o estilo
                cp.ClassStyle |= CP;

                // Aplique o estilo
                return cp;
            }
        }

        /// <summary>
        /// Tecla
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancele
            e.Cancel = true;
        }
    }
}
