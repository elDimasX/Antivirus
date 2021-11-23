///
/// Arquivo de escaneamento, escaneaia arquivos
/// 

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Nottext_Antivirus
{
    class Escanear
    {
        /// <summary>
        /// Escaneia um processo
        /// </summary>
        public async static void EscanearProcesso(Process processo, int intervalo)
        {
            try
            {
                // Local do processo
                string local = processo.MainModule.FileName;

                // Escaneie o arquivo
                EscanearArquivo(local, true, processo.Id);

                // Agora, escaneie todas os modulos no processo
                foreach (ProcessModule dll in processo.Modules)
                {
                    // Espere o intervalo
                    await Task.Delay(intervalo);

                    try
                    {
                        // Escaneie o módulo
                        EscanearArquivo(dll.FileName, true, processo.Id);
                    } catch (Exception) { }
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Caso escanear um processo falhe, faça um modo mais avançado
        /// </summary>
        public static void EscanearArquivoNovamente(string arquivo)
        {
            try
            {
                // Se a pasta de copiar não existir
                if (!Directory.Exists(global.Strings.escanearNovamente))
                {
                    // Crie a pasta
                    Directory.CreateDirectory(global.Strings.escanearNovamente);
                }

                // Arquivo que vai ser copiado
                string arquivoCopiar = global.Strings.escanearNovamente + "ALEATORY" + Path.GetFileName(arquivo + ".NOTTEXT");

                try
                {
                    // Caso um arquivo já exista com esse nome, apague ele antes de copiar o novo
                    File.Delete(arquivoCopiar);
                }  catch (Exception) { }

                // Copia o arquivo para um lugar seguro
                File.Copy(arquivo, arquivoCopiar);

                // Verifique se o arquivo copiado contém malware
                if (global.ArquivoContemMalware(arquivoCopiar, false))
                {
                    try
                    {
                        // Delete o arquivo para não acumular espaço no HD
                        File.Delete(arquivoCopiar);
                    } catch (Exception) { }

                    // Remova o vírus
                    global.RemoverVirus(arquivo, 0, true);
                }

                try
                {
                    // Delete o arquivo para não acumular espaço no HD
                    File.Delete(arquivoCopiar);
                } catch (Exception) { }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Escanear um arquivo
        /// </summary>
        public static void EscanearArquivo(string arquivo, bool adicionarExecao, int pid)
        {
            try
            {
                // Se a extensão do arquivo for NOTTEXT
                if (Path.GetExtension(arquivo) == ".NOTTEXT")
                {
                    // Pare
                    return;
                }

                // Se o arquivo conter malware
                if (global.ArquivoContemMalware(arquivo, adicionarExecao))
                {
                    // Remova o arquivo
                    global.RemoverVirus(arquivo, pid, true);
                }
            }
            catch (Exception) // Caso não consiga escanear o arquivo
            {
                // Faça um escan mais profundo
                EscanearArquivoNovamente(arquivo);
            }
        }

        /// <summary>
        /// Escaneia um arquivo lendo o código fonte, usado em arquivos .BAT
        /// </summary>
        public static bool EscanearBAT(string arquivo)
        {
            try
            {
                // Código do arquivo do arquivo
                string codigo = File.ReadAllText(arquivo).ToLower();

                // Se conter algo que delete silenciosamente e forçado
                if (
                    // Valores que queremos bloquear
                    codigo.Contains("/s") ||
                    codigo.Contains("-s") ||

                    codigo.Contains("/f") ||
                    codigo.Contains("-f") ||

                    codigo.Contains("goto") ||
                    codigo.Contains("set") ||
                    
                    codigo.Contains("csscript"))
                {
                    // Contém um malware
                    return true;
                }
            }
            catch (Exception) { }

            // Não há vírus
            return false;
        }

        /// <summary>
        /// Verifica se o arquivo é assinado
        /// </summary>
        public static bool Assinado(string arquivo)
        {
            try
            {
                // Obtenha o certificado do arquivo
                X509Certificate arquivoCert = X509Certificate.CreateFromSignedFile(arquivo);

                // Obtenha o certificado do arquivo
                X509Certificate2 certificado = new X509Certificate2(arquivoCert);

                // Se o arquivo estiver assinado
                if (certificado != null)
                {
                    // Retorne verdadeiro
                    return true;
                }
            }
            catch (Exception) { }

            // Falso
            return false;
        }

        /// <summary>
        /// Escaneia um arquivo usando engenharia reversa simples
        /// </summary>
        public static bool EscanearCodigo(string arquivo)
        {
            bool existe = false;

            try
            {
                existe = File.Exists(global.Strings.leituraProfunda);
            } catch (Exception) { }

            try
            {
                // Se a proteção não estiver habilitada
                if (global.Strings.ProtecaoHabilita == false || global.ListaBranca(arquivo) == true)
                {
                    // Retorne falso
                    return false;
                }

                // Se o arquivo não estiver assinado
                if (!Assinado(arquivo) && !existe)
                {
                    // Leia o arquivo
                    StreamReader ler = new StreamReader(arquivo);

                    // Todo o contéudo do arquivo
                    string conteudo = ler.ReadToEnd();

                    // Substitua bytes nulos por espaços, e depois, remova outros espaços
                    conteudo = conteudo.Replace("\x0", "").Replace(" ", "").ToLower();

                    // Feche o arquivo
                    ler.Close();

                    // Se conter algo malicioso
                    if (
                        conteudo.Contains("\\currentversion\\imagefileexecutionoptions\\"
                    ))
                    {
                        try
                        {
                            // Adicione o HASH na memória que contém os hashes dos vírus
                            global.Strings.db = global.Strings.db + global.ObterMD5(arquivo);
                        } catch (Exception) { }

                        // Contém algo malicioso
                        return true;
                    }
                }
            } catch (Exception) { }

            // Nada encontrado
            return false;
        }

        /// <summary>
        /// Escaneia uma pasta
        /// </summary>
        public static async Task EscanearPasta(DirectoryInfo pasta, string arquivo, bool verificacao)
        {
            await Task.Delay(1); // Intervalo

            try
            {
                // Pega as pastas
                foreach (DirectoryInfo subPasta in pasta.GetDirectories())
                {
                    // Se o arquivo não existir, a verificação parou, mas também
                    // Tem que ser uma verificação
                    if (verificacao == true && !File.Exists(arquivo))
                    {
                        // Pare o verificação
                        break;
                    }

                    // Procura outro subdiretorio
                    await EscanearPasta(subPasta, arquivo, verificacao);
                }
            }
            catch (Exception) { }

            try
            {
                // Procura todos os arquivos
                foreach (FileInfo arquivosInf in pasta.GetFiles())
                {
                    // Se o arquivo não existir, a verificação parou, mas também
                    // Tem que ser uma verificação
                    if (verificacao == true && !File.Exists(arquivo))
                    {
                        // Pare o verificação
                        break; 
                    }

                    // Escaneie o arquivo
                    EscanearArquivo(arquivosInf.FullName, false, 0);
                }
            }
            catch (Exception) { }
        }

    }
}