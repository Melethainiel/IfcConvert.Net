using IfcConvert.Net.Cloud;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IfcConvert.Net.Test;

public class UnitTest1
{
    [Fact]
    public async Task TestEmbedded()
    {
        IIfcConvert ifcConvert = new Embedded.IfcConvert();
        var input = "TestFiles/1.ifc";
        var output = "TestFiles/1.svg";
        var argument = new ArgumentBuilder().AutoYes()
                .Include()
                .Entities(new[] { "IfcFooting" })
                .SvgNoCss()
                .SectionHeightFromStoreys()
                .SectionHeight(-.1)
                .NoProgress()
                .Build();
        await ifcConvert.Convert(
                input,
                output,
                argument);
    }
    
    [Fact]
    public async Task TestCloud()
    {
        var services = new ServiceCollection();
        services.AddIfcConvert();
        
        
        IIfcConvert ifcConvert = new Cloud.IfcConvert(new ToolSetting());
        var input = "TestFiles/1.ifc";
        var output = "TestFiles/1.svg";
        var argument = new ArgumentBuilder().AutoYes()
                .Include()
                .Entities(new[] { "IfcFooting" })
                .SvgNoCss()
                .SectionHeightFromStoreys()
                .SectionHeight(-.1)
                .NoProgress()
                .Build();
        await ifcConvert.Convert(
                input,
                output,
                argument);
    }
}