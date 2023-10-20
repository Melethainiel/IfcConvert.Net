namespace IfcConvert.Net;

public sealed class ArgumentBuilder
{
    private bool _autoYes = true;
    private bool _noProgress;
    private bool _no2dBoolean;
    private IncludeBuilder? _includeBuilder;

    /// <summary>
    /// answer 'yes' automatically to possible confirmation queries (e.g. overwriting an existing output file)
    /// </summary>
    /// <returns>return argument builder</returns>
    public ArgumentBuilder AutoYes(bool value = true)
    {
        _autoYes = value;
        return this;
    }

    /// <summary>
    /// suppress possible progress bar type of prints that use carriage return
    /// </summary>
    /// <returns>return argument builder</returns>
    public ArgumentBuilder NoProgress()
    {
        _noProgress = true;
        return this;
    }

    /// <summary>
    /// Do not attempt to process boolean subtractions in 2D.
    /// </summary>
    /// <returns>return argument builder</returns>
    public ArgumentBuilder No2dBoolean()
    {
        _no2dBoolean = true;
        return this;
    }

    /// <summary>
    /// Specifies that the instances that match a specific filtering criteria are to be included in the geometrical output:
    /// 1) 'entities': the following list of types should be included. SVG output defaults to IfcSpace to be included. The entity names are handledcase-insensitively.
    /// 2) 'layers': the instances that are assigned to presentation layers of which names match the given values should be included.
    /// 3) 'attribute AttributeName': products whose value for AttributeName should be included .
    /// Currently supported arguments are GlobalId, Name, Description, and Tag.
    /// </summary>
    /// <returns>return Include builder</returns>
    public IncludeBuilder Include()
    {
        _includeBuilder = new IncludeBuilder(this);
        return _includeBuilder;
    }

    public Arguments Build()
    {
        return new Arguments
        {
            AutoYes = _autoYes,
            NoProgress = _noProgress,
            No2dBoolean = _no2dBoolean,
            IncludeFilterType = _includeBuilder?.FilterType ?? FilterType.None,
            IncludeFilterValues = _includeBuilder?.FilterValues
        };
    }
}

public enum FilterType
{
    None,
    Entities,
    Layers,
    Attribute
}