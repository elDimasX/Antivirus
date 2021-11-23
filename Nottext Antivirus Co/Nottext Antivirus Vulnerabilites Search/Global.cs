using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nottext_Antivirus_Vulnerabilites_Search
{
    class Global
    {
        /// <summary>
        /// Todas as strings
        /// </summary>
        public static class Strings {

            // Verifica se a proteção está ou não habilitada
            public static string protecaoDeVulnerabilidade = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\vulnerability.set";

            // Notificações de vulnerabilidades
            public static string notificarVulnerabilidade = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\vulnerability.notify";

            // Quais programas já foram avisados, e o usuário ignorou (serve para não avisar novamente)
            public static string listaIgnorados = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\programs.notified";

            // Deletação pendente de pastas ou arquivos
            public static string deletacaoPendente = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\DELETINGPENTEND.set";
        }

    }
}
