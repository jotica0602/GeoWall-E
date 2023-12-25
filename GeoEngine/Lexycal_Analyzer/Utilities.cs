namespace GeoEngine;
public partial class Lexer
{
    private void MoveNext(int positions)
    {
        if (currentPosition + positions < sourceCode.Length)
            currentPosition += positions;
    }

    private void MoveNext()
    {
        if (currentPosition + 1 <= sourceCode.Length)
            currentPosition++;
    }

    private char LookAhead(int positions)
    {
        if ((currentPosition + positions) >= sourceCode.Length)
        {
            return ' ';
        }

        return sourceCode[currentPosition + positions];
    }

    private static bool IsLetter(char c) => char.IsLetter(c) || c == '_';

    private static bool IsLetterOrDigit(char c) => char.IsLetterOrDigit(c) || c == '_';

    private static bool IsOperator(char currentChar)
    {
        List<char> Operators = new()
            {
                '+', '-', '*', '/', '^','%',
                '=','<','>','!'
            };

        return Operators.Contains(currentChar);
    }

    private static bool IsPunctuator(char currentChar)
    {
        List<char> Punctuators = new()
            {
                '(', ')', ';', ',', '.', '"', '{' ,'}'
            };

        return Punctuators.Contains(currentChar);
    }

    private static bool IsKeyWord(string idkind)
    {
        List<string> keywords = new()
            {
                "let", "in", "if","then","else","point",
                "line", "segment", "ray", "circle", "sequence",
                "color","restore","import","draw","arc","measure",
                "intersect","count","randoms","points","samples","rest",
                "underScore","and","or","not"
            };

        return keywords.Contains(idkind);
    }

    private bool IsColor(string identifier)
    {
        List<string> colors = new List<string>
        {
            "red", "blue","yellow","green","cyan",",magenta","white","gray","black"
        };

        return colors.Contains(identifier);
    }
}