namespace GeoEngine;
using Microsoft.JSInterop;

public class Point : Figure
{
    //in case we need a label
    public string Label = null!;
    //now its parameters
    public double X { get; }
    public double Y { get; }

    //===>>> Contructor with label
    public Point(string label, double x, double y, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        X = x;
        Y = y;
        Color = "white";
        Type = NodeType.Point;
        Value = this;
    }

    //===>>> Constructor with no label
    public Point(double x, double y, int lineOfCode) : base(lineOfCode)
    {
        X = x;
        Y = y;
        Color = "white";
        Type = NodeType.Point;
        Value = this;
    }

    //===>>> Contructor for random point with label
    public Point(string label, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        Random rnd = new Random();
        X = rnd.Next(0, 691);
        Y = rnd.Next(0, 741);
        Color = "white";
        Type = NodeType.Point;
        Value = this;
    }

    //===>>> Constructor for random point with no label
    public Point(int lineOfCode) : base(lineOfCode)
    {
        Random rnd = new Random();
        X = rnd.Next(0, 691);
        Y = rnd.Next(0, 741);
        Color = "white";
        Type = NodeType.Point;
        Value = this;
    }

    public override /* async */ void Draw(string label = "")
    {
        if (label is not "")
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledPoint", "graphCanvas", X, Y, label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawPoint", "graphCanvas", X, Y, Color, 3);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Point)
            return false;
        Point p2 = (Point)obj;
        if (X == p2.X && Y == p2.Y) return true;
        else return false;
    }
    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: ({this.X};{this.Y}) Color: {Color}";

        else return $"({this.X};{this.Y}) Color: {Color}";
    }
}