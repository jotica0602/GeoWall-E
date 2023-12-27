namespace GeoEngine;

public class NegativeNumber : UnaryExpression
{
    public override Node Expression { get; set; }
    public NegativeNumber(Node expression, int lineOfCode) : base(lineOfCode)
    {
        Expression = expression;
        Type = NodeType.Number;
    }

    public override void Evaluate()
    {
        Expression.Evaluate();
        
        Value = ((Expression)Expression.Value);
    }
}