namespace GeoEngine;
using System.Linq.Expressions;

public class CircleFunction : FigureFunction
{
    public Node Radius { get; set; }
    public Node Center { get; set; }


    public CircleFunction(Node radius, Node center, int lineOfCode) : base(lineOfCode)
    {
        Radius = radius;
        Center = center;
        Type = NodeType.Circle;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        Radius.Evaluate();
        Center.Evaluate();
        bool flag = false;
        
        if (Radius.Value is not double)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, radius is not a number", LineOfCode);
        }
        if (Center.Value is not Point)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call, center argument is not a point", LineOfCode);
        }

        if (flag) throw new Exception();
        Value = new Circle((Point)Center.Value, (double)Radius.Value, LineOfCode);
    }
}