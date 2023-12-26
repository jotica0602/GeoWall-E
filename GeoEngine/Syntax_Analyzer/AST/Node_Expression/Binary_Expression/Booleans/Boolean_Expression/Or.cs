namespace GeoEngine;
public class Or : BooleanExpression
{
    public Or(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = Value = BooleanValue.Checker(LeftNode.Value) || BooleanValue.Checker(RightNode.Value) ? (double)1 : (double)0;
    }
}