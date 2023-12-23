namespace GeoEngine;
public class LessThan : BooleanExpression
{
    public LessThan(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value < (double)RightNode.Value ? 1 : 0;
    }
}