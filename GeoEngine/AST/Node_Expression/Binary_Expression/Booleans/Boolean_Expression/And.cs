namespace GeoEngine;
public class And : BooleanExpression
{
    public And(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { }

    public override bool CheckSemantic() => LeftNode.CheckSemantic() && RightNode.CheckSemantic();

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();

        Value = Tools.BooleanChecker(LeftNode.Value) && Tools.BooleanChecker(RightNode.Value) ? (double)1 : (double)0;
    }
}
