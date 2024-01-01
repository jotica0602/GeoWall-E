namespace GeoEngine;

public abstract class Figure
{
    public string Color { get; set; }

    public abstract void Draw();

    public virtual void GetColor()
    {
        if(DrawEngine.stackColor.Count > 0)
        {
            Color = DrawEngine.stackColor.Peek();
        }
        else
        {
            Color = "white";
        }
    }
}