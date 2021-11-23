///
/// Proteção de Windows
/// 

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Windows : Form
    {
        /// <summary>
        /// Saber se o ponto de restauração foi criado hoje
        /// </summary>
        private async void PointCreatedToday()
        {
            // Se a proteção não estiver habilitada
            if (restorePoint.Checked == false)
                return; // Pare

            try
            {
                // 5 Segundos
                await Task.Delay(7500);

                // Arquivo writed, o serviço escreve o dia em que ele criou o ponto de
                // Restauração.
                string readed = File.ReadAllText(Global.Strings.dataPonto);

                // Se o serviço criar um ponto de restauração hoje
                if (File.Exists(Global.Strings.pontoEscrito))
                {
                    // Mostre a notificação
                    Global.MostrarNotificacao(
                        "Tudo certo, o Nottext Antivirus criou um ponto de restauração pra você.\r\n" + "Dia: " + readed
                    );

                    // Delete o arquivo, para não mostrar a notificação sempre que iniciar
                    File.Delete(Global.Strings.pontoEscrito);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Windows()
        {
            // Permita thread acessarem outros threads
            CheckForIllegalCrossThreadCalls = false;

            // FORM
            InitializeComponent();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            // Verifique a proteção
            Global.VerificarProtecao(Global.Strings.windowsProtection, restorePoint);

            // Saber se criou algum ponto de restauração, e então, mostrar uma notificação
            PointCreatedToday();
        }

        /// <summary>
        /// Ponto de restauração
        /// </summary>
        private void restorePoint_Click(object sender, EventArgs e)
        {
            // Se for para desabilitar a proteção
            if (restorePoint.Checked == false)
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a proteção de ponto de restauração?") == false)
                {
                    restorePoint.Checked = true;
                    return;
                }
            }

            // Ative ou desative a proteção
            Global.AtivarDesativarProtecao("RESTORED.set", restorePoint);
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Botão de vulnerabilidade
        /// </summary>
        public void vulnerability_Click(object sender, EventArgs e)
        {
            // Se for para desabilitar a proteção
            if (vulnerability.Checked == false)
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a proteção de vulnerabilidades?") == false)
                {
                    vulnerability.Checked = true;
                    return;
                }
            }

            // Ative ou desative a proteção
            Global.AtivarDesativarProtecao("vulnerability.set", vulnerability);
        }
    }
}
