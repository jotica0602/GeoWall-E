public class Literal : Node
{
    public override NodeType Type { get; set; }
    public override object Value { get; set; }

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