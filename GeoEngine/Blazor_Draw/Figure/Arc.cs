namespace GeoEngine;
using Microsoft.JSInterop;

public class Arc : Figure
{
    public string Label = null!;
    //Parameters
    public Point Center { get; }
    public Point StartPoint { get; }
    public Point EndPoint { get; }
    public double StartAngle { get; set; }
    public double EndAngle { get; set; }

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
        StartPoint = b;
        EndPoint = c;
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
        StartPoint = b;
        EndPoint = c;
        Radius = radius;
        Type = NodeType.Arc;
        Value = this;
        Color = "white";
    }

    public Arc(Point center, Point b, Point c, double radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        StartPoint = b;
        EndPoint = c;
        Radius = radius;
    }

    public void GetAngles()
    {
        StartAngle = Math.Atan2(StartPoint.Y - Center.Y, StartPoint.X - Center.X);
        EndAngle = Math.Atan2(EndPoint.Y - Center.Y, EndPoint.X - Center.X);

        if (EndAngle < StartAngle)
        {
            EndAngle += 360;
        }
    }
    public async override void Draw()
    {
        GetAngles();
        if (Label is not null)
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledArc", "graphCanvas", Center.X, Center.Y, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, Radius, Label, Color, 3);
        }
        else
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeVoidAsync("drawArcBetweenPoints", "graphCanvas", Center.X, Center.Y, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, Radius, Color, 3);
            GetColor();
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Arc) return false;
        Arc a2 = (Arc)obj;
        if (Equals(Center, a2) && Equals(StartPoint, a2.StartPoint) && Equals(EndPoint, a2.EndPoint)) return true;
        return false;
    }
    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.StartPoint.X},{this.StartPoint.Y}) P2:({this.EndPoint.X},{this.EndPoint.Y})";

        else return $" C:({this.Center.X};{this.Center.Y}) R:{Radius} P1:({this.StartPoint.X},{this.StartPoint.Y}) P2:({this.EndPoint.X},{this.EndPoint.Y})";
    }
}