namespace GeoEngine;
public class LessOrEquals : BooleanExpression
{
    public LessOrEquals(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value <= (double)RightNode.Value ? 1 : 0;
    }
}