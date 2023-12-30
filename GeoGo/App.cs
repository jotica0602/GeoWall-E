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
        fib(x) = if(x>1) then fib(x-1) + fib(x-2) + f(x) else 1;
        f(x) = x;

        let 
            b = 
                let 
                    m = 5; 
                    in fib(m) + f(m+10);
        in b;
        ";

        #region Lexer
        Lexer lexer = new Lexer(input);
        
        List<Token> tokens = lexer.Tokenize();
        // System.Console.WriteLine(string.Join('\n', tokens));
        Error.CheckErrors(ErrorKind.Lexycal);
        #endregion

        #region Parser
        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);
        Error.CheckErrors(ErrorKind.Syntax);
        #endregion

        #region Semantic Analyzer
        foreach (Expression node in nodes.Where(node => node is Expression))
            node.CheckSemantic();

        Error.CheckErrors(ErrorKind.Semantic);
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
