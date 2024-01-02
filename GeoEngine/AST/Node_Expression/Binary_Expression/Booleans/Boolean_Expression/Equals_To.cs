namespace GeoEngine;
public class EqualsTo : BooleanExpression
{
    public EqualsTo(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { 
        Operator = "==";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Tools.NumberTypeChecker(LeftNode,Operator,RightNode,LineOfCode);
        Value = LeftNode.Value.ToString() == RightNode.Value.ToString() ? (double)1 : (double)0;
    }
}