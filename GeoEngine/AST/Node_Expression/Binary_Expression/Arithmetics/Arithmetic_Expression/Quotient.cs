namespace GeoEngine;
public class Quotient : ArithmeticExpression
{
    public Quotient(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value / (double)RightNode.Value;
    }
}