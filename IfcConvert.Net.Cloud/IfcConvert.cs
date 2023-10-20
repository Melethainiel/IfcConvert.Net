using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;

namespace IfcConvert.Net.Cloud;

public class IfcConvert : IIfcConvert
{
    private readonly ToolSetting _options;

    private string DownloadUrl
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.Is64BitOperatingSystem
                        ? _options.Win64Url
                        : _options.Win32Url;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return _options.LinuxUrl;
            }

            throw new PlatformNotSupportedException();
        }
    }
    
    private static string ToolName
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "IfcConvert.exe";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "IfcConvert";
            }

            throw new PlatformNotSupportedException();
        }
    }

    private readonly Process _process;

    public IfcConvert(IOptions<ToolSetting> options)
    {
        _options = options.Value;
        _process = new Process();
        _process.StartInfo.FileName = $"Tool/{ToolName}";
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.RedirectStandardError = true;
    }
    
    public IfcConvert(ToolSetting options)
    {
        _options = options;
        _process = new Process();
        _process.StartInfo.FileName = $"Tool/{ToolName}";
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.RedirectStandardError = true;
    }
    
    private async Task Download()
    {
        if(File.Exists($"Tool/{ToolName}")) return;
        var downloadPath = Path.Combine(Path.GetTempPath(), "IfcConvert.zip");
        using var client = new HttpClient();
        using var response = await client.GetAsync(DownloadUrl);
        await using var stream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = File.Create(downloadPath);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
        ZipFile.ExtractToDirectory(downloadPath, "Tool");
        File.Delete(downloadPath);
    }

    public async Task Convert(
            string input,
            string output,
            Arguments arguments)
    {
        await Download();
        _process.StartInfo.Arguments = $"\"{input}\" \"{output}\" {arguments}";
        _process.Start();
        await _process.WaitForExitAsync();
    }
}