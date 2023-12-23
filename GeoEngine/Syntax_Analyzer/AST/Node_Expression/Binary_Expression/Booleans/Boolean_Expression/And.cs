namespace GeoEngine;
public class And : BooleanExpression
{
    public And(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        if (LeftNode.BooleanValue && RightNode.BooleanValue) { Value = 0; }
        else { Value = 1; }
    }
}