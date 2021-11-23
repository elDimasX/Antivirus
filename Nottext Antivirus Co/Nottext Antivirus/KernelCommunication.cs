
///
/// Arquivo com as funções e importações de DLL
///

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Nottext_Antivirus
{
    class KernelCommunication
    {
        /// <summary>
        /// Classe que vai conter as definiçõs de mensagens
        /// </summary>
        public static class CtlCodes
        {
            // Ativa a auto-proteção as operações
            public static uint ENABLE_SELF_PROTECT = KernelCommunication.CTL_CODE(
                KernelCommunication.FILE_DEVICE_UNKNOWN,
                0x1000,
                KernelCommunication.METHOD_BUFFERED,
                KernelCommunication.FILE_ANY_ACCESS
            );

            // Desativa a auto-proteção as operações
            public static uint DISABLE_SELF_PROTECT = KernelCommunication.CTL_CODE(
                KernelCommunication.FILE_DEVICE_UNKNOWN,
                0x1100,
                KernelCommunication.METHOD_BUFFERED,
                KernelCommunication.FILE_ANY_ACCESS
            );
        }

        /// <summary>
        /// Envia a mensagem ao kernel
        /// </summary>
        public static bool EnviarMensagem(string mensagem, uint ctl)
        {
            // Valor falso
            bool retornar = false;

            try
            {
                // Crie um arquivo, necessário para outras
                // Operações depois
                IntPtr device = (IntPtr)CreateFile(
                    "\\\\.\\NottextAntivirus", // Nome do dispositivo
                    GENERIC_READ | GENERIC_WRITE, // Escrita
                    FILE_SHARE_READ | FILE_SHARE_WRITE, // Escrita
                    IntPtr.Zero,
                    OPEN_EXISTING, // Abra o que já existe
                    0,
                    0
                );

                // Novos bytes
                int uCnt = 10;

                // Mensagem para o kernel
                StringBuilder enviarAoKernel = new StringBuilder(mensagem);

                // Mensagem para receber do kernel
                StringBuilder receberDoKernel = new StringBuilder();

                // Envie a mensagem para o kernel
                DeviceIoControl(
                    device, // Dispositivo
                    ctl, // Que tipo de mensagem
                    enviarAoKernel, // Mensagem para o kernel

                    // Tamanho da mensagem enviada, não sei porque, mas após eu adicionar
                    // + 5, o kernel sempre recebe a mensagem sem "lixo".
                    enviarAoKernel.Length + 5,

                    receberDoKernel, // Receber mensagem do kernel
                    1, // Length, só deixar 1 pra receber as mensagens

                    ref uCnt,
                    IntPtr.Zero
                );

                if (receberDoKernel.ToString() == "success!")
                {
                    retornar = true;
                }

                // Feche o dispositivo
                CloseHandle(
                    device
                );
            } catch (Exception) { }

            return retornar;
        }

        /// <summary>
        /// Importação da DLL KERNEL32.DLL CreateFile
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int CreateFile(
            String lpFileName, // Nome da porta
            int dwDesiredAccess, // Acesso
            int dwShareMode, // Compartilhamento
            IntPtr lpSecurityAttributes, // Security
            int dwCreationDisposition, // Disposition
            int dwFlagsAndAttributes, // Atributos
            int hTemplateFile // Arquivo
        );

        /// <summary>
        /// Fechar o dispositivo
        /// </summary>
        [DllImport("kernel32", SetLastError = true)]
        static extern bool CloseHandle(
            IntPtr handle // O que fechar
        );

        /// <summary>
        /// DeviceIoControl, necessário para receber e enviar mensagens
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int DeviceIoControl(
            IntPtr hDevice, // Dispositivo
            uint dwIoControlCode, // Control
            StringBuilder lpInBuffer, // Buffer
            int nInBufferSize, // BufferSize
            StringBuilder lpOutBuffer, // Outbuffer
            Int32 nOutBufferSize, // OutbufferSize
            ref Int32 lpBytesReturned, // Retorno
            IntPtr lpOverlapped //
        );

        // Definições, necessárias
        internal const uint FILE_DEVICE_UNKNOWN = 0x00000022;
        internal const uint FILE_ANY_ACCESS = 0;
        internal const uint METHOD_BUFFERED = 0;
        private const int GENERIC_WRITE = 0x40000000;
        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int OPEN_EXISTING = 3;
        private const int IOCTL_DISK_GET_DRIVE_LAYOUT_EX = unchecked((int)0x00070050);
        private const int ERROR_INSUFFICIENT_BUFFER = 122;

        /// <summary>
        /// CTL_CODE, necessário para o driver saber se queremos continuar operações
        /// </summary>
        public static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            // Retorne o valor
            return ((DeviceType << 16) | (Access << 14) | (Function << 2) | Method);
        }

    }
}
