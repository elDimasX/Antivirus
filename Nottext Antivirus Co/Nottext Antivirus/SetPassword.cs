///
/// Setar uma senha
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class SetPassword : Form
    {
        /// <summary>
        /// Inicia tudo
        /// </summary>
        public SetPassword()
        {
            InitializeComponent();

            // Aplique a sombra na FORM
            ShadowForm.ApplyShadows(this);

            // Configure o cursor nos controles
            Global.SetHandCursor(this);
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            await Global.Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Botão de cancelar
        /// </summary>
        private async void CANCEL_Click(object sender, EventArgs e)
        {
            await Global.Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Botão de OK
        /// </summary>
        private async void OK_Click(object sender, EventArgs e)
        {
            try
            {
                // Se for maior que 2
                if (passwordText.TextLength > 0)
                {
                    // Crie a senha
                    File.WriteAllText(Global.Strings.arquivoSenha, Global.Cryptography.CriptografarString(passwordText.Text));

                    // Saia
                    await Global.Styles.Fechar(this);
                    DialogResult = DialogResult.OK;
                }
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Carregado
        /// </summary>
        private async void SetPassword_Load(object sender, EventArgs e)
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
