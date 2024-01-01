namespace GeoEngine;
using Microsoft.JSInterop;

public class Line : Figure
{
    public string Label = null!;
    //Parameters
    public Point P1 { get; }
    public Point P2 { get; }

    //===>>> Constructor with label
    public Line(string label, Point p1, Point p2)
    {
        Label = label;
        P1 = p1;
        P2 = p2;
        Color = "white";
    }
    //===>>> Constructor with no label
    public Line(Point p1, Point p2)
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
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledLine", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLineThroughPoints", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 3);
        }
    }
}