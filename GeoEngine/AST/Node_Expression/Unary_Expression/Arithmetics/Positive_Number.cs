namespace GeoEngine;

public class PositiveNumber : UnaryExpression
{
    public override Node Expression { get; set; }
    public PositiveNumber(Node expression, int lineOfCode) : base(lineOfCode)
    {
        Expression = expression;
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        Expression.Evaluate();
        Value = Expression.Value;
    }
}