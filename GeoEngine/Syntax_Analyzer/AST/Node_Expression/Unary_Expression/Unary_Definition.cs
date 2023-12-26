namespace GeoEngine;

public abstract class UnaryExpression : Node
{
    public abstract Node Expression { get; set; }
    protected UnaryExpression(int lineOfCode) : base(lineOfCode) { }

}