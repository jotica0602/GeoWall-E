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
    public Arc(string label, int lineOfCode) : base(lineOfCode)
    {
        Label = label;

        //generating point, center and radius
        Random random = new Random();
        Point center = new Point(lineOfCode);
        Point b = new Point(lineOfCode);
        Point c = new Point(lineOfCode);
        double radius = random.Next(3, 190);
        Center = center;
        B = b;
        C = c;
        Radius = radius;
        Type = NodeType.Arc;
        Value = this;
        Color = "white";
    }

    //==> Constructor random with no label
    public Arc(int lineOfCode) : base(lineOfCode)
    {
        //generating point, center and radius
        Random random = new Random();
        Point center = new Point(lineOfCode);
        Point b = new Point(lineOfCode);
        Point c = new Point(lineOfCode);
        double radius = random.Next(3, 190);
        Center = center;
        B = b;
        C = c;
        Radius = radius;
        Type = NodeType.Arc;
        Value = this;
        Color = "white";
    }

    public Arc(Point center, Point b, Point c, double radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        B = b;
        C = c;
        Radius = radius;
    }
    
    public override void Draw()
    {
        if (Label is not null)
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
        if (Label is not null)
            return $"{Label}: C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.B.X},{this.B.Y}) P2:({this.C.X},{this.C.Y}) Color: {Color}";

        else return $" C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.B.X},{this.B.Y}) P2:({this.C.X},{this.C.Y}) Color: {Color}";
    }
}