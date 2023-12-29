namespace GeoEngine;
public class Literal : Expression
{

    public Literal(object value, int lineOfCode) : base(lineOfCode)
    {
        Value = value;
        
        if (Value is double)
            Type = NodeType.Number;
        else if (Value is string)
            Type = NodeType.String;
        else
            Type = NodeType.Undefined;
    }

    public override bool CheckSemantic() => true;
    public override void Evaluate() { }
}

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

    public override bool CheckSemantic()
    {
        return Exists();
    }

    bool Exists()
    {
        bool exists = false;
        for (var actualScope = Scope; actualScope!.Parent is not null; actualScope = actualScope.Parent)
        {
            if (actualScope.Constants.Exists(x => x.Name == this.Name))
                return true;
            if (actualScope.Parent is null)
                new Error
                (
                    ErrorKind.Semantic,
                    ErrorCode.invalid,
                    $"constant invocation, \"{Name}\" does not exists.",
                    LineOfCode
                );

        }
        return exists;
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