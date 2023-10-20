namespace IfcConvert.Net;

public interface IIfcConvert
{
    Task Convert(
            string input,
            string output,
            Arguments arguments);
}