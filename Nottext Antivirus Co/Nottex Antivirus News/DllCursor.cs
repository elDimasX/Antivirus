using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nottex_Antivirus__News
{
    class DllCursor
    {
        // Deixa o cursor em forma de mão
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

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
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}