namespace GeoEngine;
public class FunctionInvocation : Expression
{
    public string Name { get; set; }
    public List<Node> Arguments { get; set; }
    public List<Token> Tokens { get; set; }
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
        SetUp();
        ASTBuilder parser = new ASTBuilder(Tokens);
        List<Node> functionTree = parser.BuildNodes(Scope);
        functionTree[0].Evaluate();
        Value = functionTree[0].Value;
        switch (Value)
        {
            case "System.Double":
                Type = NodeType.Number;
                break;
            case "System.String":
                Type = NodeType.String;
                break;
        }
    }

    public void SetUp()
    {
        FunctionDeclaration original = null!;
        Scope scope = new Scope();
        scope.Parent = Scope.Parent;

        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Functions.Exists(x => Equals(x.Name, this.Name)))
                original = actualScope.Functions.Find(x => Equals(x.Name, this.Name))!;
        scope.Functions.Add(original);
        Tokens = original.Body;
        List<string> argNames = original.ArgumentsName;

        for (int i = 0; i < argNames.Count; i++)
            scope.Constants.Add(new ConstantDeclaration(argNames[i], Arguments[i], Scope.MakeChild(), LineOfCode));

        Scope = scope;
    }
}