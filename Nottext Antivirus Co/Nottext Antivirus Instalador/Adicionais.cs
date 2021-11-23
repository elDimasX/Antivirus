using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nottext_Antivirus_Instalador
{
    class Adicionais
    {
        // Pasta onde ficará os arquivos
        string pastaAtual = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Nottext\\Antivirus";

        /// <summary>
        /// Salvar as configurações do registro
        /// </summary>
        public async Task Registro()
        {
            try
            {
                // Pegando a versão do arquivo
                var versao = FileVersionInfo.GetVersionInfo(pastaAtual + "\\NottextAnt.exe");
                string versaoFinal = versao.ProductVersion;

                // Mostrar o desinstalador no painel de control
                RegistryKey configurar;

                // Cria a pasta onde armazena as configurações
                configurar = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Nottext Antivirus", true);

                // Mostra o icone no painel de controle
                configurar.SetValue("DisplayIcon", '"' + pastaAtual + "\\Nottext Antivirus Uinstall.exe" + '"', RegistryValueKind.String);

                // Mostra a versão
                configurar.SetValue("DisplayVersion", versaoFinal, RegistryValueKind.String);

                // Mostra o subtitlo (descrição)
                configurar.SetValue("Publisher", "Nottext Antivirus", RegistryValueKind.String);

                // Mostra o nome
                configurar.SetValue("DisplayName", "Nottext Antivirus", RegistryValueKind.String);

                // Configura o desinstalador
                configurar.SetValue("UninstallString", '"' + pastaAtual + "\\Nottext Antivirus Uinstall" + '"', RegistryValueKind.String);

            } catch (Exception) { }
            await Task.Delay(500);
        }

        /// <summary>
        /// Iniciar um programa com argumentos
        /// </summary>
        private void IniciarPrograma(string nome, string argumento)
        {
            // Novo process
            Process.Start(new ProcessStartInfo()
            {
                // Nome
                FileName = nome,

                // Argumento
                Arguments = argumento,

                // Processo oculto
                WindowStyle = ProcessWindowStyle.Hidden,

                // Sem criar janelas
                CreateNoWindow = true
            });
        }

        /// <summary>
        /// Agendar a tarefa
        /// </summary>
        public async Task Agendador()
        {
            try
            {
                // Aguarde
                await Task.Delay(1);

                // Programa o evento no agendador de tarefas
                IniciarPrograma(
                    "schtasks.exe",
                    "/create /f /rl HIGHEST /sc ONEVENT /tn Nottext /tr " + '"' + "'" + pastaAtual + "\\NottextAnt.exe'" + '"' + " /ec APPLICATION"
                );
            } catch (Exception) { }
        }
    }
}