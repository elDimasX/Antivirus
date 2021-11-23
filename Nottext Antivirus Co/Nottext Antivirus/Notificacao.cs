///
/// Notificação
///

using System;
using System.IO;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Notificacao : Form
    {
        /// <summary>
        /// Carrega tudo
        /// </summary>
        public Notificacao(string msgPrincipal)
        {
            try
            {
                // Se o arquivo não existir
                if (!File.Exists(Global.Strings.programasIgnorados))
                {
                    // Crie
                    File.Create(Global.Strings.programasIgnorados).Close();
                }

                // Ignore os threads
                CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();

                // Aplique a sombra na FORM
                ShadowForm.ApplyShadows(this);

                // Configure o cursor nos controles
                Global.SetHandCursor(this);

                message.Text = msgPrincipal;

                // Se for um programa potencialmente prejudicial
                if (message.Text.Contains("Um programa potencialmente prejudicial"))
                {
                    close.Visible = false;

                    actionButton.Visible = true;
                    ok.Text = "IGNORAR";

                    // Se for um arquivo
                    if (File.Exists(ObterLinha(message.Text, 2)))
                    {
                        actionButton.Text = "REMOVER ARQUIVO";
                    }
                    // Se for uma pasta
                    else if (Directory.Exists(ObterLinha(message.Text, 2)))
                    {
                        actionButton.Text = "REMOVER PASTA";
                    }
                }
            }
            catch (Exception) { }
        }

        // Se permiti ou não fechar a FORM
        bool permitir = false;

        /// <summary>
        /// Botão de OK
        /// </summary>
        private async void ok_Click(object sender, EventArgs e)
        {
            try
            {
                // Se ele clicar em ignorar, adicione as exeções
                if (actionButton.Text.Contains("REMOVER PASTA") && actionButton.Visible == true)
                {
                    // Verifique a existencia da senha antes
                    if (File.Exists(Global.Strings.arquivoSenha) && (Global.Senha("Confirme a senha antes de completa a operação") == false))
                    {
                        // Pare
                        return;
                    }

                    // Adicione as exeções
                    File.AppendAllText(Global.Strings.programasIgnorados, ObterLinha(message.Text, 2).ToLower() + Environment.NewLine);
                }
            } catch (Exception) { }

            await Global.Styles.Fechar(this);
            permitir = true;
            Close();
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            try
            {
                // Se for para remover uma pasta, e o botão estiver visível
                if (actionButton.Text.Contains("REMOVER PASTA") && actionButton.Visible == true)
                {
                    // Verifique a existencia da senha antes
                    if (File.Exists(Global.Strings.arquivoSenha) && (Global.Senha("Confirme a senha antes de completa a operação") == false))
                    {
                        // Pare
                        return;
                    }

                    // O usuário clicou em OK, ele não quer remover a pasta, adicione as exeções
                    // Crie o arquivo para não notificar novamente
                    File.AppendAllText(Global.Strings.programasIgnorados, ObterLinha(message.Text, 2).ToLower() + Environment.NewLine);
                }
            } catch (Exception) { }

            // Efeito
            await Global.Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Quando carregado
        /// </summary>
        private async void Notificacao_Load(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Iniciar(this);
        }

        /// <summary>
        /// Obtém a string da linha
        /// </summary>
        private string ObterLinha(string texto, int linha)
        {
            // Linhas
            string[] linhas = texto.Replace("\r", "").Split('\n');

            // Retorne
            return linhas.Length >= linha ? linhas[linha - 1] : null;
        }

        /// <summary>
        /// Botão de ação
        /// </summary>
        private void actionButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifique a existencia da senha antes
                if (File.Exists(Global.Strings.arquivoSenha) && (Global.Senha("Confirme a senha antes de completa a operação") == false))
                {
                    // Pare
                    return;
                }

                // Se for para remover uma pasta
                if (actionButton.Text.Contains("REMOVER PASTA"))
                {
                    // Local da pasta para remover (linha 2)
                    string local = ObterLinha(message.Text, 2).ToLower();

                    // Se não existir
                    if (!File.Exists(Global.Strings.deletacaoPendente))
                    {
                        // Crie
                        File.Create(Global.Strings.deletacaoPendente).Close();
                    }

                    // Deixa marcado para deletação após a reinicialização
                    File.AppendAllText(Global.Strings.deletacaoPendente, local + Environment.NewLine);
                    actionButton.Visible = false;

                    message.Text = "Tudo certo, os arquivos e pastas serão deletados na próxima inicialização do Windows.";
                    ok.Text = "OK";
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Quando estiver fechando
        /// </summary>
        private void Notificacao_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (permitir == false)
                e.Cancel = true;
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