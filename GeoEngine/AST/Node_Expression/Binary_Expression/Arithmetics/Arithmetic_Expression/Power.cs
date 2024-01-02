namespace GeoEngine;
public class Power : ArithmeticExpression
{
    public Power(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
        Operator = "^";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Tools.RunTimeNumberTypeChecker(LeftNode, Operator, RightNode, LineOfCode);
        Value = Math.Pow((double)LeftNode.Value, (double)RightNode.Value);
    }
}