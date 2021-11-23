using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Nottext_Antivirus
{
    class Unlock
    {

        private const int RmRebootReasonNone = 0;
        private const int CCH_RM_MAX_APP_NAME = 255;
        private const int CCH_RM_MAX_SVC_NAME = 63;

        [StructLayout(LayoutKind.Sequential)]
        struct RM_UNIQUE_PROCESS
        {
            public int dwProcessId;
            public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct RM_PROCESS_INFO
        {
            public RM_UNIQUE_PROCESS Process;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)]
            public string strAppName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)]
            public string strServiceShortName;
            public RM_APP_TYPE ApplicationType;
            public uint AppStatus;
            public uint TSSessionId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bRestartable;
        }

        enum RM_APP_TYPE
        {
            RmUnknownApp = 0,
            RmMainWindow = 1,
            RmOtherWindow = 2,
            RmService = 3,
            RmExplorer = 4,
            RmConsole = 5,
            RmCritical = 1000
        }

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int RmRegisterResources(uint pSessionHandle,
            UInt32 nFiles, string[] rgsFilenames,
            UInt32 nApplications,
            [In] RM_UNIQUE_PROCESS[] rgApplications,
            UInt32 nServices, string[] rgsServiceNames);

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int RmStartSession(out uint pSessionHandle, int dwSessionFlags,
            string strSessionKey);

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int RmEndSession(uint pSessionHandle);

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int RmGetList(uint dwSessionHandle, out uint pnProcInfoNeeded,
            ref uint pnProcInfo,
            [In, Out] RM_PROCESS_INFO[] rgAffectedApps,
            ref uint lpdwRebootReasons);

        static public List<Process> FindLockers(string filename)
        {
            uint session_handle;
            string session_key = Guid.NewGuid().ToString();
            int result = RmStartSession(out session_handle, 0, session_key);
            if (result != 0)
                throw new Exception("Error " +
                    result + " starting a Restart Manager session.");

            List<Process> processes = new List<Process>();
            try
            {
                const int ERROR_MORE_DATA = 234;
                uint pnProcInfoNeeded = 0, num_procs = 0,
                    lpdwRebootReasons = RmRebootReasonNone;
                string[] resources = new string[] { filename };
                result = RmRegisterResources(
                    session_handle, (uint)resources.Length,
                    resources, 0, null, 0, null);
                if (result != 0)
                    throw new Exception("Could not register resource.");

                result = RmGetList(session_handle, out pnProcInfoNeeded,
                    ref num_procs, null, ref lpdwRebootReasons);
                if (result == ERROR_MORE_DATA)
                {
                    RM_PROCESS_INFO[] processInfo =
                        new RM_PROCESS_INFO[pnProcInfoNeeded];
                    num_procs = pnProcInfoNeeded;

                    result = RmGetList(session_handle,
                        out pnProcInfoNeeded, ref num_procs,
                        processInfo, ref lpdwRebootReasons);
                    if (result != 0)
                        throw new Exception("Error " + result +
                            " listing lock processes");

                    for (int i = 0; i < num_procs; i++)
                    {
                        try
                        {
                            processes.Add(
                                Process.GetProcessById(processInfo[i].
                                    Process.dwProcessId));
                        }
                        catch (ArgumentException) { }
                    }
                }
                else if (result != 0)
                    throw new Exception("Error " + result +
                        " getting the size of the result.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error execpt");
            }
            finally
            {
                RmEndSession(session_handle);
            }
            return processes;
        }

    }
}
