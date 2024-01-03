namespace GeoEngine;

public class Color : Expression
{
    public string ColorName { get; set; } = null!;
    public Color(string colorName, int lineOfCode) : base(lineOfCode)
    {
        ColorName = colorName;
        Value = this;
    }

    public override bool CheckSemantic() => true;
    public override void Evaluate()
    {
        DrawEngine.stackColor.Push(ColorName);
    }
}