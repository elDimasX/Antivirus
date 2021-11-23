///
/// Arquivo de SOBRE
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class About : Form
    {
        /// <summary>
        /// Inicia tudo
        /// </summary>
        public About()
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
        /// Quando for iniciado
        /// </summary>
        private async void About_Load(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Iniciar(this);
        }
    }
}
