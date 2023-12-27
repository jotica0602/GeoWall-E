namespace GeoEngine;
public class NotEquals : BooleanExpression
{
    public NotEquals(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = LeftNode.Value.ToString() != RightNode.Value.ToString() ? 1 : 0;
    }
}