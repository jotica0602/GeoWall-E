namespace GeoEngine;
using Microsoft.JSInterop;

public class Circle : Figure
{
    public string Label = null!;
    //Parameters
    public Point Center {get; }
    public double Radius {get; }

    //===>>> Contructor with label
    public Circle(string label, Point center, double radius, int lineOfCode) : base(lineOfCode)
    {
        Label = label;
        Center = center;
        Radius = radius;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Circle(Point center, double radius, int lineOfCode) : base(lineOfCode)
    {
        Center = center;
        Radius = radius;
        Color = "white";
    }
    public override void Draw()
    {
        if(Label is not null)
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
        if(Label is not null)
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

    public override void Evaluate() {}

    public override bool CheckSemantic() => true;
}
