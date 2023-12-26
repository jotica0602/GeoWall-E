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
        // 2^(3+2)^2;
        // 3 +

        // 4;
        // 7%4 + 1;
        // 3 and 0;
        // !0 and 1;
        // 0 and 1;
        // 0 or 1;
        string input =
        @"3 + 2";
        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        // System.Console.WriteLine(string.Join('\n', tokens));

        if (Error.diagnostics.Count > 0)
            Error.ShowErrors();
        else
            System.Console.WriteLine("Clean of Errors!");

        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes();

        if (Error.diagnostics.Count > 0)
            Error.ShowErrors();
        else
            System.Console.WriteLine("Clean of Errors!");

        foreach (var node in nodes)
        {
            node.Evaluate();
            System.Console.WriteLine(node.Value);
        }

        Console.WriteLine();
    }

    public static void Manual()
    {
        System.Console.WriteLine("Make ur input: ");
        System.Console.Write(">>");

        string input = Console.ReadLine()!;

        if (input.Length == 0)
        {
            System.Console.WriteLine("No input");
            return;
        }

        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        System.Console.WriteLine(string.Join('\n', tokens));
    }
}
