using GeoEngine;
class Interpreter
{
    static void Main()
    {
        Auto();
    }

    public static void Auto()
    {
        string input = @"
        point p1(250,283);
        point p2(374,283)
        ray r1(250,283);
        ray r2(645,217);
        print r1;
        print r2;

        draw {r1,r2};

        color cyan;
        draw intersect(r1,r2);";

        #region Lexer
        string path = "prueba";
        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        Error.CheckErrors();
        #endregion

        #region Parser
        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);
        Error.CheckErrors();
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
