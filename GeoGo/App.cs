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
            //    " a,b,_ = intersect(c1, c2);";
            // "_,t = { 1, 2, 3 }; ";
            // "_,_ = { 2, 3, 4 };";
            "_ = { 2, 3 };  ";


        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        System.Console.WriteLine(string.Join('\n', tokens));

    }
}
