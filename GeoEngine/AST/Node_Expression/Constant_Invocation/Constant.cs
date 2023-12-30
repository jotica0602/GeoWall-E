namespace GeoEngine;
public class Constant : Expression
{
    Scope Scope { get; set; }
    public string Name { get; set; }
    public Constant(Scope scope, string name, int lineOfCode) : base(lineOfCode)
    {
        Scope = scope;
        Name = name;
        Type = NodeType.Temporal;
    }

    public override bool CheckSemantic() => Exists();

    public bool Exists()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Constants.Exists(x => x.Name == this.Name))
                return true;


        new Error
        (
            ErrorKind.Semantic,
            ErrorCode.invalid,
            $"constant invocation, constant \"{Name}\" does not exists.",
            LineOfCode
        );

        return false;
    }

    public override void Evaluate()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Constants.Exists(x => x.Name == this.Name))
            {
                var match = actualScope.Constants.Find(x => x.Name == this.Name);
                match!.Evaluate();
                Value = match.Value;
                break;
            }

        switch (Value.GetType().ToString())
        {
            case "System.Double":
                Type = NodeType.Number;
                break;
            case "System.String":
                Type = NodeType.String;
                break;
        }
    }
}