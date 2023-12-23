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
            //    " a,b,_ = intersect(c1, c2);";
            // "_,t = { 1, 2, 3 }; ";
            // "_,_ = { 2, 3, 4 };";
            // "_ = { 2, 3 };  ";
            @"point p1;
            point p2;
            draw {p1, p2};";


        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        System.Console.WriteLine(string.Join('\n', tokens));

        if (Error.diagnostics.Count > 0)
        {
            Error.ShowErrors();
        }
        else
        {
            System.Console.WriteLine("Clean of Errors!");
        }
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
