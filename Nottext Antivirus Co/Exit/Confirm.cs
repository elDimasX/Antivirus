using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Exit.KeyboardHook;

namespace Exit
{
    public partial class Confirm : Form
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
        /// Inicia tudo
        /// </summary>
        public Confirm()
        {
            // Inicie a FORM
            InitializeComponent();

            // Sete a FORM no topo
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            // Adicione as sombras
            ShadowForm.ApplyShadows(this);
            DllCursor.SetHandCursor(panel1);
        }

        /// <summary>
        /// Botão de cancelar
        /// </summary>
        private void cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Sair depois de um tempo
        /// </summary>
        private void SairTempo()
        {
            // Novo thread
            new Thread(async () =>
            {
                // Tempo (6 segundos)
                await Task.Delay(6000);
                await Styles.Fechar(this);

                // Saia
                Environment.Exit(0);
            }).Start();
        }

        /// <summary>
        /// Botão de sair
        /// </summary>
        private void sairBtn_Click(object sender, EventArgs e)
        {
            // Saia
            SairCls sair = new SairCls();

            // Desabilite os botões de sair
            sairBtn.Enabled = false;
            cancelar.Enabled = false;

            // Sair
            SairTempo();

            // Novo thread
            new Thread(async () =>
            {
                await sair.Sair(); // Sai do aplicativo
            }).Start();
        }

        /// <summary>
        /// FORM fechado
        /// </summary>
        private void Confirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se o botão tiver desabilitado
            if (sairBtn.Enabled == false)
                e.Cancel = true; // Não saia
        }

        /// <summary>
        /// Quando carregar
        /// </summary>
        private async void Confirm_Load(object sender, EventArgs e)
        {
            await Styles.Iniciar(this);
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
