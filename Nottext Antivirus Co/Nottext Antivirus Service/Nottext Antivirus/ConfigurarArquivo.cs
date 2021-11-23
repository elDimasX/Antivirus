///
/// Configurações de arquivo, como desbloquear e outros
///

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Nottext_Antivirus
{
    class ConfigurarArquivo
    {
        // Configurar a DLL de deletar após a reinicialização
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, int dwFlags);

        /// <summary>
        /// Bloqueia a execução do arquivo na ACL
        /// </summary>
        public static async Task BloquearACL(string arquivos)
        {
            try
            {
                try
                {
                    // Vamos bloquear o arquivo
                    FileStream s2 = new FileStream(
                        arquivos, // Este é o arquivo
                        FileMode.Open, // Nos vamos abrir
                        FileAccess.Read, // Com permissão de leitura

                        // Não vamos compartilhar NENHUMA permissão para os outros processos
                        FileShare.None
                        );
                } catch (Exception) { }

                await Task.Delay(1); // Intervalo

                // Pega a segurança atual do arquivo
                FileSecurity seguranca = File.GetAccessControl(arquivos);

                // Pega o grupo "Todos"
                SecurityIdentifier todos = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                // Nega o acesso de ler e executar
                seguranca.AddAccessRule(new FileSystemAccessRule(todos, FileSystemRights.ReadAndExecute, AccessControlType.Deny));

                // Aplica a ACL
                File.SetAccessControl(arquivos, seguranca);
            }
            catch (Exception) // Se falhar, bloqueie o arquivo pelo Regedit
            {
            }
        }

        /// <summary>
        /// Forçar a finalização de processos relacionados ao arquivo
        /// </summary>
        public async static void Desbloquear(string arquivo)
        {
            // Garante acesso total ao arquivo
            try
            {
                // Nova chave de segurança
                FileSecurity seguranca = new FileSecurity();

                // Acesso total ao arquivo que o usuário está conectado
                FileSystemAccessRule regras = new FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Allow);

                // Adiciona a ACL ao arquivo
                seguranca.AddAccessRule(regras);
                File.SetAccessControl(arquivo, seguranca);
                await Task.Delay(50);
            } catch (Exception) { }

            // Remover o somente leitura
            try
            {
                FileInfo info = new FileInfo(arquivo); // Pega as informações do arquivo
                info.IsReadOnly = false; // Remove o modo somente leitura
            } catch (Exception) { }

            // Intervalo
            await Task.Delay(50);

            try
            {
                // Liste os processos dos arquivos abertos selecionados
                List<Process> aberto = Unlock.FindLockers(arquivo);

                // Procure pelos processos
                foreach (Process processo in aberto)
                {
                    try
                    {
                        // Suspenda o processo antes de tentar mata-ló
                        Processos.SuspenderProcesso(processo.Id);
                    } catch (Exception) { }
                }

                // Agora, procure os processos novamente
                foreach (Process processo in aberto)
                {
                    try
                    {
                        // Só continue caso o processo não esteja na pasta do antivírus
                        if (!processo.ProcessName.Contains("Nottext Antivirus"))
                        {
                            // Tente matar o procesos aberto
                            processo.Kill();
                        }
                    } catch (Exception) { }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Deletar arquivos depois da reinicialização
        /// </summary>
        public static void DeletarReiniciar(string arquivo, string quarentena)
        {
            try
            {
                // Move o arquivo para a quarentena após a reinicialização
                MoveFileEx(arquivo, quarentena, 0x4);
            } catch (Exception) { }
        }

    }
}