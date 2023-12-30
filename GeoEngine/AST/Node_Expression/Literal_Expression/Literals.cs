namespace GeoEngine;
public class Literal : Expression
{

    public Literal(object value, int lineOfCode) : base(lineOfCode)
    {
        Value = value;
        
        if (Value is double)
            Type = NodeType.Number;
        else if (Value is string)
            Type = NodeType.String;
        else
            Type = NodeType.Undefined;
    }

    public override bool CheckSemantic() => true;
    public override void Evaluate() { }
}

