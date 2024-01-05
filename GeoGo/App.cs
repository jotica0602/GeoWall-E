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
        a = point(200,200);
        b = point(200,400);
        c = point (300,300);
        l = line(a,b);
        m = measure(a,b);
        c1 = circle(a,m);
        c2 = circle(b,m);
        i1 = intersect(l,c1);
        i2 = intersect(l,c2);
        draw i1;";

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
