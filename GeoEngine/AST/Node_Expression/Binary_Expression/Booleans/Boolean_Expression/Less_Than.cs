namespace GeoEngine;
public class LessThan : BooleanExpression
{
    public LessThan(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value < (double)RightNode.Value ? (double)1 : (double)0;
    }
}