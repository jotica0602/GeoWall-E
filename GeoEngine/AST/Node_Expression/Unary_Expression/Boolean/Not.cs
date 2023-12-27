namespace GeoEngine;

public class Not : UnaryExpression
{
    public override Node Expression { get; set; }
    public Not(Node expression, int lineOfCode) : base(lineOfCode)
    {
        Expression = expression;
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        Expression.Evaluate();
        Value = !BooleanValue.Checker(Expression.Value) ? (double)1 : (double)0;
    }
}