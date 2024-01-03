namespace GeoEngine;
public class ArcFunction : FigureFunction
{
    Node Center { get; set; }
    Node A { get; set; }
    Node B { get; set; }
    Node Radius { get; set; }

    public ArcFunction(Node center, Node a, Node b, Node radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        A = a;
        B = b;
        Radius = radius;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        Center.Evaluate();
        A.Evaluate();
        B.Evaluate();
        Radius.Evaluate();
        bool flag = false;

        if (Center.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, first argument is not a point", LineOfCode);
        }

        if (A.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, second argument is not a point", LineOfCode);

        }

        if (B.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, third argument is not a point", LineOfCode);

        }

        if (Radius.Value is not double)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, radius is not a number", LineOfCode);
        }


        if (flag) throw new Exception();
        Value = new Arc((Point)Center.Value, (Point)A.Value, (Point)B.Value, (double)Radius.Value, LineOfCode);
    }
}