public class Literal : Node
{
    public Literal(object Value)
    {
        this.Value = Value;

        if(Value is double)
            Type = NodeType.Number;
        else if(Value is string)
            Type = NodeType.String;
        else
            Type = NodeType.Undefined;
    }

    public override void Evaluate(){ }
}