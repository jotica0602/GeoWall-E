namespace GeoEngine;

public abstract class UnaryExpression : Expression
{
    public abstract Node Expression { get; set; }
    protected UnaryExpression(int lineOfCode) : base(lineOfCode) { }

    public override bool CheckSemantic() => Expression.CheckSemantic();
}