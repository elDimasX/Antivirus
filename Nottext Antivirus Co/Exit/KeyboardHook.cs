using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Exit
{
    class KeyboardHook
    {
        /// <summary>
        /// Seta o gancho no teclado
        /// </summary>
        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            // Processo atual
            using (Process curProcess = Process.GetCurrentProcess())

            // Módulo atual
            using (ProcessModule curModule = curProcess.MainModule)
            {
                // Sete o gancho
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // Valores
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;
        public static LowLevelKeyboardProc _proc = HookCallback;
        public static IntPtr _hookID = IntPtr.Zero;

        // LowLevelKeyboardProc
        public delegate int LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Quando uma tecla for pressionada, chamarão essa função
        /// </summary>
        public static int HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            /*
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Console.WriteLine((Keys)vkCode);
            }
            */

            // Apenas, pare
            return 1;
            //else return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

    }
}
