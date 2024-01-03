namespace GeoEngine;
using Microsoft.JSInterop;


//Draw a line between P1 and P2
public class Segment : Figure
{
    public string Label = null!;
    //Parameters
    public Point P1 { get; }
    public Point P2 { get; }

    //===>>> Constructor with label
    public Segment(string label, Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        P1 = p1;
        P2 = p2;
        Color = "white";
    }
    
    //===>>> Constructor with no label
    public Segment(Point p1, Point p2, int lineOfCode) : base(lineOfCode)
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
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledSegment", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLine", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 3);
        }
    }

    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: (P1({this.P1.X};{this.P2.Y})):(P2({this.P2.X};{this.P2.Y}))";

        else return $"(P1({this.P1.X};{this.P2.Y})):(P2({this.P2.X};{this.P2.Y}))";
    }
}