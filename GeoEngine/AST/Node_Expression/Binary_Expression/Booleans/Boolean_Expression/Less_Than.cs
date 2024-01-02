namespace GeoEngine;
public class LessThan : BooleanExpression
{
    public LessThan(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Operator = "<";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Tools.RunTimeNumberTypeChecker(LeftNode,Operator,RightNode,LineOfCode);
        Value = (double)LeftNode.Value < (double)RightNode.Value ? (double)1 : (double)0;
    }
}