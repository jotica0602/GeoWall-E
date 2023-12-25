namespace GeoEngine;
public class LineBreak : Node
{
    public LineBreak(object value)
    {
        Type = NodeType.LineBreak;
        Value = value;
    }

    public override void Evaluate() { }
}