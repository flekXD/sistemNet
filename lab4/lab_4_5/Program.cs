using Microsoft.Win32;
using System;

class Program
{
    static void Main()
    {
        AddToStartup("steam", @"D:\steam\steam.exe");
    }

    static void AddToStartup(string appName, string appPath)
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
        {
            key.SetValue(appName, appPath);
        }
    }
}
