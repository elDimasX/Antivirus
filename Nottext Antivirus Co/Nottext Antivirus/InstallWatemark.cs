using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Nottext_Antivirus
{
    class InstallWatemark
    {
        // Arquivo para checkar se já foi instalado outra vez
        string arquivoJaInstalado = "C:\\Windows\\System32\\installedMaketark.set";

        // Por a FORM para frente
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        // Remove a marca dagua
        public async Task RemoverMarcaDagua()
        {
            try
            {
                if (!File.Exists(arquivoJaInstalado))
                {
                    File.Create(arquivoJaInstalado).Close(); // Cria o arquivo

                    // Inicia o arquivo
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Nottext\\Antivirus\\uwd.exe");

                    await Task.Delay(450); // Intervalo

                    // Processo
                    Process p = Process.GetProcessesByName("uwd").FirstOrDefault();
                    //Minimizar(); // Minimiza o processo

                    await Task.Delay(1000); // Intervalo

                    // Pega a FORM
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h); // Coloca a FORM com focus

                    // Aperta as KEYS
                    SendKeys.SendWait("{ENTER}");
                    await Task.Delay(1500);
                    SendKeys.SendWait("{ENTER}");
                    await Task.Delay(500);
                    SendKeys.SendWait("{ENTER}");


                    await Task.Delay(6000);
                }
            } catch (Exception) { }
        }
    }
}
