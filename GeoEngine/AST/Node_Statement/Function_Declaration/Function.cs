using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace GeoEngine;
public class FunctionDeclaration : Statement
{
    public string Name { get; set; }
    public List<Node> Arguments { get; set; }
    public List<string> ArgumentsName { get; set; }
    public List<Token> Body { get; set; }
    Scope Scope { get; set; }
    public FunctionDeclaration(string name, List<Node> arguments, Scope scope, int lineOfCode) : base(lineOfCode)
    {
        Name = name;
        Scope = scope;
        Type = NodeType.Temporal;
        Arguments = arguments;
        Body = new List<Token>();
        ArgumentsName = new List<string>();
        if (CheckSemantic()) { FeedArgsName(); }
    }

    public override void Evaluate()
    {
        System.Console.WriteLine("Function");
    }

    public override bool CheckSemantic()
    {
        bool isOverWriting = IsOverWriting();
        bool argsAreVariables = CheckArgsType();
        return !isOverWriting && argsAreVariables;
    }

    bool CheckArgsType()
    {
        if (Arguments.Where(x => x is not Constant).Count() is not 0)
        {
            Error error = new Error
            (
                ErrorKind.Syntax,
                ErrorCode.invalid,
                Arguments.Count > 1 ?
                "parameters type in function declaration, they must be identifiers" :
                "parameter type in function declaration, it must be an identifier",
                LineOfCode
            );
            return false;
        }
        return true;
    }

    bool IsOverWriting()
    {
        if (Scope.Functions.Exists(x => Equals(x.Name, this.Name)))
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                $"operation, function \"{Name}\" already exists",
                LineOfCode
            );
            return true;
        }

        return false;
    }

    void FeedArgsName()
    {
        foreach (Constant arg in Arguments)
            ArgumentsName.Add(arg.Name);
    }
}