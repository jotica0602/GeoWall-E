using System.Data;
using GeoEngine;

public class ConstantDeclaration : Statement
{
    Node Expression { get; set; }
    public ConstantDeclaration(Node expression, int lineOfCode) : base(lineOfCode)
    {
        Expression = expression;
        Type = NodeType.Temporal;
    }

    public override void Evaluate()
    {
        Expression.Evaluate();
        Value = Expression.Value;
    }
}