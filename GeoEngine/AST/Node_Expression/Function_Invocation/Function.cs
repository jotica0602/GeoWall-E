using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;

namespace GeoEngine;
public class FunctionInvocation : Expression
{
    public string Name { get; set; }
    public List<Node> Arguments { get; set; }
    Node Body { get; set; }
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
        bool argsAreExpressions = CheckArgsType();
        bool argsCountIsOk = false;

        if (exists)
            argsCountIsOk = CheckArgsCount();

        return exists && argsAreExpressions && argsCountIsOk;
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
            $"function invocation, function \"{Name}\" does not exists",
            LineOfCode
        );

        return false;
    }

    bool CheckArgsType()
    {
        if (Arguments.Where(x => x is not Expression).Count() is not 0)
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "parameter(s) type must be Expression(s).",
                LineOfCode
            );
            return false;
        }
        return true;
    }

    bool CheckArgsCount()
    {
        int originalCount = 0;
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Functions.Exists(x => Equals(x.Name, this.Name)))
            {
                var original = actualScope.Functions.Find(x => Equals(x.Name, this.Name));
                originalCount = original!.Arguments.Count;
                if (original!.Arguments.Count == this.Arguments.Count) return true;
            }

        new Error
        (
            ErrorKind.Semantic,
            ErrorCode.invalid,
            $"function invocation, \"{Name}\" recieves {originalCount} argument(s), but {this.Arguments.Count} were given",
            LineOfCode
        );

        return false;
    }

    public override void Evaluate()
    {
        SetUp();

        // Evaluate Tree
        Body.Evaluate();
        Value = Body.Value;
        Type = Body.Type;
    }

    public void SetUp()
    {
        FunctionDeclaration original = null!;
        Scope scope = new Scope();
        scope.Parent = Scope.Parent;

        // Getting original function declaration
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            foreach (var function in actualScope.Functions)
            {
                scope.Functions.Add(function);
                if (function.Name == this.Name)
                    original = function;
            }
        // if (actualScope.Functions.Exists(x => Equals(x.Name, this.Name)))
        //     original = actualScope.Functions.Find(x => Equals(x.Name, this.Name))!;


        // Adding it to the new scope
        scope.Functions.Add(original);

        // Getting Tokens
        Tokens = original.Body;

        // Getting Argument Names
        List<string> argNames = original.ArgumentsName;

        // Matching Argument Names with Expression
        for (int i = 0; i < argNames.Count; i++)                                    //error here
            scope.Constants.Add(new ConstantDeclaration(argNames[i], Arguments[i], scope, LineOfCode));

        // Setting the function invocation scope to the new one
        Scope = scope;

        // Parse Tokens
        ASTBuilder parser = new ASTBuilder(Tokens);
        Node functionTree = parser.BuildNodes(Scope).First();
        Body = functionTree;

        Error.CheckErrors(ErrorKind.Syntax);

        Body.CheckSemantic();

        Error.CheckErrors(ErrorKind.Semantic);

    }
}