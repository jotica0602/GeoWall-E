namespace GeoEngine;
public class Quotient : ArithmeticExpression
{
    public Quotient(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
        Operator = "/";
    }

    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        if (RightNode.Value is (double)0)
        {
            Error error = new Error(ErrorKind.RunTime, ErrorCode.invalid, "operation, cannot divide by 0", LineOfCode);
            Error.CheckErrors();
            throw new DivideByZeroException();
        }
        Tools.RunTimeNumberTypeChecker(LeftNode, Operator, RightNode, LineOfCode);
        Value = (double)LeftNode.Value / (double)RightNode.Value;
    }
}