namespace IfcConvert.Net.Test;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var ifcConvert = new IfcConvert();
        var input = "TestFiles/1.ifc";
        var output = "TestFiles/1.svg";
        var argument = new ArgumentBuilder()
            .AutoYes()
            .Include()
            .Entities(new []{"IfcWall", "IfcColumn"})
            .Build();
        await ifcConvert.Convert(input, output, argument);
    }
}