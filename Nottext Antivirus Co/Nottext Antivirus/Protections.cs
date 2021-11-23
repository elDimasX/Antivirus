using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Protections : Form
    {
        FileProtection pt = new FileProtection();
        Exceptions ex = new Exceptions();

        /// <summary>
        /// Altera o checkbox da FORM FileProtection
        /// </summary>
        public void HabilitarTempoReal(bool habilitar)
        {
            pt.realTimeProtection.Checked = habilitar;
        }

        /// <summary>
        /// Altera a checkbox do kernel
        /// </summary>
        public void HabilitarKernel(bool habilitar)
        {
            pt.kernelProtection.Checked = habilitar;
        }

        /// <summary>
        /// Altera a checkbox da leitura profunda
        /// </summary>
        public void HabilitarLeitura(bool habilitar)
        {
            pt.advancedScan.Checked = habilitar;
        }

        /// <summary>
        /// Status da checkbox do kernel
        /// </summary>
        public bool StatusKernel()
        {
            return pt.kernelProtection.Checked;
        }

        /// <summary>
        /// Retorna o status da leitura profunda
        /// </summary>
        /// <returns></returns>
        public bool StatusLeituraProfunda()
        {
            return pt.advancedScan.Checked;
        }

        /// <summary>
        /// Quando iniciado
        /// </summary>
        public Protections()
        {
            InitializeComponent();

            pt.TopLevel = false;
            forms.Controls.Add(pt);

            ex.TopLevel = false;
            forms.Controls.Add(ex);

            Global.SetHandCursor(this);
        }

        /// <summary>
        /// Quando carregar
        /// </summary>
        private async void Protections_Load(object sender, EventArgs e)
        {
            await Task.Delay(50);
            Effects.Animate(pt, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        private void protectionsForm_CheckedChanged(object sender, EventArgs e)
        {
            if (protectionsForm.Checked == true)
            {
                Effects.Animate(pt, Effects.Effect.Slide, Effects.intervalo, 360);
            } else
            {
                Effects.Animate(pt, Effects.Effect.Slide, Effects.intervalo, 180);
            }
        }

        private void exceptionsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (protectionsForm.Checked == true)
            {
                Effects.Animate(ex, Effects.Effect.Slide, Effects.intervalo, 360);
            }
            else
            {
                Effects.Animate(ex, Effects.Effect.Slide, Effects.intervalo, 180);
            }
        }
    }
}
