namespace GeoEngine;
using System.Linq.Expressions;

public class RayFunction : FigureFunction
{
    public Node P1 { get; set; }
    public Node P2 { get; set; }

    public RayFunction(Node p1, Node p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
        Type = NodeType.Segment;
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

        if (flag) throw new Exception();
        Value = new Ray((Point)P1.Value, (Point)P2.Value, LineOfCode);
    }
}