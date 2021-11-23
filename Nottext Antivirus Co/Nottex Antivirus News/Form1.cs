using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottex_Antivirus__News
{
    public partial class Form1 : Form
    {
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
        /// Inicie a FORM
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            ShadowForm.ApplyShadows(this);

            DllCursor.SetHandCursor(this);
        }

        /// <summary>
        /// FORM Fechando
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Inicie a form "iniciando"
                Process.Start(Application.StartupPath + "\\Nottext Antivirus Start.exe");
            } catch (Exception) { }

            Environment.Exit(0);
        }

        /// <summary>
        /// Botão de OK
        /// </summary>
        private async void ok_Click(object sender, EventArgs e)
        {
            await Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            await Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Carregado
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
