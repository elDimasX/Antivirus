///
/// Arquivo que contém as funções que escaneia a partir do kernel
///

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nottext_Antivirus
{
    class Kernel
    {
        // Aqui e o nome da porta para entramos e iniciarmos a comunicacao
        public static string portName = "\\FsFilterport";

        // Vamos salvar a porta de entrada aqui
        public static IntPtr port = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));

        // Fechar a porta de comunicação
        [DllImport("Kernel32")]
        internal extern static bool CloseHandle(IntPtr handle);

        // Filter communication, para se conectar a porta
        [DllImport("FltLib.dll", SetLastError = true)]
        internal extern static int FilterConnectCommunicationPort([MarshalAs(UnmanagedType.LPWStr)] string lpPortName, uint dwOptions, IntPtr lpContext, uint dwSizeOfContext, IntPtr lpSecurityAttributes, IntPtr hPort);

        /// <summary>
        /// Conectar-se ao kernel
        /// </summary>
        public async static void ConectarAoKernel()
        {
            // Espere iniciar o processo do kernel
            await Processos.IniciarProcesso("sc.exe", "start " + '"' + "Nottext Antivirus Driver Kernel" + '"');

            // Espere iniciar o processo do kernel
            await Processos.IniciarProcesso("sc.exe", "start " + '"' + "Nottext Antivirus Vulnerabilites Search" + '"');

            // Se conecte com o kernel
            FilterConnectCommunicationPort(portName, 0, IntPtr.Zero, 0, IntPtr.Zero, port);
        }

        // Este e o arquivo que o kernel nós enviara os arquivos
        // Para serem escaneados
        static string arquivoLer = "C:\\ProgramData\\Microsoft\\file.txt";

        // Arquivo onde o kernel anota as DLLS carregadas, ou imagens
        static string arquivoDLL = "C:\\ProgramData\\Microsoft\\LOADED.txt";

        /// <summary>
        /// Classe que vai conter as definiçõs de mensagens
        /// </summary>
        public class CtlCodes : Kernel
        {
            // Ativa a proteção em tempo real
            public static uint ENABLE_SELF_PROTECT = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x1000,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Continue as operações
            public static uint CONTINUE_OPERATION = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x2000,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Bloquear um arquivo
            public static uint LOCK_FILE = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x2100,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Continuar processo
            public static uint CONTINUE_PROCESS = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x3000,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Negar processo
            public static uint DENY_PROCESS = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x3100,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Força a exclusão de um arquivo
            public static uint FORCE_DELETE_FILE = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x3200,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );

            // Força a finalização de um processo
            public static uint FORCE_KILL_PROCESS = Communication.CTL_CODE(
                Communication.FILE_DEVICE_UNKNOWN,
                0x4000,
                Communication.METHOD_BUFFERED,
                Communication.FILE_ANY_ACCESS
            );
        }

        /// <summary>
        /// Bloqueia um arquivo
        /// </summary>
        public static bool BloquearArquivo(string arquivo)
        {
            // Diga ao driver pra bloquear esse arquivo
            return Communication.EnviarMensagem("\\??\\" + arquivo, CtlCodes.LOCK_FILE);
        }

        /// <summary>
        /// Força a exclusão de um arquivo
        /// </summary>
        public static bool ForcarDeletarArquivo(string arquivo)
        {
            // Diga ao driver para deletar o arquivo
            return Communication.EnviarMensagem("\\??\\" + arquivo, CtlCodes.FORCE_DELETE_FILE);
        }

        /// <summary>
        /// Proteção que o minifiltro analisa e envia pra nós
        /// </summary>
        public static void ProtecaoFiltro()
        {
            // Vamos criar um novo thread
            new Thread(async () =>
            {
                // Repetição infinita
                while (true)
                {
                    try
                    {
                        // Durma
                        await Task.Delay(10);

                        // Tipo de enconder, necessário para ler catacterias especiais
                        Encoding encode = Encoding.GetEncoding("iso-8859-1");

                        // Vamos ler linha por linha do arquivo de notificação em que
                        // O driver escreveu
                        foreach (string linha in File.ReadAllLines(arquivoLer, encode))
                        {
                            // Se o arquivo conter vírus
                            if (global.ArquivoContemMalware(linha, true))
                            {
                                try
                                {
                                    // Se for somente pra bloquear
                                    if (File.Exists(global.Strings.bloquearAmeaca))
                                    {
                                        // Bloqueie
                                        global.NotificarUsuario(linha, "Arquivo bloqueado");
                                        BloquearArquivo(linha);
                                    }
                                    
                                    // Se for a opção automaticamente
                                    else
                                    {
                                        // Remova o vírus
                                        global.RemoverVirus(linha, 0, false);
                                        BloquearArquivo(linha);
                                    }
                                } catch (Exception) { }
                            }
                        }
                    } catch (Exception) { }

                    // Continue a operação pendente no minifiltro
                    Communication.EnviarMensagem("", CtlCodes.CONTINUE_OPERATION);

                    try
                    {
                        // Terminamos, vamos deletar o arquivo de notificação
                        // Para que o driver continuar as outras operações
                        File.Delete(arquivoLer);
                    } catch (Exception) { }
                }

            }).Start(); // Inicie o thread
        }

        /// <summary>
        /// Analisa os processos que são abertos
        /// </summary>
        public static void ProtecaoProcessos()
        {
            // Vamos criar um novo thread
            new Thread(async () =>
            {
                // Repetição infinita
                while (true)
                {
                    try
                    {
                        // Durma
                        await Task.Delay(200);

                        // Tipo de enconder, necessário para ler catacterias especiais
                        Encoding encode = Encoding.GetEncoding("iso-8859-1");

                        // Vamos ler linha por linha do arquivo de notificação em que
                        // O driver escreveu
                        foreach (string linha in File.ReadAllLines(arquivoDLL, encode))
                        {
                            // Se o arquivo conter vírus
                            if (
                                global.ArquivoContemMalware(linha, true) ||
                                Escanear.EscanearCodigo(linha)
                            )
                            {
                                try
                                {
                                    // Negue o processo atual
                                    Communication.EnviarMensagem("", CtlCodes.DENY_PROCESS);

                                    // Remova o vírus
                                    global.RemoverVirus(linha, 0, false);
                                } catch (Exception) { }
                            }

                            // Se não conter malware
                            else
                            {
                                // Continue o processo
                                Communication.EnviarMensagem("", CtlCodes.CONTINUE_PROCESS);

                                try
                                {
                                    // Um novo thread
                                    new Thread(() =>
                                    {
                                        try
                                        {
                                            // Processo
                                            Process pr = Process.GetProcessesByName(linha)[0];

                                            // Escaneie o processo, pra verificar as DLLS
                                            // Que foram iniciado com ele
                                            Escanear.EscanearProcesso(pr, 500);
                                        }
                                        catch (Exception) { }
                                    }).Start();
                                }
                                catch (Exception) { }
                            }
                        }
                    } catch (Exception) { }

                    try
                    {
                        // Terminamos, vamos deletar o arquivo de notificação
                        // Para que o driver continuar as outras operações
                        File.Delete(arquivoDLL);
                    } catch (Exception) { }
                }

            }).Start(); // Inicie o thread
        }

    }
}


