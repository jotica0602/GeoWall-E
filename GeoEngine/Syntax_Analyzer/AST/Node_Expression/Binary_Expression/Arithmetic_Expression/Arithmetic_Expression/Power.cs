public class Power : BinaryExpression
{
    public Power(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        Type = NodeType.Number;
    }
   
    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = Math.Pow((double)LeftNode.Value, (double)RightNode.Value);
    }
}