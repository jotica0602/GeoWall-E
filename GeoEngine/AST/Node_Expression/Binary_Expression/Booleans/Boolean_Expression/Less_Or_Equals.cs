namespace GeoEngine;
public class LessOrEquals : BooleanExpression
{
    public LessOrEquals(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value <= (double)RightNode.Value ? 1 : 0;
    }
}