namespace GeoEngine;
public class Print : Expression
{
    Node Argument { get; set; }
    public Print(Node argument, int lineOfCode) : base(lineOfCode)
    {
        Argument = argument;
    }

    public override bool CheckSemantic()
    {
        if (Argument is not Expression)
        {
            new Error(ErrorKind.Semantic, ErrorCode.invalid, "argument type, it must be an expression", LineOfCode);
            return false;
        }

        return true;
    }

    public override void Evaluate()
    {
        Argument.Evaluate();
        Value = Argument.Value;
        // System.Console.WriteLine(Value);
    }
}
