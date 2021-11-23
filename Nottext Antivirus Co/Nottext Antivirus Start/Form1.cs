using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus_Start
{
    public partial class Form1 : Form
    {
        private void IniciarProcesso(string nome, string argumento)
        {
            try
            {
                ProcessStartInfo processo = new ProcessStartInfo();
                processo.FileName = nome;
                processo.Arguments = argumento;
                processo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(processo);
            }
            catch (Exception) { }
        }

        // Configurações para mover uma FORM para o foco
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        // Configurações iniciais
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        /// <summary>
        /// Mostrar processo
        /// </summary>
        private void BringMainWindowToFront(string nomeProcesso)
        {
            try
            {
                // Pegar o processo
                Process processo = Process.GetProcessesByName(nomeProcesso).FirstOrDefault();

                // Checke se o processo está aberto
                if (processo != null)
                {
                    // Check se está minimizado / maximizado
                    if (processo.MainWindowHandle == IntPtr.Zero)
                    {
                        // Mostre a FORM
                        ShowWindow(processo.Handle, ShowWindowEnum.Restore);
                    }

                    // Foco na FORM
                    SetForegroundWindow(processo.MainWindowHandle);
                }
                else
                {
                    // Se não estiver executando, execute
                    Process.Start(nomeProcesso);
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
        /// Inicie o aplicativo
        /// </summary>
        private async void Iniciar()
        {
            try
            {
                // Se o processo do antivírus já estiver em execução
                if (Process.GetProcessesByName("NottextAnt").Length > 0)
                {
                    BringMainWindowToFront("NottextAnt");
                    Environment.Exit(0);
                }

                await Task.Delay(10000);

                // Inicia o processo
                IniciarProcesso("schtasks.exe", "/run /tn Nottext");
                await Styles.Fechar(this);
                Hide();

                // Espere 5 segundos
                await Task.Delay(5000);

                // Se o processo não tiver sido iniciado
                if (Process.GetProcessesByName("NottextAnt").Length == 0)
                {
                    // Inicie-o
                    Process.Start(Application.StartupPath + "\\NottextAnt.exe");
                }

                Environment.Exit(0);
            } catch (Exception) { }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            ShadowForm.ApplyShadows(this);

            Iniciar();
        }

        /// <summary>
        /// Quando a FORM estiver sendo fechada
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Quando carregado
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            await Styles.Iniciar(this);
        }
    }
}
