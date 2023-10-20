using Microsoft.Extensions.DependencyInjection;

namespace IfcConvert.Net.Embedded;

public static class ServiceInstaller
{
    public static void AddIfcConvert(this IServiceCollection services)
    {
        services.AddSingleton<IIfcConvert, IfcConvert>();
    }
}