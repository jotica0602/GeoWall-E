using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using GeoEngine;

class Interpreter
{
    static void Main()
    {
        Auto();
    }

    public static void Auto()
    {
        string input =
        @"
            fact(x) = if(n>1) then fib(n-1) + fib(n-2) else 1;
            fib(5);
        ";

        #region Lexer
        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        // System.Console.WriteLine(string.Join('\n', tokens));

        if (Error.diagnostics.Count > 0) { Error.ShowErrors(); }
        else { System.Console.WriteLine("Clean of Errors!"); }
        #endregion

        #region Parser
        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);


        if (Error.diagnostics.Count > 0) { Error.ShowErrors(); }
        else { System.Console.WriteLine("Clean of Errors!"); }
        #endregion

        #region Semantic Analyzer
        #endregion

        #region Interpreter
        foreach (Expression node in nodes.Where(node => node is Expression))
        {
            node.Evaluate();
            System.Console.WriteLine(node.Value);
        }
        #endregion
    }
}
