namespace GeoEngine;

public class Import : Expression
{
    public Import(int lineOfCode) : base(lineOfCode)
    {

    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}