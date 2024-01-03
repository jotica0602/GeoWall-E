namespace GeoEngine;
using Microsoft.JSInterop;

public class Line : Figure
{
    public string Label = null!;
    //Parameters
    public Point P1 { get; }
    public Point P2 { get; }

    //===>>> Constructor for random line without label
    public Line(int lineOfCode) : base(lineOfCode)
    {
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        P1 = p1;
        P2 = p2;
        Color = "white";
        Type = NodeType.Line;
        Value = this;
    }

    //===>>> Constructor for random line with label
    public Line(string label, int lineOfCode) : base(lineOfCode)
    {
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        Label = label;
        P1 = p1;
        P2 = p2;
        Color = "white";
        Type = NodeType.Line;
        Value = this;
    }

    //===>>> Constructor with label
    public Line(string label, Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        P1 = p1;
        P2 = p2;
        Color = "white";
        Type = NodeType.Line;
        Value = this;
    }
    //===>>> Constructor with no label
    public Line(Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
        Color = "white";
        Type = NodeType.Line;
        Value = this;
    }

    public override void Draw()
    {
        if (Label is not null)
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledLine", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLineThroughPoints", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 3);
        }
    }

    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: (P1({this.P1.X};{this.P2.Y})):(P2({this.P2.X};{this.P2.Y}))";

        else return $"(P1({this.P1.X};{this.P2.Y})):(P2({this.P2.X};{this.P2.Y}))";
    }
}