namespace GeoEngine;
public abstract class Node
{
    public NodeType Type { get; set; }
    public object Value { get; set; }
    public abstract bool BooleanValue { get; set; }
    public abstract void Evaluate();
}