///
/// Arquivo que contém as funções globais, tudo de mais importante
///

using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    class Global
    {
        /// <summary>
        /// Arquivos strings
        /// </summary>
        public class Strings : Global
        {
            // Quais programas já foram avisados, e o usuário ignorou (serve para não avisar novamente)
            public static string programasIgnorados = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\programs.notified";

            // Bloquear a ameaça em vez de remove-la
            public static string bloquearAmeaca = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\blockThreat.set";

            // Vezes em que malware foi encontrado
            public static int virusEncontrados = 0;

            // Arquivo de senha
            public static string arquivoSenha = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\password.set";

            // Leitura profunda
            public static string leituraProfunda = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\advancedScan.set";

            // Arquivo de proteção em tempo real
            public static string protecaoTempoReal = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\realTime.set";

            // Arquivo de atualização automatica
            public static string bancoAutomatico = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\MANU.set";

            // Saber se hoje, o banco de dados já foi atualizado
            public static string atualizarHoje = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\UPDATEDGO.set";

            // Saber se o serviço criou um ponto de restauração hoje
            public static string pontoEscrito = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\CREATEDPOINT.set";

            // Saber se a data que o ponto de restauração foi criado
            public static string dataPonto = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\WRITED.set";

            // Arquivo de database, que o serviço vai ler para detectar vírus
            public static string database = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\database.ini";

            // Arquivo de notificação para o Nottext ler e mostrar a mensagem
            public static string notificar = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\NOTIFY.set";

            // Deletação pendente de pastas ou arquivos para o serviço de vulnerabilidades
            public static string deletacaoPendente = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\DELETINGPENTEND.set";

            // Verifica se a notificação está ligada ou não
            public static string notificacaoAtiva = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\DSBPT.set";

            // Saber se a proteção de Windows está ligada ou não
            public static string windowsProtection = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\RESTORED.set";

            // O driver irá escrever neste arquivo para mostrarmos ao usuário
            // Qual processo foi bloqueado de modificar a pasta protegida
            public static string ransoBlocked = "C:\\ProgramData\\Microsoft\\BLOCKEDRNS.set";
            public static string arquivoBlocked = "C:\\ProgramData\\Microsoft\\BLOCKEDFL.set";

            // Notificações das vulnerabilidades
            public static string notificaoVulnerabilidade = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\vulnerability.notify";

            // Saber se o anti-ransomware está ligado
            public static string antiRansomware = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\antiRansomware.set";

            // Pasta de configurações
            public static string configuracoes = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\";

            // Psta ProgramData
            public static string programData = "C:\\ProgramData\\Nottext\\Nottext Antivirus";

            // Arquivo que contém as exeções de vírus
            public static string excecoes = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\EXECPTION.set";

            // Arquivo que contém as exeções de anti-ransomware
            public static string excecoesAntiRanso = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\EXECPTIONRANSO.set";

            // Aqui contém os arquivos de atualizações do banco de dado do antivírus
            public static string atualizarListaVirus = "https://pastebin.com/raw/YePnKfK7";

            // Saber a versão para decidir atualizar o Nottext
            public static string versaoAtualizar = "https://pastebin.com/raw/uEtRiw5a";
        }

        // Senha usada na função de criptografia
        static string passwordForEncrypt = "cryptographyForPassword";

        /// <summary>
        /// Operação de criptografia
        /// </summary>
        public class Cryptography : Global
        {
            /// <summary>
            /// Criptografa uma string, meu metódo porco foi feito para esse projeto
            /// Eu quero evitar o máximo o uso de outros códigos
            /// </summary>
            public static string CriptografarString(string texto)
            {
                // Texto criptografado
                string textoCriptografado = "";

                // Procure todas as caracterias na senha
                foreach (char chars in passwordForEncrypt)
                {
                    // Letra
                    string c = chars.ToString();

                    // Faça uma "criptografia" básicona
                    textoCriptografado +=

                        // Substitua algumas caracterias
                        texto.Replace(c, "Aa9d3i" + c + "3t4s22F" + c + "21dF3g4" + c + c)

                       // Outras caracterias
                       .Replace("a", "32FK3").Replace("1", "3radf4").Replace("d", "asd32rjiID")

                       // Outras caracterias
                       .Replace("e", "fgk3J").Replace("3", "f239kf3").Replace("5", "6lhg4")

                       // Outras caracterias
                       .Replace("g", "fk5k3").Replace("i", "lkh4").Replace("u", "u2").Replace("o", "5");
                }

                // Retorne a string criptografada
                return textoCriptografado;
            }
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

            /// <summary>
            /// Ativa o efeito de sombra
            /// </summary>
            public static void GunaButtonEfeitoAtivar(Guna2Button botao)
            {
                //botao.ShadowDecoration.Depth = 40;
                botao.ShadowDecoration.Enabled = true;
            }

            /// <summary>
            /// Desativa o efeito de sombra
            /// </summary>
            public static void GunaButtonEfeitoDesativar(Guna2Button botao)
            {
                //botao.ShadowDecoration.Depth = 0;
                botao.ShadowDecoration.Enabled = false;
            }
        }

        /// <summary>
        /// Verifica se a senha está correta
        /// </summary>
        public static bool VerificarSenha(string text)
        {
            try
            {
                // Senha criptografado
                string criptografado = Cryptography.CriptografarString(text);

                // Se for o mesmo texto do arquivo
                if (criptografado == File.ReadAllText(Strings.arquivoSenha))
                {
                    // Verdade
                    return true;
                }
            } catch (Exception) { }

            // Falso
            return false;
        }

        /// <summary>
        /// Verifica se a senha está correta para continuar a operação atual
        /// </summary>
        public static bool Senha(string msg)
        {
            // Novo alerta
            Alert alert = new Alert(msg, true);
            
            // Se for OK
            if (alert.ShowDialog() == DialogResult.OK)
            {
                // Senha correta, retorne verdadeiro
                return true;
            }

            // Falso
            return false;
        }

        /// <summary>
        /// Verifica uma proteção
        /// </summary>
        public static void VerificarProtecao(string arquivo, Guna2CustomCheckBox checkBox)
        {
            // Se a proteção estiver desativada
            if (File.Exists(arquivo))
            {
                // Desmarque o arquivo
                checkBox.Checked = false;
            } else // Se a proteção estiver ativada
            {
                // Marque o checkbox
                checkBox.Checked = true;
            }
        }

        /// <summary>
        /// Ativa a proteção de algum componente
        /// </summary>
        public static void AtivarDesativarProtecao(string arquivo, Guna2CustomCheckBox checkbox)
        {
            try
            {
                // Se a proteção estiver ativada
                if (checkbox.Checked == true)
                {
                    // Delete o arquivo para ativar a proteção
                    File.Delete(Strings.configuracoes + arquivo);
                }
                else // Se a proteção estiver desativada
                {
                    // Crie o arquivo para desativar a proteção
                    File.Create(Strings.configuracoes + arquivo).Close();
                }
            }
            catch (Exception ex)
            {
                // Mostre a mensagem de erro
                MostrarErro(ex.Message, false);
                checkbox.Checked = !checkbox.Checked;
            }
        }

        /// <summary>
        /// Inicia um processo
        /// </summary>
        public static async Task IniciarProcesso(string arquivo, string argumento)
        {
            try
            {
                // Inicia um processo
                Process processo = new Process();
                processo.StartInfo.FileName = arquivo;
                processo.StartInfo.Arguments = argumento;

                await Task.Delay(1); // Intervalo

                // Inicia o processo em segundo plano
                processo.StartInfo.Verb = "runas";
                processo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processo.Start();
                processo.WaitForExit();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Importação da DLL para alterar o cursor
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        // Novo cursor
        private static readonly Cursor CursorMao = new Cursor(LoadCursor(IntPtr.Zero, 32649));

        /// <summary>
        /// Configurar o cursor
        /// </summary>
        public static void SetHandCursor(Control body)
        {
            // Procure todos os controles na FORM
            foreach (Control control in body.Controls)
            {
                try
                {
                    // Int
                    int i;

                    // Se for um 
                    if (control.Cursor == Cursors.Hand)
                    {
                        // Altere o cursor
                        control.Cursor = CursorMao;
                    }

                    // Procure outros paineis na FORM
                    for (i = 0; i < 2; i++)
                    {
                        // Sete de novo
                        SetHandCursor(control);
                    }
                } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        /// <summary>
        /// Função que mostra uma janela de ERRO
        /// </summary>
        public static void MostrarErro(
            // String mensagem
            string ex,

            // Ativar o botão de "Cancelar", caso seja uma notificação ou confirmação
            bool ativarBotaoCancelar
        )
        {
            // Alerta, altere os textos 
            Alert alerta = new Alert(ex, ativarBotaoCancelar);

            // Mostre
            alerta.ShowDialog();
        }

        /// <summary>
        /// Função que verifica se a notificação está habilitada
        /// </summary>
        public static bool NotificacaoHabilitada()
        {
            // Se o arquivo de notificação existir
            if (File.Exists(Strings.notificacaoAtiva))
            {
                // Notificação desabilitadas
                return false;
            }

            // Notificação estão habiltiadas
            return true;
        }

        // Tamanho do monitor, usado para mostrar a notificação no canto superior direito
        static int width = Screen.PrimaryScreen.WorkingArea.Width;
        static int height = Screen.PrimaryScreen.WorkingArea.Height;

        /// <summary>
        /// Mostre a FORM de notificação
        /// </summary>
        public static void MostrarNotificacao(

            // Mensagem para mostrar
            string mensagem
        )
        {
            // Novo thread
            new Thread(() =>
            {
                // Só continue caso as notificação estejam habilitas
                if (NotificacaoHabilitada())
                {
                    // FORM de notificação
                    Notificacao notificacao = new Notificacao(mensagem);

                    // Posição da FORM
                    notificacao.Location = new Point(
                        width - notificacao.Width,
                        height - notificacao.Height);

                    // Mostra a FORM
                    notificacao.ShowDialog();
                }
            }).Start();
        }

        /// <summary>
        /// Verifica se a proteção está habilitada
        /// </summary>
        public static bool ProtecaoHabilitada()
        {
            // Se a proteção estiver desabilita
            if (File.Exists(Strings.protecaoTempoReal))
            {
                // Proteção desabilitada
                return false;
            }

            // Proteção habilitada
            return true;
        }

    }
}
