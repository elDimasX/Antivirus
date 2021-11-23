using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nottext_Antivirus_Uinstall
{
    class Necessarios
    {
        /// <summary>
        /// Desisntalar as configurações do registro
        /// </summary>
        public async Task DesinstalarRegistro()
        {
            // Remover o icone do menu iniciar
            await Task.Delay(2);
            try
            {
                File.Delete("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Nottext Antivirus.lnk");
            } catch (Exception) { }

            // Remover o icone da área de trabalho
            try
            {
                File.Delete("C:\\Users\\Public\\Desktop\\Nottext Antivirus.lnk");
            } catch (Exception) { }

            // Remover o 3D Tools do controle de painel
            try
            {
                // Nova key
                RegistryKey configurar;
                configurar = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", true);

                // Deletar a chave
                configurar.DeleteSubKey("Nottext Antivirus");
            } catch (Exception) { }
        }
    }
}