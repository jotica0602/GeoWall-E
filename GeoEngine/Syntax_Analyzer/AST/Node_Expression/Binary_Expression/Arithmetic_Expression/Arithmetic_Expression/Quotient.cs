public class Quotient : BinaryExpression
{
    public override NodeType Type { get; set; }
    public override object Value { get; set; }
    public Quotient(Node LeftNode, Node RightNode) : base(LeftNode, RightNode)
    {
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        if(RightNode.Value is 0) // error de operacion Divide By Zero Exception
        Value = (double)LeftNode.Value / (double)RightNode.Value;
    }
}