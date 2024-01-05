namespace GeoEngine;

public class Undefined : Expression
{
    public Undefined(int lineOfCode) : base(lineOfCode)
    {
        Value = this;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate() { }
}