using GeoEngine;

public class Not : Node
{
    Node Expression;
    public Not(Node expression)
    {
        this.Type = NodeType.Number;
        Expression = expression;
    }
    public override void Evaluate()
    {
        Expression.Evaluate();

    }
}