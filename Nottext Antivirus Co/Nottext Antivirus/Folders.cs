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
    public partial class Folders : Form
    {
        /// <summary>
        /// Carrega tudo
        /// </summary>
        public Folders()
        {
            InitializeComponent();

            // Aplique uma sombra
            ShadowForm.ApplyShadows(this);

            // Configure o cursor nos controles
            Global.SetHandCursor(this);
        }

        /// <summary>
        /// Botão de fechar
        /// </summary>
        private async void close_Click(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Fechar(this);
            Close();
        }

        /// <summary>
        /// Carregado
        /// </summary>
        private async void Folders_Load(object sender, EventArgs e)
        {
            // Efeito
            await Global.Styles.Iniciar(this);
        }
    }
}
