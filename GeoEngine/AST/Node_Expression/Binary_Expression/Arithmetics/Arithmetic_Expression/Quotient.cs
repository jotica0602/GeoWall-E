namespace GeoEngine;
public class Quotient : ArithmeticExpression
{
    public Quotient(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        if (RightNode.Value is 0)
        {
            Error error = new Error(ErrorKind.RunTimeError, ErrorCode.invalid, "operation, cannot divide by 0.", LineOfCode);
        }
        Value = (double)LeftNode.Value / (double)RightNode.Value;
    }
}