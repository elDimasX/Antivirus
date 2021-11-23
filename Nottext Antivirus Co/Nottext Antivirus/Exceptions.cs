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
    public partial class Exceptions : Form
    {
        /// <summary>
        /// Quando iniciado
        /// </summary>
        public Exceptions()
        {
            InitializeComponent();
            Global.SetHandCursor(this);

            try
            {
                // Pegue as linhas do arquivo
                string[] linhas = File.ReadAllLines(Global.Strings.excecoes);

                // Leia linha por linha
                foreach (string linha in linhas)
                {
                    // Adicione o item ao listbox
                    listBox1.Items.Add(linha);
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Adiciona um processo a lista de permitidos
        /// </summary>
        private async Task AtualizarLista()
        {
            try
            {
                await Task.Delay(100);
                File.WriteAllText(Global.Strings.excecoes, "");

                // Uma lista para salvar todos os processos permitidos
                List<string> lista = new List<string>();

                // Procura todos os items no listBox para salvar eles depois
                foreach (string processo in listBox1.Items)
                {
                    // Adiciona a lista
                    lista.Add(processo);
                }

                // Escreve no arquivo
                File.WriteAllLines(Global.Strings.excecoes, lista);
            }
            catch (Exception ex)
            {
                // Caso falhe, tente novamente
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Botão de adicionar arquivo
        /// </summary>
        private async void addFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "|*.*";
                dialog.Multiselect = true;
                dialog.Title = "Adicionar arquivos á lista branca do antivírus";

                // Se selecionou os arquivos
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Se a senha estiver errada
                    if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo adicionar esses arquivos nas exeções?") == false)
                    {
                        // Pare
                        return;
                    }

                    // Procure todos os arquivos
                    foreach (string file in dialog.FileNames)
                    {
                        // Adicione ao arquivo
                        listBox1.Items.Add(file);

                        // Adicione a lista de exeções
                        await AtualizarLista();
                    }
                }
            } catch (Exception ex)
            {
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Adicionar pasta
        /// </summary>
        private async void addFolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Selecione a pasta para o antivírus não verificar";

                // Se for OK
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Verifique a senha
                    if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo adicionar essa pasta nas exeções?") == false)
                    {
                        // Pare
                        return;
                    }

                    // Adicione a listBox
                    listBox1.Items.Add(dialog.SelectedPath);
                    await AtualizarLista();
                }
            } catch (Exception ex)
            {
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Botão de remover
        /// </summary>
        private async void remove_Click(object sender, EventArgs e)
        {
            // Se não estiver nada selecionado
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            // Se a senha estiver errada
            if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo remover esses arquivos das exeções?") == false)
            {
                // Pare
                return;
            }

            // Procure todos os items selecionados
            foreach (string item in listBox1.SelectedItems.OfType<string>().ToList())
            {
                // Vamos remover o item selecionado para atualizar a lista
                listBox1.Items.Remove(item);
            }

            // Remova as exeções
            await AtualizarLista();
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
