namespace GeoEngine;
using Microsoft.JSInterop;

public class Circle : Figure
{
    public string Label = null!;
    //Parameters
    public Point Center { get; }
    public double Radius { get; }


    //===>>> Contructor with label
    public Circle(string label, int lineOfCode) : base(lineOfCode)
    {
        Point center = new Point(lineOfCode);
        Random random = new Random();
        Label = label;
        Radius = (double)random.Next(3, 190);
        Center = center;
        Value = this;
        Type = NodeType.Circle;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Circle(int lineOfCode) : base(lineOfCode)
    {
        Point center = new Point(lineOfCode);
        Random random = new Random();
        Radius = (double)random.Next(3, 190);
        Center = center;
        Value = this;
        Type = NodeType.Circle;
        Color = "white";
    }

    // Constructor
    public Circle(Point center, double radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        Radius = radius;
        Value = this;
        Type = NodeType.Circle;
        Color = "white";
    }


    public override void Draw()
    {
        if (Label is not null)
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledCircleOutline", "graphCanvas", Center.X, Center.Y, Radius, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawCircleOutline", "graphCanvas", Center.X, Center.Y, Radius, Color, 3);
        }
    }

    public void Fill()
    {
        if (Label is not null)
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawLabeledCircle", "graphCanvas", Center.X, Center.Y, Radius, Label, Color, 3);
        }
        else
        {
            GetColor();
            DrawEngine._jsRuntime.InvokeVoidAsync("drawCircle", "graphCanvas", Center.X, Center.Y, Radius, Color, 3);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Circle) return false;
        Circle c2 = (Circle)obj;
        if (Equals(Center, c2.Center) && Equals(Radius, c2.Radius)) return true;
        return false;
    }
    public override string ToString()
    {
        if (Label is not null)
            return $"{Label}: C:({this.Center.X};{this.Center.Y}) R:{Radius} ";

        else return $" C:({this.Center.X};{this.Center.Y}) R:{Radius}";
    }
}
