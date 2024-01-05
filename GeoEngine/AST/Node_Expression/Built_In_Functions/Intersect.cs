namespace GeoEngine;

public class Intersect : Expression
{
    Node Figure1 { get; set; }
    Node Figure2 { get; set; }
    public Intersect(Node figure1, Node figure2, int lineOfCode) : base(lineOfCode)
    {
        Figure1 = figure1;
        Figure2 = figure2;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        Figure1.Evaluate();
        Figure2.Evaluate();
        bool flag = false;

        if (Figure1.Value is not Figure)
        {
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function parameter, first parameter must be a figure", LineOfCode);
            flag = true;
        }

        if (Figure2.Value is not Figure)
        {
            new Error(ErrorKind.RunTime, ErrorCode.invalid, "function parameter, second parameter must be a figure", LineOfCode);
            flag = true;
        }

        if (flag) throw new Exception();
        Value = Intersections.Intersection((Figure)Figure1.Value, (Figure)Figure2.Value, LineOfCode);
    }
}