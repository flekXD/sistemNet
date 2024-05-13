using Microsoft.Win32;
using System;

class Program
{
    static void Main()
    {
        PrintAutoStartPrograms(Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"));
        PrintAutoStartPrograms(Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"));
    }

    static void PrintAutoStartPrograms(RegistryKey regKey)
    {
        foreach (string name in regKey.GetValueNames())
        {
            Console.WriteLine($"{name} : {regKey.GetValue(name)}");
        }
    }
}
