///
/// Arquivo de escenamento
///

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class virusScan : Form
    {
        // Verificação personalizada
        string verPersonalizada = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\SCAN.set";

        // Anote os arquivos da listBox neste arquivo primeiro, depois mova-o para que o antivírus
        // Possa escanear
        string verPersonalizadaArquivo = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\SCAN.wait";

        // Verificação rápida
        string verRapidaArquivo = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\FAST.set";

        // Verificação rápida
        string verCompletaArquivo = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\COMPLETE.set";

        // Verifica se já foi iniciado
        public bool escanIniciado = false;

        // Verifica se algum escaneamento já foi concluido, pra não ter que chamar 
        // "EscaneamentoNecessario" toda vez
        private bool escanFeito = false;

        // Lista de arquivos para verificação personalizada
        string pastaParaEscanear = "";

        /// <summary>
        /// Verifica quantas vezes malware foi encontrado na máquina, se ele
        /// Encontrar 2 vezes malwares, vamos realizar um escaneamento rápido
        /// </summary>
        private async void EscaneamentoNecessario()
        {
            // Repetição infinita
            while (true)
            {
                // 10 Segundos
                await Task.Delay(10000);

                // Se mais de 2 vírus foram encontrados, e nenhum escaneamento estiver
                // Sido inicializado, e também, se nenhum escaneamento já estiver em execução
                if (Global.Strings.virusEncontrados >= 3 && escanIniciado == false && escanFeito == false)
                {
                    // Altere o valor, não chame essa função novamente
                    escanFeito = true;

                    // Alerta
                    Global.MostrarNotificacao("Muitos malwares foram encontrados na sua máquina recentemente, para garantir sua segurança, iremos iniciar a verificação rápida.");

                    // Remova a string
                    pastaParaEscanear = "";

                    // Desmarque a verificação completa e personalizada
                    verificaoRapida.Checked = false;
                    verificaoPersonalizada.Checked = false;

                    // Verificação rápida
                    verificaoRapida.Checked = true;

                    // Inicie o escaneamento
                    await playEscan();
                }
            }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public virusScan()
        {
            // Inicia a FORM
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            // Verifica a quantidade de malwares que foram encontrados
            // Se forem muitos, iniciaremos um escaneamento rápido
            EscaneamentoNecessario();
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Adicionar arquivos para escanear
        /// </summary>
        private void adicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Novo OpenFileDialog
                FolderBrowserDialog pasta = new FolderBrowserDialog();
                pasta.ShowNewFolderButton = false;
                pasta.Description = "Procure uma pasta para escanear";

                // Se o usuário já fez a seleção de arquivos
                if (pasta.ShowDialog() == DialogResult.OK)
                {
                    // Ative o botão
                    playScan.Enabled = true;

                    // Altere o valor
                    pastaParaEscanear = pasta.SelectedPath;
                    playScan.Visible = true;

                    // Desabilite os botões de verificação rápida e completa
                    verificaoRapida.Checked = false;
                    verificaoRapida.Checked = false;
                }

                // Se nenhuma pasta for selecionada
                else
                {
                    // Desmarque o botão
                    verificaoPersonalizada.Checked = false;
                    playScan.Visible = false;
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Deletar com try catch
        /// </summary>
        private void DeletarArquivoTry(string arquivo)
        {
            try
            {
                // Deleta o arquivo
                File.Delete(arquivo); 
            } catch (Exception) { }
        }

        /// <summary>
        /// Esperar a verificação rápida ou completa terminar
        /// </summary>
        private async Task EsperarVerificacaoTerminar(string arquivo)
        {
            // Repetição infinita
            while (true)
            {
                // 5 Segundos
                await Task.Delay(5000);

                // Se o arquivo não existir, a verificação terminou
                if (!File.Exists(arquivo))
                {
                    // Aumente todo o valor
                    progress.Value = 100;
                    playScan.Enabled = false;
                    await Task.Delay(5000);

                    // Oculte a barra de progresso
                    progress.Visible = false;

                    // Altera a mensagem e mostre-a
                    waitLabel.Visible = false;
                    waitLabel.Text = "Verificação completa";

                    // Muda a imagem do botão de play
                    playScan.Image = Properties.Resources.play_button;
                    playScan.Enabled = true;
                    playScan.Visible = false;

                    // Limpe a lista
                    pastaParaEscanear = "";

                    // Desmarque as opções
                    verificaoCompleta.Checked = false;
                    verificaoRapida.Checked = false;
                    verificaoPersonalizada.Checked = false;

                    // Habilite os botões
                    verificaoCompleta.Enabled = true;
                    verificaoPersonalizada.Enabled = true;
                    verificaoRapida.Enabled = true;

                    // Pare o loop
                    break;
                }
            }
        }

        /// <summary>
        /// Vai alterando o valor do ProgressBar
        /// </summary>
        private void ProgressBar(int time)
        {
            // Resete o valor
            progress.Value = 1;

            // Novo thread
            new Thread(async () =>
            {
                // 92 vezes
                for (int i = 0; i < 90; i++)
                {
                    // Espere
                    await Task.Delay(time);

                    // Se o valor for 0 ou 100, o escaneamento já parou
                    if (progress.Value == 0 || progress.Value == 100)
                    {
                        // Pare
                        break;
                    }

                    // Só adicione o valor se for menor que 90, ou seja, máximo 89
                    else if (progress.Value < 90)
                    {
                        // Adicione um valor
                        progress.Value += 1;
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Verificação de vírus, se é rápida ou completa ou personalizada
        /// </summary>
        private async Task Verificacoes()
        {
            // Escaneamento iniciado
            escanIniciado = true;
            progress.Visible = true;
            progress.Value = 0;

            // Desabilite o botão
            playScan.Enabled = false;

            // Antes de começar o escaneamento, vamos atualizar o banco de dados
            waitLabel.Text = "Atualizando a base de dados...";
            waitLabel.Visible = true;

            // 1 Segundo
            await Task.Delay(4000);

            // Espere a atualização do banco de dados
            AtualizarBanco.AtualizarBancoDeDados(false);

            // Altere o texto
            waitLabel.Text = "Base de dados atualizada";

            // 1 Segundo
            await Task.Delay(3000);

            // Restaure o botão
            playScan.Enabled = true;

            // Se for verificação personalizada
            if (verificaoPersonalizada.Checked == true)
            {
                // Escreva um novo arquivo
                File.WriteAllText(
                    verPersonalizadaArquivo,
                    pastaParaEscanear
                );

                // Agora, mova o arquivo para o antivírus escanear
                File.Move(verPersonalizadaArquivo, verPersonalizada);

                // Vá aumentando o valor do ProgressBar, 50 milisegundos
                ProgressBar(600);

                // Altere o texto
                waitLabel.Text = "Verificando: " + pastaParaEscanear;

                // Esperar a verficiação
                await EsperarVerificacaoTerminar(verPersonalizada);

                // Verificação completa
                Global.MostrarNotificacao("A verificação personalizada foi concluída com sucesso.");
            }

            // Verificação rápida
            if (verificaoRapida.Checked == true)
            {
                // Crie um arquivo para iniciar a verificação
                File.Create(verRapidaArquivo).Close();
                escanFeito = true;

                // Vá aumentando o valor do ProgressBar
                ProgressBar(1250);

                // Altere o texto
                waitLabel.Text = "Verificação rápida em andamento...";

                // Esperar a verficiação
                await EsperarVerificacaoTerminar(verRapidaArquivo);

                // Verificação completa
                Global.MostrarNotificacao("A verificação rápida foi concluída com sucesso.");
            }

            // Verificação completa
            else if (verificaoRapida.Checked == true)
            {
                // Crie um arquivo para iniciar a verificação
                File.Create(verCompletaArquivo).Close();
                escanFeito = true;

                // Vá aumentando o valor do ProgressBar
                ProgressBar(5000);

                // Altere o texto
                waitLabel.Text = "Verificação completa em andamento...";

                // Esperar a verficiação
                await EsperarVerificacaoTerminar(verCompletaArquivo);

                // Verificação completa
                Global.MostrarNotificacao("A verificação completa foi concluída com sucesso.");
            }

            // O escan terminou
            escanIniciado = false;
        }

        /// <summary>
        /// Para o escaneamento
        /// </summary>
        private void PararEscan()
        {
            try
            {
                // Desabilita o botão
                playScan.Enabled = false;
                escanIniciado = false;

                // Delete o arquivo de verificação rápida
                DeletarArquivoTry(verRapidaArquivo);

                // Delete o arquivo de verificação completa
                DeletarArquivoTry(verCompletaArquivo);

                // Delete o arquivo de verificação personalizada
                DeletarArquivoTry(verPersonalizada);
            } catch (Exception) { }
        }

        /// <summary>
        /// Evento ocorre quando o botão é clicado, ou a FORM solicita um escaneamento
        /// </summary>
        public async Task playEscan()
        {
            try
            {
                if (
                    // Se nenhuma opção tiver sido marcada
                    verificaoCompleta.Checked == false &&
                    verificaoPersonalizada.Checked == false &&
                    verificaoRapida.Checked == false
                )
                {
                    // Pare
                    return;
                }

                // Desabilite os botões
                verificaoCompleta.Enabled = false;
                verificaoPersonalizada.Enabled = false;
                verificaoRapida.Enabled = false;
                playScan.Visible = true;

                // Se o escaneamento já foi iniciado
                if (escanIniciado == true)
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo parar a verificação atual?") == false)
                    {
                        return;
                    }

                    // Pare o escaneamento
                    PararEscan();

                    // Não execute mais nada
                    return;
                }

                // Altere a imagem
                playScan.Image = Properties.Resources.stop_button;

                // Habilite o botão para que o usuário possa parar o escaneamento
                playScan.Enabled = true;

                // Escaneamento iniciado
                escanIniciado = true;

                // Inicia as verificação
                await Verificacoes();
            }
            catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Botão de iniciar escaneamento
        /// </summary>
        private async void play_Click(object sender, EventArgs e)
        {            
            // Inicie o escaneamento
            await playEscan();
        }

        /// <summary>
        /// Evento que ocorre quando algum botão de escanear é pressionado
        /// </summary>
        private void verificacoesBotoes_Click(object sender, EventArgs e)
        {
            // Habilite o botão
            playScan.Visible = true;
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


