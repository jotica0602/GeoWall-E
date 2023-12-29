namespace GeoEngine;
public class ConstantDeclaration : Statement
{
    public string Name { get; set; }
    Scope Scope { get; set; }
    Node Expression { get; set; }
    public ConstantDeclaration(string name, Node expression, Scope scope, int lineOfCode) : base(lineOfCode)
    {
        Name = name;
        Scope = scope;
        Expression = expression;
        Type = NodeType.Temporal;
        CheckSemantic();
    }

    public override bool CheckSemantic()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Constants.Exists(x => x.Name == this.Name))
            {
                new Error
                (
                    ErrorKind.Semantic, 
                    ErrorCode.invalid, 
                    $"constants cannot be redefined", 
                    LineOfCode
                );
                return false;
            }

        return true;
    }

    public override void Evaluate()
    {
        Expression.Evaluate();
        Value = Expression.Value;
    }
}