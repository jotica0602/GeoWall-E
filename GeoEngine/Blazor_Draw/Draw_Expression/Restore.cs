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
        DrawEngine.stackColor.Pop();
    }
}