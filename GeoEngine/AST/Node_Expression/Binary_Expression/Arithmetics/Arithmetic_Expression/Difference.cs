namespace GeoEngine;
public class Difference : ArithmeticExpression
{
    public Difference(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value - (double)RightNode.Value;
    }
}