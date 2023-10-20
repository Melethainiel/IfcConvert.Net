using System.Globalization;
using System.Text;

namespace IfcConvert.Net;

public readonly struct Arguments
{
    public bool AutoYes { get; init; }
    public bool NoProgress { get; init; }
    public bool No2dBoolean { get; init; }
    public FilterType IncludeFilterType { get; init; }
    public HashSet<string>? IncludeFilterValues { get; init; }
    public bool SectionHeightFromStoreys { get; init; }
    public double? SectionHeight { get; init; }
    public bool SvgNoCss { get; init; }


    public override string ToString()
    {
        var sb = new StringBuilder();

        if (AutoYes) sb.Append("-y ");
        if (NoProgress) sb.Append("--no-progress ");
        if (No2dBoolean) sb.Append("--no-2d-boolean ");
        if (IncludeFilterValues is not null)
        {
            switch (IncludeFilterType)
            {
                case FilterType.None:
                    break;
                case FilterType.Entities:
                    sb.Append("--include=entities ")
                            .AppendJoin(
                                    ' ',
                                    IncludeFilterValues)
                            .Append(' ');
                    break;
                case FilterType.Layers:
                    break;
                case FilterType.Attribute:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (SectionHeightFromStoreys) sb.Append("--section-height-from-storeys ");
        if (SectionHeight is not null)
            sb.Append($"--section-height {SectionHeight.Value.ToString(CultureInfo.InvariantCulture)} ");

        if (SvgNoCss) sb.Append("--svg-no-css ");

        return sb.ToString();
    }
}