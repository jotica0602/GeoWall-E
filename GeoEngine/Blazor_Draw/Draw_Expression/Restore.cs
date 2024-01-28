namespace GeoEngine;

public class Restore : Expression
{
    public Restore(int lineOfCode) : base(lineOfCode)
    {
        Value = this;
    }

    public override bool CheckSemantic() => true;
    public override void Evaluate()
    {
        if (DrawEngine.stackColor.Count() is not 0)
            DrawEngine.stackColor.Pop();
    }
}