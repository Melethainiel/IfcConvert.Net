using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IfcConvert.Net;

public class IfcConvert
{
    private static string ToolName
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.Is64BitOperatingSystem ? "IfcConvert64.exe" : "IfcConvert32.exe";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "IfcConvert";
            }

            throw new PlatformNotSupportedException();
        }
    }

    private readonly Process _process;

    public IfcConvert()
    {
        _process = new Process();
        _process.StartInfo.FileName = $"Tool/{ToolName}";
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.RedirectStandardError = true;
    }

    public async Task Convert(
        string input,
        string output,
        Arguments arguments)
    {
        //run process of IfcConvert.exe from Tool folder
        _process.StartInfo.Arguments = $"\"{input}\" \"{output}\" {arguments}";
        _process.Start();
        await _process.WaitForExitAsync();
    }
}