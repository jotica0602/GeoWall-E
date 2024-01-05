namespace GeoEngine;
using System.Linq.Expressions;

public class LineFunction : FigureFunction
{
    public Node P1 { get; set; }
    public Node P2 { get; set; }

    public LineFunction(Node p1, Node p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        P1.Evaluate();
        P2.Evaluate();
        bool flag = false;
        if (P1.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call first argument is not a point", LineOfCode);
        }
        if (P2.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call second argument is not a point", LineOfCode);
        }

        if (flag)
        {
            Error.CheckErrors();
            throw new Exception();
        }
        
        Value = new Line((Point)P1.Value, (Point)P2.Value, LineOfCode);
    }
}