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

    //===>>> Constructor with label
    public Arc(string label, Point center, Point b, Point c, double radius)
    {
        Label = label;
        Center = center;
        B = b;
        C = c;
        Radius = radius;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Arc(Point center, Point b, Point c, double radius)
    {
        Center = center;
        B = b;
        C = c;
        Radius = radius;
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
}