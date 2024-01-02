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

            @"a = {1,2,3} + {1...10}; 
            a+ {100...200};
            3 and undefined;";


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

        // Environment.Exit(0);
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
            System.Console.WriteLine(node);
        }
        #endregion
    }
}
