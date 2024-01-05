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
        Random rnd = new Random();
        int x = rnd.Next(0, 691);
        int y = rnd.Next(0, 741);
        Point p1 = new Point(x, y, lineOfCode);
        x = rnd.Next(0, 691);
        while (x == p1.X) x = rnd.Next(0, 691);
        y = rnd.Next(0, 741);
        while (y == p1.Y) y = rnd.Next(0, 741);
        Point p2 = new Point(x, y, lineOfCode);
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
    public override bool Equals(object? obj)
    {
        if (obj is not Line) return false;
        Line l2 = (Line)obj;
        if (Equals(P1, l2.P1) && Equals(P2, l2.P2)) return true;
        return false;
    }
    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: (P1({this.P1.X};{this.P1.Y})):(P2({this.P2.X};{this.P2.Y})) Color: {Color}";

        else return $"(P1({this.P1.X};{this.P1.Y})):(P2({this.P2.X};{this.P2.Y})) Color: {Color}";
    }
}