///
/// Arquivo de banco de dados
///

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    class AtualizarBanco
    {
        /// <summary>
        /// Procurar por atualizações
        /// </summary>
        public static void AtualizarAplicativo()
        {
            try
            {
                // Pega a versão atual do produto
                string versaoAtual = Application.ProductVersion;

                // Nova web
                WebClient web = new WebClient();
                string localInstalar = Path.GetTempPath() + "Nottext AntiVirus-Ant";

                // Se o arquivo exitir, significa que ele veio de uma atualização
                try
                {
                    Directory.Delete(localInstalar);
                } catch (Exception) { }

                // Versão do aplicativo
                string versao = web.DownloadString(Global.Strings.versaoAtualizar);

                // Se a versão atual for igual a so site, não há atualização
                if (versao == versaoAtual)
                    return;

                // Mostre o erro
                Global.MostrarErro(
                    // Mensagem
                    "Há uma atualização disponível, iremos atualizar agora!",

                    // Sem cancelamento, vamos forçar a atualização
                    false
                );

                // Se a pasta não existir
                if (!Directory.Exists(localInstalar))
                {
                    // Crie a pasta
                    Directory.CreateDirectory(localInstalar);
                }

                // Link para download
                string linkOficial = web.DownloadString("https://pastebin.com/raw/MXra22SL");

                // Fazer o download do aplicativo
                web.DownloadFile(linkOficial, localInstalar + "\\x86.exe");

                // Iniciar a instalação
                Process.Start(localInstalar + "\\x86.exe");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Atualizar o banco de dados
        /// </summary>
        public static bool AtualizarBancoDeDados(bool verificarAplicativo)
        {
            try
            {
                // Se for pra verificar se há alguma atualização do aplicativo mais
                // Recente
                if (verificarAplicativo == true)
                {
                    // Verifique se o aplicativo está atualizado
                    AtualizarAplicativo();
                }

                // Pega a data
                string data = DateTime.Now.ToString("M/d/yyyy");

                // Se o arquivo não existir
                if (!File.Exists(Global.Strings.atualizarHoje))
                {
                    // Crie o arquivo
                    File.Create(Global.Strings.atualizarHoje).Close();
                }

                // Ler o arquivo
                string leu = File.ReadAllText(Global.Strings.atualizarHoje);

                // Se a data for igual ao arquivo
                if (leu == data)
                {
                    // O banco de dados já foi atualizado hoje, retorne sucesso
                    return true;
                }
                
                // Fazer o download do banco de dados
                try
                {
                    // Novo client
                    WebClient web = new WebClient();

                    // Lista de novo vórus
                    string db = web.DownloadString(Global.Strings.atualizarListaVirus);

                    // Escreva no arquivo
                    File.WriteAllText(Global.Strings.configuracoes + "relase.ini", db);
                }
                
                // Se falhar
                catch (Exception ex)
                {
                    // Mostre o erro, e retorne falso
                    Global.MostrarErro(ex.Message, false);
                    return false;
                }

                // Escreve no arquivo para não atualizar novamente
                File.WriteAllText(Global.Strings.atualizarHoje, data);

                // Atualizou o banco de dados
                return true;
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }

            // Não conseguiu atualizar o banco de dados
            return false;
        }
    }
}