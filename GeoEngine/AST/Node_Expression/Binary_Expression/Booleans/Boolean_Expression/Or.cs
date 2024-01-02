namespace GeoEngine;
public class Or : BooleanExpression
{
    public Or(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {

    }
    public override bool CheckSemantic() => true;
    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = Value = Tools.BooleanChecker(LeftNode.Value) || Tools.BooleanChecker(RightNode.Value) ? (double)1 : (double)0;
    }
}