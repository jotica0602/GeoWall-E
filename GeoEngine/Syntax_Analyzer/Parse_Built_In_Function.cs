using System.Reflection.Emit;

namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuiltInFunction(string functionName, Scope scope, int idLine)
    {
        HandlingFunction = true;
        Node builtInFunction = null!;
        List<Node> arguments = new List<Node>();
        MoveNext();

        switch (functionName)
        {
            case "print":
                Node argument = BuildLevel1(scope);
                builtInFunction = new Print(argument, idLine);
                break;

            case "draw":
                string label = "";
                argument = BuildLevel1(scope);
                if (currentToken.Type is TokenType.String)
                {
                    label = currentToken.GetName();
                    System.Console.WriteLine($"label : {label}");
                    MoveNext();
                }
                builtInFunction = new Draw(argument, label, idLine);
                break;

            case "count":
                argument = BuildLevel1(scope);
                builtInFunction = new Count(argument, idLine);
                break;

            case "measure":
                Expect(TokenType.LeftParenthesis);
                GetArguments(ref arguments, scope);
                if (arguments.Count is not 2)
                    new Error
                    (
                        ErrorKind.Semantic,
                        ErrorCode.invalid,
                        $"function \"measure\" recieves 2 argument(s) but {arguments.Count} were given.",
                        idLine
                    );
                Expect(TokenType.RightParenthesis);
                HandlingFunction = false;
                return new Measure(arguments[0], arguments[1], idLine);

            case "intersect":
                Expect(TokenType.LeftParenthesis);
                GetArguments(ref arguments, scope);
                if (arguments.Count is not 2)
                    new Error
                    (
                        ErrorKind.Semantic,
                        ErrorCode.invalid,
                        $"function \"measure\" recieves 2 argument(s) but {arguments.Count} were given.",
                        idLine
                    );
                Expect(TokenType.RightParenthesis);
                HandlingFunction = false;
                return new Intersect(arguments[0], arguments[1], idLine);
        }

        HandlingFunction = false;

        return builtInFunction;
    }
}