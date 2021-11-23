///
/// Efeitos entre janelas
///

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    class Effects
    {
        /// <summary>
        /// Aqui guardaremos os efeitos
        /// </summary>

        public static int intervalo = 350;

        // Efeitos
        public enum Effect { Roll, Slide, Center, Blend }

        // Animação
        public static void Animate(Control ctl, Effect effect, int msec, int angle)
        {
            try
            {


                // Flags
                int flags = effmap[(int)effect];

                // Se estiver visível
                if (ctl.Visible) { flags |= 0x10000; angle += 180; }
                else
                {
                    // Se não
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException(); // Efeito
                }

                // Configure os agulos
                flags |= dirmap[(angle % 360) / 45];

                // Vamos fazer as animações
                bool ok = AnimateWindow(ctl.Handle, msec, flags);

                // Falhou na animação
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
            } catch (Exception) { }
        }

        // Efeitos
        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        // Importe a DLL
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

    }
}