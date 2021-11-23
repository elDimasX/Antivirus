///
/// Arquivo para string globais, funções e etc
///

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nottext_Antivirus
{
    class global
    {
        /// <summary>
        /// Local para string globais
        /// </summary>
        public class Strings : global
        {
            // Verifica se a proteção está habilitada ou não
            public static bool ProtecaoHabilita = false;

            // Local do processo atual
            public static string servico = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Bloquear a ameaça em vez de remove-la
            public static string bloquearAmeaca = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\blockThreat.set";

            // Lista de excções
            public static string execoes = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\EXECPTION.set";

            // Leitura profunda
            public static string leituraProfunda = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\advancedScan.set";

            // Pasta onde a função "Escanear Novamente" vai copiar os arquivos
            public static string escanearNovamente = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\COPIED\\";

            // Arquivo de banco de dados
            public static string arquivoDB = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\database.ini";

            // Arquivo de banco de dados atualizando, quando o user-mode atualiza o DB
            public static string arquivoDBUpdate = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\relase.ini";

            // A proteção de kernel está ativada ou não
            public static bool kernelAtivado = false;

            // Verificação rápida
            public static string verRapida = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\FAST.set";

            // Verificação rápida
            public static string verCompleta = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\COMPLETE.set";

            // Arquivo da proteção em tempo real
            public static string protecaoTempoRealArquivo = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\realTime.set";

            // Interface (Nottext)
            public static string interfaceAntivirus =  

                Environment.GetFolderPath(
                // Pasta de programFiles x86
                Environment.SpecialFolder.ProgramFilesX86 ) + 

                // Mais o arquivo NottextAnt
                "\\Nottext\\Antivirus\\NottextAnt.exe".ToLower();

            // Pasta das configurações
            public static string pasta = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs";

            // Pasta de quarentena
            public static string quarentena = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Quarentine\\";

            // Arquivo de escan
            public static string arquivoEscan = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\SCAN.set";

            // Arquivo de notificação
            public static string arquivoNotify = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\NOTIFY.set";

            // Arquivo para saber que dia foi que o ponto de restauração foi feito
            public static string escritoRestauracao = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\WRITED.set";

            // Arquivo para saber se a proteção de Windows está ativada
            public static string pontoDeRestauracao = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\RESTORED.set";

            // Arquivo para enviar ao user-mode sobre a proteção de Windows, ela notifica
            // Que o ponto de restaurado foi criado com êxito
            public static string pontoCriado = "C:\\ProgramData\\Nottext\\Nottext Antivirus\\Configs\\CREATEDPOINT.set";

            // Banco de dados
            public static string db = File.ReadAllText(arquivoDB);
            public static string dbUpdate = "";
        }

        /// <summary>
        /// Pegar a string MD5
        /// </summary>
        public static string ObterMD5(string arquivo)
        {
            // MD5 que vamos retornar
            string md5Return = "Its not possible get MD5 file";

            try
            {
                // Abre o arquivo em modo de MD5
                var sha512 = MD5.Create();

                // Abre o arquivo
                FileStream stream = new FileStream(
                    arquivo, // Nome do arquivo
                    FileMode.Open, // Abra o arquivo
                    FileAccess.Read, // Leitura
                    FileShare.Delete | FileShare.ReadWrite // Podem escrever e deletar
                    );

                // Lê o hash do arquivo
                var hashArquivo = sha512.ComputeHash(stream);

                // Substitua algumas letras
                md5Return = BitConverter.ToString(hashArquivo).Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "").Replace("-", "").ToLower(); // Hash do arquivo

                stream.Close(); // Fecha o arquivo
            } catch (Exception) { }

            // Retorne o MD5
            return md5Return;
        }

        /// <summary>
        /// Função que lê o arquivo rapidamente, e procura hash
        /// </summary>
        public static bool ContainsLine(
            // Palavra pra procurar
            string hash
        )
        {
            // Retorne o que achou
            return Strings.db.Contains(hash) || Strings.dbUpdate.Contains(hash);
        }

        /// <summary>
        /// Tente bloquear um arquivo de todas as maneiras possíveis, isso serve
        /// Para casos urgentes
        /// </summary>
        private static async void BloquearTudo(string arquivo)
        {
            try
            {
                // Bloqueie o arquivo com o kernel
                ConfigurarArquivo.Desbloquear(arquivo);
                Kernel.BloquearArquivo(arquivo);

                // Bloquear a execução do arquivo
                await ConfigurarArquivo.BloquearACL(arquivo);

                // Delete o arquivo após a reinicialização
                ConfigurarArquivo.DeletarReiniciar(arquivo, null);

                NotificarUsuario(arquivo, "Execução bloqueada");
            } catch (Exception) { }
        }

        /// <summary>
        /// Função para remover um malware
        /// </summary>
        public static async void RemoverVirus(string arquivo, int pid, bool desbloquear)
        {
            try
            {
                // Mate o processo com o pid que foi enviado
                Process.GetProcessById(pid).Kill();
            } catch (Exception) { }

            // Se for pra desbloquear
            if (desbloquear == true)
            {
                // Forçar a desbloquear o arquivo
                ConfigurarArquivo.Desbloquear(arquivo);
            }

            // Se for somente pra bloquear
            if (File.Exists(Strings.bloquearAmeaca))
            {
                // Bloqueie
                if (Kernel.BloquearArquivo(arquivo) == true)
                {
                    NotificarUsuario(arquivo, "Arquivo bloqueado");
                    return;
                }
            }

            // Nome do arquivo
            string nome = Path.GetFileName(arquivo);

            // Local do arquivo em quarentena
            string quarentenaLocal = Strings.quarentena + nome + ".NOTTEXT";

            try
            {
                // Se já existir o arquivo na quarentena
                if (File.Exists(quarentenaLocal))
                {
                    // Deleta o arquivo atual, pois já existe um em quarentena
                    if (Kernel.ForcarDeletarArquivo(arquivo) == true)
                    {
                        NotificarUsuario(arquivo, "Removido do computador");
                        return;
                    }

                    else
                    {
                        try
                        {
                            // Se o arquivo ainda existir, o kernel falhou em deletar
                            // O arquivo
                            if (File.Exists(arquivo))
                            {
                                File.Delete(arquivo);
                                NotificarUsuario(arquivo, "Removido do computador");

                                return;
                            }
                        }
                        catch (Exception) { }

                        // Se o arquivo ainda existir
                        if (File.Exists(arquivo))
                        {
                            // Tente o modo de emergência
                            BloquearTudo(arquivo);
                            return;
                        }
                    }


                }
                else
                {
                    try
                    {
                        // Move o arquivo criptografado para a quarentena
                        File.Move(arquivo, quarentenaLocal);
                    }
                    catch (Exception) { }

                    // Se não conseguir mover o arquivo
                    if (!File.Exists(quarentenaLocal))
                    {

                        try
                        {
                            // Tente copiar o arquivo
                            File.Copy(arquivo, quarentenaLocal);
                        } catch (Exception) { }

                        // Se o arquivo ainda não existir, não conseguimos criar um
                        // Backup na quarentena, logo, precisamos extermina-ló
                        if (!File.Exists(quarentenaLocal))
                        {
                            // Tente deletar o arquivo
                            if (Kernel.ForcarDeletarArquivo(arquivo) == true)
                            {
                                NotificarUsuario(arquivo, "Removido do computador");
                                return;
                            }
                            else
                            {
                                // Tente o modo de emergência
                                BloquearTudo(arquivo);
                                return;
                            }
                        }
                    }
                }
                
                await Task.Delay(100);

                // Se o processo for executado mesmo com a extensão .NOTTEXT, mate-o
                ConfigurarArquivo.Desbloquear(quarentenaLocal);

                NotificarUsuario(arquivo, "Removido para quarentena");
            }
            catch (Exception)
            {
                // Tente o modo de emergência
                BloquearTudo(arquivo);
            }
        }

        /// <summary>
        /// Verifica se o usuário deixou um arquivo na lista branca
        /// </summary>
        public static bool ListaBranca(string arquivo)
        {
            try
            {
                if (!File.Exists(Strings.execoes))
                    return false;

                // Pegue todas as linhas
                string[] linhas = File.ReadAllLines(Strings.execoes);

                // Leia linha por linha do arquivo de exeções
                foreach (string linha in linhas)
                {
                    try
                    {
                        if (linha.Length > 0)
                        {
                            // Se a linha for igual ao arquivo
                            if (arquivo.ToLower().Contains(linha.ToLower()))
                            {
                                return true; // Contem exceção
                            }
                        }
                    } catch (Exception) { }
                }
            } catch (Exception) { }

            // Não foi possível encontrar exeções
            return false;
        }

        /// <summary>
        /// Lista que vai conter os arquivos já escaneados, nós agiliza
        /// O processo
        /// </summary>
        static List<string> JaEscaneados = new List<string>();

        /// <summary>
        /// Escaneia um arquivo e diz se ele contém malware ou não
        /// </summary>
        public static bool ArquivoContemMalware(string arquivo, bool adicionarExecao)
        {
            try
            {
                // Se a proteção não estiver habilitada
                if (Strings.ProtecaoHabilita == false)
                {
                    // Retorne falso
                    return false;
                }

                // Se este arquivo já foi escaneado
                if (ListaBranca(arquivo) == true || JaEscaneados.Contains(arquivo))
                {
                    // Se ele deseja adicionar exeções, ele também deseja verificar
                    // Se o arquivo já está nas exeções, se sim, devemos parar, pois
                    // O arquivo está em alguma exeção
                    if (adicionarExecao == true)
                    {
                        // Retorne falso
                        return false;
                    }
                }

                string extension = Path.GetExtension(arquivo).ToLower();

                // Se for um arquivo BAT
                if (extension == ".bat" || extension == ".cmd" || extension == ".vbs")
                {
                    // Retorne o valor do "EscanearBAT", que escaneia arquivos
                    // .BAT, .CMD e .VBS
                    return Escanear.EscanearBAT(arquivo);
                }

                // Obtenha o MD5 do arquivo
                string hashAb = ObterMD5(arquivo);

                // Se o arquivo conter malware
                if (ContainsLine(hashAb))
                {
                    // Retorne verdadeiro
                    return true;
                }

                // Se não conter malware
                else
                {
                    // Se for para adicionar exeções
                    if (adicionarExecao == true)
                    {
                        // Adicione o arquivo ás exeções
                        JaEscaneados.Add(arquivo + "\r\n");
                    }
                }
            } catch (Exception) { }

            // Retorne falso, o arquivo não contém vírus
            return false;
        }

        /// <summary>
        /// Notifica o user-mode
        /// </summary>
        public static void NotificarUsuario(string arquivo, string status)
        {
            try
            {
                File.WriteAllText(
                    // Arquivo
                    Strings.arquivoNotify,

                    // Mensagem
                    "O Nottext Antivirus detectou e bloqueou uma ameaça:\r\n\r\n" +

                    // Arquivo
                    "Arquivo: " + Path.GetFileName(arquivo).ToUpper() + "\r\n" +

                    // Status
                    "Status: " + status + "\r\n" +

                   // Data
                   "Horário: " + DateTime.Now.ToString("HH:mm")
                );
            } catch (Exception) { }
        }
    }
}
