namespace GeoEngine;
public class Difference : ArithmeticExpression
{
    public Difference(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
        Operator = "-";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Tools.RunTimeNumberTypeChecker(LeftNode, Operator, RightNode, LineOfCode);
        Value = (double)LeftNode.Value - (double)RightNode.Value;
    }
}