namespace GeoEngine;
public class FunctionInvocation : Node
{
    public string Name { get; set; }
    public List<Node> Arguments { get; set; }
    public Scope Scope { get; set; }
    public FunctionInvocation(string name, List<Node> arguments, Scope scope, int lineOfCode) : base(lineOfCode)
    {
        Name = name;
        Arguments = arguments;
        Scope = scope;
    }

    public override bool CheckSemantic()
    {
        bool exists = Exists();
        bool argsAreExpressions = CheckArgs();

        return exists && argsAreExpressions;
    }

    bool Exists()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Functions.Exists(x => Equals(x.Name, this.Name)))
                return true;

        new Error
        (
            ErrorKind.Semantic,
            ErrorCode.invalid,
            $"function call, function \"{Name}\" does not exists",
            LineOfCode
        );

        return false;
    }

    bool CheckArgs()
    {
        if (Arguments.Where(x => x is not Expression).Count() is not 0)
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "parameter(s) type, they/it must be Expression(s).",
                LineOfCode
            );
            return false;
        }

        return true;
    }

    public override void Evaluate()
    {

    }

    public void SetUp()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Functions.Exists(x => Equals(x.Name, this.Name)));

    }
}