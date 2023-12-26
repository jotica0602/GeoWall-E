namespace GeoEngine;
public class GreaterThan : BooleanExpression
{
    public GreaterThan(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value > (double)RightNode.Value ? 1 : 0;
    }
}