namespace GeoEngine;
public class Literal : Node
{
    public override bool BooleanValue
    {
        get
        {
            if (Type is NodeType.Undefined) { return false; }
            else if (Type is NodeType.Number) { return Value is not 0; }
            else { return (string)Value is not ""; }
        }
        set { }
    }
    public Literal(object Value)
    {
        this.Value = Value;

        if (Value is double)
            Type = NodeType.Number;
        else if (Value is string)
            Type = NodeType.String;
        else
            Type = NodeType.Undefined;
    }

    public override void Evaluate() { }
}