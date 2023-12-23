namespace GeoEngine;
public class Or : BooleanExpression
{
    public Or(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        if (LeftNode.BooleanValue || RightNode.BooleanValue) { Value = 1; }
        else { Value = 0; }
    }
}