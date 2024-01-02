using GeoEngine;

class Interpreter
{
    static void Main()
    {
        Auto();
    }

    public static void Auto()
    {
        string asd = "pepe";
        string input =
        "{1,2,3,4,5,6,9...-1};";


        #region Lexer
        string path = "prueba";
        Lexer lexer = new Lexer(input);

        List<Token> tokens = lexer.Tokenize();

        // System.Console.WriteLine(string.Join('\n', tokens));
        Error.CheckErrors();
        #endregion

        #region Parser
        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);
        Error.CheckErrors();

        // Environment.Exit(0);
        #endregion

        #region Semantic Analyzer
        foreach (Expression node in nodes.Where(node => node is Expression))
            node.CheckSemantic();

        Error.CheckErrors();
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
