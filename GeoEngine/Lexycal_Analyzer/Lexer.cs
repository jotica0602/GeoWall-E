namespace GeoEngine;

// A Lexer walks through the code and splits it into Tokens
// So, Lexer:
// Recieves <code> ------- returns Tokens
public partial class Lexer
{
    #region  Lexer Object

    //actual line of code
    public readonly string sourceCode;

    private int currentPosition;
    private int line;

    //current actual token list in tokenizer
    public List<Token> tokens;

    // ===>>> CONSTRUCTOR <<<===
    public Lexer(string sourceCode)
    {
        this.sourceCode = sourceCode;
        tokens = new List<Token>();
        currentPosition = 0;
        line = 1;
    }

    #endregion

    #region Lexer Main Function: Tokenize

    // We need to split the Tokens
    // So I created a function named Tokenize, wich returns a List of Tokens.

    public List<Token> Tokenize()
    {
        // Initialize List

        while (currentPosition < sourceCode.Length)
        {
            // The character we are looking at is the current position of the sourceCode
            char currentChar = sourceCode[currentPosition];

            // If there's any white space we move on
            if (char.IsWhiteSpace(currentChar) && !(currentChar is '\n'))
            {
                MoveNext();
                continue;
            }
            //if its a line jump we add a token
            if (currentChar is '\n')
            {
                tokens.Add(new CommonToken(TokenKind.LineBreak, "\\n"));
                line++;
                MoveNext();
                continue;
            }
            // Add identifier
            else if (IsLetter(currentChar))
            {
                tokens.Add(GetIdentifier());
            }
            // Add quoted string
            else if (currentChar == '"')
            {
                tokens.Add(String());
            }
            // Add number
            else if (char.IsDigit(currentChar))
            {
                tokens.Add(Number());
            }
            // Add operator 
            else if (IsOperator(currentChar))
            {
                tokens.Add(Operator());
            }
            // Add punctuator
            else if (IsPunctuator(currentChar))
            {
                tokens.Add(Punctuator());
            }
            // Unknown token
            else
            {
                tokens.Add(new CommonToken(TokenKind.Unknown, currentChar.ToString()));
                Console.WriteLine($"!lexical error: \"{tokens.Last()}\" is not a valid token in line {line}");
                MoveNext();
            }
        }

        return tokens;
    }


    #endregion

    private Token Number()
    {

        string number = "";

        while ((currentPosition < sourceCode.Length) && (char.IsDigit(sourceCode[currentPosition]) || sourceCode[currentPosition] == '.'))
        {
            number += sourceCode[currentPosition];

            if (IsLetter(LookAhead(1)))
            {
                Console.WriteLine($"!lexical error: \"{number + LookAhead(1)}\" is not a valid token in line {line}");
                throw new Exception();
            }

            currentPosition++;
        }

        return new Data(TokenKind.Number, Double.Parse(number));
    }

    private Token GetIdentifier()
    {

        string identifier = "";

        while (currentPosition < sourceCode.Length && (IsLetterOrDigit(sourceCode[currentPosition]) || sourceCode[currentPosition] == '_'))
        {
            identifier += sourceCode[currentPosition];
            currentPosition++;
        }
        if (IsColor(identifier))
            return Color(identifier);

        else if (identifier is "_")
            return new CommonToken(TokenKind.UnderScore, identifier);

        else if (IsKeyWord(identifier))
            return KeyWord(identifier);

        else
            return new CommonToken(TokenKind.Identifier, identifier);
    }

    private Token String()
    {
        currentPosition++;
        string str = "";

        while (currentPosition < sourceCode.Length && sourceCode[currentPosition] != '"')
        {
            str += sourceCode[currentPosition];
            currentPosition++;
        }

        MoveNext();
        return new Data(TokenKind.String, str);
    }

    private Token Operator()
    {
        char _operator = sourceCode[currentPosition];

        switch (_operator)
        {
            case '+':
                MoveNext();
                return new CommonToken(TokenKind.Addition, _operator.ToString());

            case '-':
                return new CommonToken(TokenKind.Addition, _operator.ToString());

            case '*':
                MoveNext();
                return new CommonToken(TokenKind.Product, _operator.ToString());

            case '/':
                MoveNext();
                return new CommonToken(TokenKind.Division, _operator.ToString());

            case '^':
                MoveNext();
                return new CommonToken(TokenKind.Power, _operator.ToString());

            case '%':
                MoveNext();
                return new CommonToken(TokenKind.Modulus, _operator.ToString());

            case '=':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenKind.EqualsTo, _operator.ToString());
                }

