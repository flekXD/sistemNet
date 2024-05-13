using System.Diagnostics;

class Program
{
    static void Main()
    {
        ExportRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Google", @"C:\Users\Fleks\sistemNet\lab4");
    }

    static void ExportRegistryKey(string key, string filePath)
    {
        Process.Start("regedit.exe", $"/E {filePath} {key}");
    }
}
