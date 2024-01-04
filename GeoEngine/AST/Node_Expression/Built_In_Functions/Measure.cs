using System.Security.Cryptography;

namespace GeoEngine;
public class Measure : Expression
{
    public Node Point1 { get; set; }
    public Node Point2 { get; set; }

    public Measure(Node point1, Node point2, int lineOfCode) : base(lineOfCode)
    {
        Type = NodeType.Number;
        Point1 = point1;
        Point2 = point2;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        bool flag = false;

        Point1.Evaluate();
        Point2.Evaluate();

        if (Point1.Value is not Point)
        {
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function first parameter must be a point", LineOfCode);
            flag = true;
        }

        if (Point2.Value is not Point)
        {
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function second parameter must be a point", LineOfCode);
            flag = true;
        }
        Error.CheckErrors();

        if (flag) throw new Exception();
        else
        {
            double p1x = ((Point)Point1.Value).X;
            double p2x = ((Point)Point2.Value).X;
            double p1y = ((Point)Point1.Value).Y;
            double p2y = ((Point)Point2.Value).Y;
            Value = Math.Floor(Math.Sqrt(Math.Pow(p2x-p1x,2)+Math.Pow(p2y-p1y,2)));
        }
    }
}