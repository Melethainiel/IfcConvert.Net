namespace IfcConvert.Net.Cloud;

public sealed class ToolSetting
{
    public string Win64Url { get; set; } = "https://s3.amazonaws.com/ifcopenshell-builds/IfcConvert-v0.7.0-f0e03c7-win64.zip";
    public string Win32Url { get; set; } = "https://s3.amazonaws.com/ifcopenshell-builds/IfcConvert-v0.7.0-f0e03c7-win32.zip";
    public string LinuxUrl { get; set; } = "https://s3.amazonaws.com/ifcopenshell-builds/IfcConvert-v0.7.0-f0e03c7-linux64.zip";
}