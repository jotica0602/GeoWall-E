namespace GeoEngine;

public abstract class Figure
{
    public string Color { get; set; }

    public abstract void Draw();

    public abstract void GetColor();
}