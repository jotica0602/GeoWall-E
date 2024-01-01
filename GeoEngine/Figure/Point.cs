namespace GeoEngine;
using Microsoft.JSInterop;

public class Point : Figure
{
    //in case we need a label
    public string Label = null!;
    //now its parameters
    public double X {get; }
    public double Y {get; }

    //===>>> Contructor with label
    public Point(string label, double x, double y)
    {
        Label = label;
        X = x;
        Y = y;
        Color = "white";
    }

    //===>>> Constructor with no label
    public Point(double x, double y)
    {
        X = x;
        Y = y;
        Color = "white";
    }

    //===>>> Contructor for random point with label
    public Point(string label)
    {
        Label = label;
        Random rnd = new Random();
        X = rnd.Next(0, 691);
        Y = rnd.Next(0, 741);
        Color = "white";
    }

    //===>>> Constructor for random point with no label
    public Point()
    {
        Random rnd = new Random();
        X = rnd.Next(0, 691);
        Y = rnd.Next(0, 741);
        Color = "white";
    }
    public override async void Draw()
    {
        if(Label is not null)
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeAsync<object>("drawLabeledPoint", "graphCanvas", X, Y, Label, Color, 3);
            //await JSRuntime.InvokeVoidAsync("drawLabeledPoint", "graphCanvas", 100, 100, "Punto A", "red", 3);
        }
        else
        {
            GetColor();
            await DrawEngine._jsRuntime.InvokeAsync<object>("drawPoint", "graphCanvas", X, Y, Color, 3);
            //await JSRuntime.InvokeVoidAsync("drawPoint", "graphCanvas", 100, 100, "red", 3);
        }
    }
}