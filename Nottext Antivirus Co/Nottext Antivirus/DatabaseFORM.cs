///
/// Arquivo de banco de dados
///

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class DatabaseFORM : Form
    {
        // O formulário está sendo iniciado
        bool iniciando = true;

        /// <summary>
        /// Checka as opções de atualizações
        /// </summary>
        private void Modos()
        {
            // Se o arquivo de Manualmente não existir
            if (!File.Exists(Global.Strings.bancoAutomatico))
            {
                // A opção automaticamente está habilitada
                opcoes.Text = "Automaticamente";
            }
            else // Se o arquivo de Manualmente existir
            {
                // A opção automaticamente está desabilitada
                opcoes.Text = "Manualmente";
            }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public DatabaseFORM()
        {
            InitializeComponent();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            Modos();
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Após as opções de atualização de banco de dados automaticamente
        /// Forem alteradas
        /// </summary>
        private void opcoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Se estiver com opção de Automaticamente
                if (opcoes.Text == "Automaticamente")
                {
                    // Delete o arquivo de opção manualmente
                    File.Delete(Global.Strings.bancoAutomatico);
                }

                else // Se estiver com opção Manualmente
                {
                    // Se a senha estiver ativada
                    if (File.Exists(Global.Strings.arquivoSenha) && iniciando == false)
                    {
                        // Se falhar na senha
                        if (Global.Senha("Deseja mesmo alterar esta configuração?") == false)
                        {
                            opcoes.Text = "Automaticamente";
                            return;
                        }
                    }

                    // Crie o arquivo de opção manualmente
                    File.Create(Global.Strings.bancoAutomatico).Close();
                }
            }
            catch (Exception)
            {
            }

            // Não está mais iniciando
            iniciando = false;
        }

        /// <summary>
        /// Ocultar e desabiltiar os botões
        /// </summary>
        private void OcultarDesabilitar(bool verdadeiro)
        {
            update.Enabled = verdadeiro;
            loading.Visible = !verdadeiro;
            label5.Visible = !verdadeiro;
        }

        /// <summary>
        /// Atualizar o banco de dados
        /// </summary>
        private async void update_Click(object sender, EventArgs e)
        {
            // Oculta a mensagem
            label6.Visible = false;

            try
            {
                // Desabilita os botões
                OcultarDesabilitar(false);
                await Task.Delay(500);

                // Coloque a Label de "atualizando banco de dados" visível
                label6.Visible = true;

                // Verifica se obteve sucesso
                if (AtualizarBanco.AtualizarBancoDeDados(false) == true)
                {
                    // Sucesso
                    label6.Text = "O banco de dados foi atualizado com êxito.";
                }

                // Se falhar
                else
                {
                    // Mostre a mensagem de erro
                    label6.Text = "Ocorreu um erro ao atualizar o banco de dados, tente novamente.";
                }

                // Habilita os botões
                OcultarDesabilitar(true);
            }
            catch (Exception)
            {
                // Habilita os botões
                OcultarDesabilitar(true);

                // Modificar texto
                label6.Text = "Ocorreu um erro ao atualizar o banco de dados, tente novamente.";
                label6.Visible = true;
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
