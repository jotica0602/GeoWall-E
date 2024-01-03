namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuiltInFunction(string functionName, Scope scope, int idLine)
    {
        // HandlingFunction = true;
        Node builtInFunction = null!;
        MoveNext();

        switch (functionName)
        {
            case "print":
                Node argument = BuildLevel1(scope);
                builtInFunction = new Print(argument, idLine);
                break;
            case "draw":
                argument = BuildLevel1(scope);
                builtInFunction = new Draw(argument, idLine);
                break;
        }

        // HandlingFunction = false;

        return builtInFunction;
    }
}