namespace IfcConvert.Net;

public sealed class IncludeBuilder
{
    private readonly ArgumentBuilder _parent;
    internal FilterType FilterType { get; private set; }
    internal HashSet<string> FilterValues { get; private set; } = default!;
    
    internal IncludeBuilder(ArgumentBuilder parent)
    {
        _parent = parent;
    }
    public ArgumentBuilder Entities(IEnumerable<string> values)
    {
        FilterType = FilterType.Entities;
        FilterValues = values.ToHashSet();
        return _parent;
    }
    
    public ArgumentBuilder Layers(IEnumerable<string> values)
    {
        FilterType = FilterType.Layers;
        FilterValues = values.ToHashSet();
        return _parent;
    }
    
    public ArgumentBuilder Attribute(IEnumerable<string> values)
    {
        FilterType = FilterType.Attribute;
        FilterValues = values.ToHashSet();
        return _parent;
    }
    
}