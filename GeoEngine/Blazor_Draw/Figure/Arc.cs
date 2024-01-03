namespace GeoEngine;
using Microsoft.JSInterop;

public class Arc : Figure
{
    public string Label = null!;
    //Parameters
    public Point Center { get; }
    public Point B { get; }
    public Point C { get; }
    public double Radius { get; }

    //==> Constructor random with label
    public Arc(string label, Point center, Point b, Point c, double radius, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        Center = center;
        B = b;
        C = c;
        Radius = radius;
        Type = NodeType.Arc;
        Value = this;
        Color = "white";
    }

    //==> Constructor random with no label
    public Arc(Point center, Point b, Point c, double radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        B = b;
        C = c;
        Radius = radius;
        Type = NodeType.Arc;
        Value = this;
        Color = "white";
    }
    public override void Draw()
    {
        if(Label is not null)
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledArc", "graphCanvas", Center.X, Center.Y, B.X, B.Y, C.X, C.Y, Radius, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawArcBetweenPoints2", "graphCanvas", Center.X, Center.Y, B.X, B.Y, C.X, C.Y, Radius, Color, 3);
        }
    }

    public override string ToString()
    {
        if(Label is not null)
            return $"{Label}: C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.B.X},{this.B.Y}) P2:({this.C.X},{this.C.Y})";

        else return $" C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.B.X},{this.B.Y}) P2:({this.C.X},{this.C.Y})";
    }
}