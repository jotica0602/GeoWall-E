namespace GeoEngine;
using Microsoft.JSInterop;

//Draw a line to the end of the canvas between P1 and P2 begining in P1
public class Ray : Figure
{
    public string Label = null!;
    //Parameters
    public Point P1 { get; }
    public Point P2 { get; }

    //===>>> Constructor with label
    public Ray(string label, Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        P1 = p1;
        P2 = p2;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Ray(Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
        Color = "white";
    }

    public override void Draw()
    {
        if(Label is not null)
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledRay", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawRayThroughPoints", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 3);
        }
    }
}