using GeoEngine;

class Interpreter
{
    static void Main()
    {
        Auto();
        // Manual();
    }

    public static void Auto()
    {

        /* let                  
                a = 5;          
                b = let         <== redefinition error
                        a = 4;
                    in a + 2; 
            in a + b 
        */

        string input =
        @"
            let a = 
                let b = 
                    let c = 21; 
                    in c + 1; 
                in b; 
            in a - 22;
        ";

        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        // System.Console.WriteLine(string.Join('\n', tokens));

        if (Error.diagnostics.Count > 0)
            Error.ShowErrors();
        else
            System.Console.WriteLine("Clean of Errors!");

        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);

        if (Error.diagnostics.Count > 0)
            Error.ShowErrors();
        else
            System.Console.WriteLine("Clean of Errors!");

        foreach (Expression node in nodes.Where(node => node is Expression))
        {
            node.Evaluate();
            System.Console.WriteLine(node.Value);
        }
    }
}
