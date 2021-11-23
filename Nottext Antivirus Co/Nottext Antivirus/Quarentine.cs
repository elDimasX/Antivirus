///
/// Arquivo de quarentena
/// 

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Quarentine : Form
    {
        // Pasta de quarentena
        string pasta = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Quarentine\\";
        public static string passwordFiles = "12345kkj";

        /// <summary>
        /// Função que atualiza a quarentena
        /// </summary>
        public void AtualizarQuarentena()
        {
            try
            {
                // Se a pasta de quarentena não existir
                if (!Directory.Exists(pasta))
                {
                    // Crie a pasta de quarentena
                    Directory.CreateDirectory(pasta); 
                }
                else // Se a pasta já existir, pegue os arquivos dentro da pasta
                {
                    // Limpe os itens
                    listBox1.Items.Clear();

                    // Pega todos os arquivos na pasta de quarentena com extensão
                    // .NOTTEXT
                    string[] arquivo = Directory.GetFiles(pasta, "*.NOTTEXT");

                    // Procure de arquivo por arquivo
                    foreach (string arquivos in arquivo)
                    {
                        // Pega o nome do arquivo sem extensão
                        string nome = Path.GetFileNameWithoutExtension(arquivos);

                        // Adicione ao listBox
                        listBox1.Items.Add(nome);
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Quarentine()
        {
            // Inicie a FORM
            InitializeComponent();

            // Atualize a quarentena
            AtualizarQuarentena();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Quando clicar em restaurar
        /// </summary>
        private void restore_Click(object sender, EventArgs e)
        {
            try
            {
                // Se nenhum arquivo for selecionado
                if (listBox1.SelectedItem == null)
                {
                    return; // Não faça mais nada
                }

                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo restaurar esses arquivos?") == false)
                {
                    return;
                }

                // Abre o selecionador de pastas
                FolderBrowserDialog pastaDialog = new FolderBrowserDialog();

                // Descrição
                pastaDialog.Description = "Selecione uma pasta para restauras os arquivos selecionados";

                // Depois que o usuário escolher a pasta
                if (pastaDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtenha todos os itens selecionados no listBox
                    foreach (string arquivos in listBox1.SelectedItems)
                    {
                        try
                        {
                            // Arquivo que foi selecionado
                            string arquivo = pasta + "\\" + arquivos + ".NOTTEXT";

                            // Mova o arquivo descriptografado
                            File.Move(arquivo, pastaDialog.SelectedPath + "\\" + arquivos);
                        }
                        catch (Exception ex)
                        {
                            // Mostre o erro
                            Global.MostrarErro(ex.Message, false);
                        }
                    }

                    // Agora, atualize a quarentena
                    AtualizarQuarentena();
                }
            }
            catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Botão de excluir
        /// </summary>
        private void delete_Click(object sender, EventArgs e)
        {
            // Se nenhum arquivo for selecionado
            if (listBox1.SelectedItem == null)
            {
                return; // Não faça mais nada
            }

            // Se a senha estiver errada
            if (Global.Senha("Deseja mesmo deletar os itens selecionados?") == false)
            {
                return;
            }
            
            // Procure todos os itens selecionados
            foreach (string arquivos in listBox1.SelectedItems)
            {
                try
                {
                    // Arquivo que foi selecionado
                    string arquivo = pasta + "\\" + arquivos + ".NOTTEXT";

                    // Deleta o arquivo
                    File.Delete(arquivo);
                }
                catch (Exception ex)
                {
                    // Mostre o erro
                    Global.MostrarErro(ex.Message, false);
                }
            }

            // Atualize a quarentena
            AtualizarQuarentena();
        }

        /// <summary>
        /// Selecionar todos os items da listBox
        /// </summary>
        private void select_LinkClicked(object sender, EventArgs e)
        {
            // Selecione tudo
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                // Seleciona todos os arquivos
                listBox1.SetSelected(i, true);
            }
        }

        /// <summary>
        /// Remover todos os items selecionados da listBox
        /// </summary>
        private void deselect_LinkClicked(object sender, EventArgs e)
        {
            // Remova tudo
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                // Remova todos os arquivos
                listBox1.SetSelected(i, false);
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