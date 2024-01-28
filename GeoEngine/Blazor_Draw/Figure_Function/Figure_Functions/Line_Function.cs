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
        if (P1.Value is not Point && P1.Value is not Sequence)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call first argument is not a point", LineOfCode);
            System.Console.WriteLine(P1.Value.GetType());
            System.Console.WriteLine(((Node)P1.Value).Type);
        }
        if (P2.Value is not Point && P1.Value is not Sequence)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call second argument is not a point", LineOfCode);
            System.Console.WriteLine(P1.Value.GetType());
            System.Console.WriteLine(((Node)P1.Value).Type);
        }

        if (flag)
        {
            Error.CheckErrors();
            throw new Exception();
        }

        if (P1.Value is Sequence && P2.Value is Sequence)
            Value = new Line((Point)((Sequence)P1.Value).Elements[0], (Point)((Sequence)P2.Value).Elements[0], LineOfCode);
        else if (P1.Value is Sequence)
            Value = new Line((Point)((Sequence)P1.Value).Elements[0], (Point)P2.Value, LineOfCode);
        else if (P2.Value is Sequence)
            Value = new Line((Point)P1.Value, (Point)((Sequence)P2.Value).Elements[0], LineOfCode);
        else
            Value = new Line((Point)P1.Value, (Point)P2.Value, LineOfCode);
    }
}