                MoveNext();
                return new CommonToken(TokenKind.Equals, _operator.ToString());

            case '>':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenKind.GreaterOrEquals, _operator.ToString());
                }

                MoveNext();
                return new CommonToken(TokenKind.GreaterThan, _operator.ToString());
            default:
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenKind.LessOrEquals, _operator.ToString());
                }
                MoveNext();
                return new CommonToken(TokenKind.LessThan, _operator.ToString());
        }
    }

    private Token Punctuator()
    {
        char punctuator = sourceCode[currentPosition];
        switch (punctuator)
        {
            case '(':
                MoveNext();
                return new CommonToken(TokenKind.LeftParenthesis, punctuator.ToString());
            case ')':
                MoveNext();
                return new CommonToken(TokenKind.RightParenthesis, punctuator.ToString());
            case ',':
                MoveNext();
                return new CommonToken(TokenKind.Comma, punctuator.ToString());
            case ';':
                MoveNext();
                return new CommonToken(TokenKind.Semicolon, punctuator.ToString());
            case '{':
                MoveNext();
                return new CommonToken(TokenKind.LeftCurlyBracket, punctuator.ToString());
            case '}':
                MoveNext();
                return new CommonToken(TokenKind.RightCurlyBracket, punctuator.ToString());
            default:
                MoveNext();
                return new CommonToken(TokenKind.Quote, punctuator.ToString());
        }
    }

    private Token KeyWord(string identifier)
    {
        switch (identifier)
        {
            case "let":
                return new Keyword(TokenKind.Let);
            case "in":
                return new Keyword(TokenKind.In);
            case "if":
                return new Keyword(TokenKind.If);
            case "then":
                return new Keyword(TokenKind.Then);
            case "point":
                return new Keyword(TokenKind.Point);
            case "line":
                return new Keyword(TokenKind.Line);
            case "segment":
                return new Keyword(TokenKind.Segment);
            case "ray":
                return new Keyword(TokenKind.Ray);
            case "circle":
                return new Keyword(TokenKind.Circle);
            case "sequence":
                return new Keyword(TokenKind.Sequence);
            case "color":
                return new Keyword(TokenKind.ColorKeyWord);
            case "restore":
                return new Keyword(TokenKind.Restore);
            case "import":
                return new Keyword(TokenKind.Import);
            case "draw":
                return new Keyword(TokenKind.Draw);
            case "arc":
                return new Keyword(TokenKind.Arc);
            case "measure":
                return new Keyword(TokenKind.Measure);
            case "intersect":
                return new Keyword(TokenKind.Intersect);
            case "count":
                return new Keyword(TokenKind.Count);
            case "randoms":
                return new Keyword(TokenKind.Randoms);
            case "points":
                return new Keyword(TokenKind.Points);
            case "samples":
                return new Keyword(TokenKind.Samples);
            case "rest":
                return new Keyword(TokenKind.Rest);
            case "underScore":
                return new Keyword(TokenKind.UnderScore);
            case "and":
                return new Keyword(TokenKind.And);
            case "or":
                return new Keyword(TokenKind.Or);
            case "not":
                return new Keyword(TokenKind.Not);
            case "undefined":
                return new Data(TokenKind.Undefined, null!);
            default:
                return new Keyword(TokenKind.Else);
        }
    }

    private Token Color(string identifier)
    {
        switch (identifier)
        {
            case "red":
                return new Data(TokenKind.Color, "red");
            case "blue":
                return new Data(TokenKind.Color, "blue");
            case "yellow":
                return new Data(TokenKind.Color, "yellow");
            case "green":
                return new Data(TokenKind.Color, "green");
            case "cyan":
                return new Data(TokenKind.Color, "cyan");
            case "magenta":
                return new Data(TokenKind.Color, "magenta");
            case "white":
                return new Data(TokenKind.Color, "white");
            case "gray":
                return new Data(TokenKind.Color, "gray");
            default:
                return new Data(TokenKind.Color, "black");
        }
    }
}