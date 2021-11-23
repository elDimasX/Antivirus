///
/// Exeções de anti-ransomware
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class ExecptionsRANSO : Form
    {
        /// <summary>
        /// Carrega tudo
        /// </summary>
        public ExecptionsRANSO()
        {
            InitializeComponent();

            // Aplique a sombra na FORM
            ShadowForm.ApplyShadows(this);

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            try
            {
                // Pegue as linhas do arquivo
                string[] linhas = File.ReadAllLines(Global.Strings.excecoesAntiRanso);

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
                // Intervalo
                await Task.Delay(500);

                // Uma lista para salvar todos os processos permitidos
                List<string> lista = new List<string>();

                // Procura todos os items no listBox para salvar eles depois
                foreach (string processo in listBox1.Items)
                {
                    // Adiciona a lista
                    lista.Add(processo.ToUpper());
                }

                // Escreve no arquivo
                File.WriteAllLines(
                    
                    // Arquivo de exeções do anti-ransomware
                    Global.Strings.excecoesAntiRanso,
                    
                    // Nossa lista
                    lista
                );
            } catch (Exception)
            {
                // Caso falhe, tente novamente
                await AtualizarLista();
            }
        }

        /// <summary>
        /// Botão de adicionar
        /// </summary>
        private async void Adicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abra uma seleção de arquivo
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;
                dialog.Filter = "Arquivo Executável (.exe)|*.exe";
                dialog.Title = "Adicionar programas na lista branca do anti-ransomware";

                // Se ele selecionou um arquivo
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Se a senha estiver errada
                    if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo adicionar esses arquivos nas exeções do anti-ransomware?") == false)
                    {
                        // Pare
                        return;
                    }

                    // Procure todos os arquivos
                    foreach (string file in dialog.FileNames)
                    {
                        // Adicione ao arquivo
                        listBox1.Items.Add(file.ToUpper());

                        // Tamanho de todas as caracterias
                        int Length = 0;

                        // Procure todos os itens na listBox
                        foreach (string items in listBox1.Items)
                        {
                            // Aumente o valor
                            Length += items.Length;
                        }

                        // Se for maior do que o permitido, o driver não conseguirá
                        // Ler o arquivo, e isso causará um erro, não podemos continuar
                        if (Length > 1023)
                        {
                            // Remova o item da listBox
                            listBox1.Items.Remove(file.ToUpper());

                            // Mostre um erro
                            Global.MostrarErro("Não é possível adicionar esse processo á lista, porque o máximo de caracterias estourou, remova outros processos da lista, e tente novamente", false);

                            // Pare
                            break;
                        }
                    }

                    // Adicione a lista de exeções
                    await AtualizarLista();
                }
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Botão de remover
        /// </summary>
        private async void Remover_Click(object sender, EventArgs e)
        {
            try
            {
                // Se não estiver nada selecionado
                if (listBox1.SelectedItem == null)
                {
                    return;
                }

                // Se a senha estiver errada
                if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo remover esses arquivos das exeções do anti-ransomware?") == false)
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
               

                await AtualizarLista();
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Restaurar tudo de volta
        /// </summary>
        public async Task RestaurarTudo()
        {
            try
            {
                // Limpe a listBox
                listBox1.Items.Clear();

                // Explorer
                listBox1.Items.Add("C:\\WINDOWS\\EXPLORER.EXE");

                // Winrar
                listBox1.Items.Add("C:\\PROGRAM FILES\\WINRAR\\WINRAR.EXE");
                listBox1.Items.Add("C:\\PROGRAM FILES (X86)\\WINRAR\\WINRAR.EXE");

                // Serviços
                listBox1.Items.Add("C:\\WINDOWS\\SYSTEM32\\SERVICES.EXE");

                // DLL Host
                listBox1.Items.Add("C:\\WINDOWS\\SYSTEM32\\DLLHOST.EXE");
                listBox1.Items.Add("C:\\WINDOWS\\SYSTEM32\\VSSVC.EXE");

                // Notepad
                listBox1.Items.Add("C:\\WINDOWS\\NOTEPAD.EXE");

                // Pasta do OneDrive
                string OneDrive =
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToUpper()
                    + "\\APPDATA\\LOCAL\\MICROSOFT\\ONEDRIVE\\";

                // OneDrive
                listBox1.Items.Add(OneDrive + "ONEDRIVE.EXE");
                listBox1.Items.Add(OneDrive + "21.016.0124.0003\\FILECOAUTH.EXE");

                // Atualize a lista
                await AtualizarLista();
            } catch (Exception)
            {
                // Caso falhe, tente novamente
                await RestaurarTudo();
            }
        }

        /// <summary>
        /// Botão de restaurar
        /// </summary>
        private async void restore_Click(object sender, EventArgs e)
        {
            try
            {
                // Se a senha estiver errada
                if (File.Exists(Global.Strings.arquivoSenha) && Global.Senha("Deseja mesmo restaurar todos os arquivos padrões?") == false)
                {
                    // Pare
                    return;
                }

                // Restaure os padrões
                await RestaurarTudo();
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
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
        /// Quando carregado
        /// </summary>
        private async void ExecptionsRANSO_Load(object sender, EventArgs e)
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