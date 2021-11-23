using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nottext_Antivirus
{
    class Processos
    {
        // Importações para dll
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]

        // Suspender thread
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]

        // Resumir thread
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Configurações do Thread
        /// </summary>
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        /// <summary>
        /// Suspender processo
        /// </summary>
        public static async void SuspenderProcesso(int pid)
        {
            await Task.Delay(1); // Intervalo

            new Thread(async () =>
            {
                // Segundo plano
                Thread.CurrentThread.IsBackground = true;

                try
                {
                    await Task.Delay(1); // Intervalo
                    var processo = Process.GetProcessById(pid); // Processo

                    // Procura os threads no processo
                    foreach (ProcessThread threadProcesso in processo.Threads)
                    {
                        try
                        {
                            // Abre o thread
                            IntPtr abrirThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)threadProcesso.Id);

                            // Se o thread for 0
                            if (abrirThread == IntPtr.Zero)
                                continue; // Ignore apenas esse, mas continue o próximo thread

                            // Suspender o thread
                            SuspendThread(abrirThread);

                            // Fechar o thread
                            CloseHandle(abrirThread);
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception) { }
            }).Start();
        }

        /// <summary>
        /// Resumir processo
        /// </summary>
        public static async void ResumirProcesso(int pid)
        {
            await Task.Delay(1); // Intervalo

            new Thread(async () =>
            {
                // Segundo plano
                Thread.CurrentThread.IsBackground = true;

                try
                {
                    // Pega o arquivo e pasta
                    string arquivo = Process.GetProcessById(pid).MainModule.FileName.ToLower();
                    string pasta = Path.GetDirectoryName(arquivo).ToLower();

                    // Se estiver na pasta System32 ou for o explorer.exe (Windows 10 bug)
                    if (pasta.Contains("c:\\windows"))
                        return; // Não execute nada
                }
                catch (Exception) { }

                try
                {
                    await Task.Delay(1); // Intervalo
                    var processo = Process.GetProcessById(pid); // Processo

                    // Procura todos os threads do processo
                    foreach (ProcessThread threadProcesso in processo.Threads)
                    {
                        try
                        {
                            // Abrir o thread
                            IntPtr abrirThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)threadProcesso.Id);

                            // Se o thread for 0
                            if (abrirThread == IntPtr.Zero)
                                continue; // Ignore, mas continue no próximo thread

                            // Tempo de suspenção
                            var suspenderInt = 0;

                            do
                            {
                                // Resumir processo
                                suspenderInt = ResumeThread(abrirThread);
                            } while (suspenderInt > 0);

                            // Fechar o o thread
                            CloseHandle(abrirThread);
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception) { }
            }).Start();
        }

        /// <summary>
        /// Inicia um processo
        /// </summary>
        public static async Task IniciarProcesso(string nome, string argumento)
        {
            try
            {
                await Task.Delay(1);

                // Inicia um novo processo
                Process processo = new Process();

                // Nome do processo
                processo.StartInfo.FileName = nome;

                // Argumento do processo
                processo.StartInfo.Arguments = argumento;

                // Executar em segundo plano
                processo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // Execute como administrador
                processo.StartInfo.Verb = "runas";

                // Iniciar processo
                processo.Start();

                // Esperar o procesos acabar
                processo.WaitForExit();
            }
            catch (Exception) { }
        }

    }
}