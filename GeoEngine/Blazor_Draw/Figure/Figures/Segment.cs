namespace GeoEngine;
using Microsoft.JSInterop;


//Draw a line between P1 and P2
public class Segment : Figure
{
    public string Label = null!;
    //Parameters
    public Point P1 { get; }
    public Point P2 { get; }

    //==> Constructor with label
    public Segment(string label, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Segment;
        Color = "white";
    }

    //==> Constructor with no label
    public Segment(int lineOfCode) : base(lineOfCode)
    {
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Segment;
        Color = "white";
    }

    // ==> Constructor
    public Segment(Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Segment;
        Color = "white";
    }
    
    public async override void Draw(string label = "")
    {
        if (label is not "")
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledSegment", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, label, Color, 1);
        }
        else
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawLine", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 1);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Segment) return false;
        Segment s2 = (Segment)obj;
        if (Equals(P1, s2.P1) && Equals(P2, s2.P2)) return true;
        return false;
    }

    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: (P1({this.P1.X};{this.P1.Y})):(P2({this.P2.X};{this.P2.Y})) C:({this.Color})";

        else return $"(P1({this.P1.X};{this.P1.Y})):(P2({this.P2.X};{this.P2.Y})) C:({this.Color})";
    }
}