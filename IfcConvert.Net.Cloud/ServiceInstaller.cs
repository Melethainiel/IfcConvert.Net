using Microsoft.Extensions.DependencyInjection;

namespace IfcConvert.Net.Cloud;

public static class ServiceInstaller
{
    public static void AddIfcConvert(this IServiceCollection services, Action<ToolSetting> option)
    {
        services.AddSingleton<IIfcConvert, IfcConvert>();
        services.Configure(option);
    }
    
    public static void AddIfcConvert(this IServiceCollection services)
    {
        services.AddIfcConvert(_ => { });
    }
}