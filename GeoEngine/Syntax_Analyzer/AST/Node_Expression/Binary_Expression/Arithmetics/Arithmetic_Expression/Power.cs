namespace GeoEngine;
public class Power : ArithmeticExpression
{
    public Power(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
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