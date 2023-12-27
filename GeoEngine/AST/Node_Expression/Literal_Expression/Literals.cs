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

    public override void Evaluate() { }
}

public class Constant : Expression
{
    Scope Scope { get; set; }
    string Name { get; set; }
    public Constant(Scope scope, string name, int lineOfCode) : base(lineOfCode)
    {
        Scope = scope;
        Name = name;
        Type = NodeType.Temporal;
    }

    public override void Evaluate()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Constants.ContainsKey(Name))
            {
                actualScope.Constants[Name].Evaluate();
                Value = actualScope.Constants[Name].Value;
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