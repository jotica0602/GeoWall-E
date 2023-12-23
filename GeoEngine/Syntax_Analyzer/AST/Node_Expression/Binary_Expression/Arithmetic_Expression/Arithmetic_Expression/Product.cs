public class Product : BinaryExpression
{
    public override NodeType Type { get; set; }
    public override object Value { get; set; }
    public Product(Node LeftNode, Node RightNode) : base(LeftNode, RightNode)
    {
        Type = NodeType.Number;
    }
    
    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value * (double)RightNode.Value;
    }
}