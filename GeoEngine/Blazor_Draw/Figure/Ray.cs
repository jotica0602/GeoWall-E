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
    public Ray(string label, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Ray;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Ray(int lineOfCode) : base(lineOfCode)
    {
        Point p1 = new Point(lineOfCode);
        Point p2 = new Point(lineOfCode);
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Ray;
        Color = "white";
    }


    // Constructor
    public Ray(Point p1, Point p2, int lineOfCode) : base(lineOfCode)
    {
        P1 = p1;
        P2 = p2;
        Value = this;
        Type = NodeType.Ray;
        Color = "white";
    }


    public async override void Draw()
    {
        if (Label is not null)
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledRay", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Label, Color, 1);
        }
        else
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawRayThroughPoints", "graphCanvas", P1.X, P1.Y, P2.X, P2.Y, Color, 1);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Ray) return false;
        Ray r1 = (Ray)obj;
        if (Equals(P1, r1.P1) && Equals(P2, r1.P2)) return true;
        return false;
    }

    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: (P1({this.P1.X};{this.P1.Y})):(P2({this.P2.X};{this.P2.Y})) Color: {Color}";

        else return $"(P1({this.P1.X};{this.P2.Y})):(P2({this.P2.X};{this.P2.Y})) Color: {Color}";
    }
}