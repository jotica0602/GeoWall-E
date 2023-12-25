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
        
        Value = BooleanValue.Checker(LeftNode.Value) && BooleanValue.Checker(RightNode.Value) ? (double)1 : (double)0;
    }
}