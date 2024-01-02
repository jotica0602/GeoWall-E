namespace GeoEngine;
public class LessOrEquals : BooleanExpression
{
    public LessOrEquals(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    { 
        Operator = "<=";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Tools.NumberTypeChecker(LeftNode,Operator,RightNode,LineOfCode);
        Value = (double)LeftNode.Value <= (double)RightNode.Value ? (double)1 : (double)0;
    }
}