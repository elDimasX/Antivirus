///
/// Arquivo de alerta, confirmação
///

using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Alert : Form
    {
        // Valor para verificar se queremos uma senha após clicar em "OK"
        bool requerSenha = false;

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Alert(string ex, bool cancelButtonActive)
        {
            InitializeComponent();

            // Aplique a sombra na FORM
            ShadowForm.ApplyShadows(this);

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            // Altere o texto
            label1.Text = ex;
            CANCEL.Visible = cancelButtonActive;

            // Se o arquivo de senha existir
            if (File.Exists(Global.Strings.arquivoSenha) && cancelButtonActive == true)
            {
                label1.Text += "\r\nPara continuar, digite sua senha:";

                // Mostre a caixa
                passwordText.Visible = true;
                requerSenha = true;
            }

            // Não é pra senha
            else
            {
                // Altere o valor
                Size = new Size(536, 326);
                MinimumSize = new Size(536, 326);
                MaximumSize = new Size(536, 326);
            }
        }

        /// <summary>
        /// Botão de OK
        /// </summary>
        private async void OK_Click(object sender, EventArgs e)
        {
            // Se requerer uma senha
            if (requerSenha == true)
            {
                // Se a senha estiver errada
                if (Global.VerificarSenha(passwordText.Text) == false)
                {
                    // Mostre uma mensagem de erro
                    label1.Text = "Senha incorreta, tente novamente.";
                    CANCEL.Visible = false;
                    OK.Click += new EventHandler(CANCEL_Click);
                    passwordText.Visible = false;

                    // Pare
                    return;
                }
            }

            // Efeito
            await Global.Styles.Fechar(this);

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Botão de cancelar
        /// </summary>
        private async void CANCEL_Click(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Fechar(this);

            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Fechar(this);

            Close();
        }

        /// <summary>
        /// Quando carregado
        /// </summary>
        private async void Alert_Load(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Iniciar(this);
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
