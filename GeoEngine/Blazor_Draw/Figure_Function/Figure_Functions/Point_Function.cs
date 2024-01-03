namespace GeoEngine;
using System.Linq.Expressions;

public class PointFunction : FigureFunction
{
    public Node X { get; set; }
    public Node Y { get; set; }

    public PointFunction(Node x, Node y, int lineOfCode) : base(lineOfCode)
    {
        X = x;
        Y = y;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        X.Evaluate();
        Y.Evaluate();
        bool flag = false;
        if (X.Value is not double)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call first argument is not a valid coordenate", LineOfCode);
        }
        if (Y.Value is not double)
        {
            flag = true;
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function call second argument is not a valid coordenate", LineOfCode);
        }

        if (flag) throw new Exception();
        Value = new Point((double)X.Value, (double)Y.Value, LineOfCode);
    }
}