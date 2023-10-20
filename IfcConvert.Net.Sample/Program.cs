// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var ifcConvert = new IfcConvert.Net.IfcConvert();
var argument = new ArgumentBuilder()
    .AutoYes()
    .Build();
await ifcConvert.Convert(args[0], args[1], argument);
