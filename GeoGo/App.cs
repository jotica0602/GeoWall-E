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
        string input =
            @"point p1;
            point p2;
            draw {p1, p2};";

        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        System.Console.WriteLine(string.Join('\n', tokens)); 
    }

    public static void Manual()
    {
        System.Console.WriteLine("Make ur input: ");
        System.Console.Write(">>");
        string input = Console.ReadLine()!;

        if(input.Length == 0)
        {
            System.Console.WriteLine("No input");
            return;
        }

        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        System.Console.WriteLine(string.Join('\n', tokens));
    }
}
