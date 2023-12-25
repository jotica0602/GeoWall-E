namespace GeoEngine;

// A Lexer walks through the code and splits it into Tokens
// So, Lexer:
// Recieves <code> ------- returns Tokens
public partial class Lexer
{
    #region  Lexer Object
    public readonly string sourceCode;
    private int currentPosition;

    // actual line of code 
    private int currentLine;

    // tokens list
    public List<Token> tokens;

    // ===>>> CONSTRUCTOR <<<===
    public Lexer(string sourceCode)
    {
        this.sourceCode = sourceCode;
        tokens = new List<Token>();
        currentPosition = 0;
        currentLine = 1;
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
                currentPosition++;
                continue;
            }
            //if its a line jump we add a token
            if (currentChar is '\n')
            {
                tokens.Add(new Data(TokenType.LineBreak, currentLine));
                currentLine++;
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
                tokens.Add(new CommonToken(TokenType.Unknown, currentChar.ToString()));
                Error newError = new Error(ErrorKind.Lexycal, ErrorCode.Unknown, $"token: {currentChar}", currentLine);
                MoveNext();
            }
        }
        tokens.Add(new Keyword(TokenType.EndOfFile));
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
                string badToken = (string)GetIdentifier().GetName();
                Error newError = new Error(ErrorKind.Lexycal, ErrorCode.Invalid, $"token \"{badToken}\"", currentLine);
            }

            currentPosition++;
        }

        return new Data(TokenType.Number, Double.Parse(number));
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
            return new CommonToken(TokenType.UnderScore, identifier);

        else if (IsKeyWord(identifier))
            return KeyWord(identifier);

        else
            return new CommonToken(TokenType.Identifier, identifier);
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
        return new Data(TokenType.String, str);
    }

    private Token Operator()
    {
        char _operator = sourceCode[currentPosition];

        switch (_operator)
        {
            case '+':
                MoveNext();
                return new CommonToken(TokenType.Addition, _operator.ToString());

            case '-':
                MoveNext();
                return new CommonToken(TokenType.Substraction, _operator.ToString());

            case '*':
                MoveNext();
                return new CommonToken(TokenType.Product, _operator.ToString());

            case '/':
                MoveNext();
                return new CommonToken(TokenType.Quotient, _operator.ToString());

            case '^':
                MoveNext();
                return new CommonToken(TokenType.Power, _operator.ToString());

            case '%':
                MoveNext();
                return new CommonToken(TokenType.Modulo, _operator.ToString());

            case '=':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.EqualsTo, "==");
                }

                MoveNext();
                return new CommonToken(TokenType.Equals, "=");

            case '!':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.NotEquals, "!=");
                }
                MoveNext();
                return new CommonToken(TokenType.Not, "!");

            case '>':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.GreaterOrEquals, ">=");
                }
                MoveNext();
                return new CommonToken(TokenType.GreaterThan, ">");

            default:
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.LessOrEquals, "<=");
                }
                MoveNext();
                return new CommonToken(TokenType.LessThan, "<");
        }
    }

    private Token Punctuator()
    {
        char punctuator = sourceCode[currentPosition];
        switch (punctuator)
        {
            case '(':
                MoveNext();
                return new CommonToken(TokenType.LeftParenthesis, punctuator.ToString());
            case ')':
                MoveNext();
                return new CommonToken(TokenType.RightParenthesis, punctuator.ToString());
            case ',':
                MoveNext();
                return new CommonToken(TokenType.Comma, punctuator.ToString());
            case ';':
                MoveNext();
                return new CommonToken(TokenType.Semicolon, punctuator.ToString());
            case '{':
                MoveNext();
                return new CommonToken(TokenType.LeftCurlyBracket, punctuator.ToString());
            case '}':
                MoveNext();
                return new CommonToken(TokenType.RightCurlyBracket, punctuator.ToString());
            default:
                MoveNext();
                return new CommonToken(TokenType.Quote, punctuator.ToString());
        }
    }

    private Token KeyWord(string identifier)
    {
        switch (identifier)
        {
            case "let":
                return new Keyword(TokenType.Let);
            case "in":
                return new Keyword(TokenType.In);
            case "if":
                return new Keyword(TokenType.If);
            case "then":
                return new Keyword(TokenType.Then);
            case "point":
                return new Keyword(TokenType.Point);
            case "line":
                return new Keyword(TokenType.Line);
            case "segment":
                return new Keyword(TokenType.Segment);
            case "ray":
                return new Keyword(TokenType.Ray);
            case "circle":
                return new Keyword(TokenType.Circle);
            case "sequence":
                return new Keyword(TokenType.Sequence);
            case "color":
                return new Keyword(TokenType.ColorKeyWord);
            case "restore":
                return new Keyword(TokenType.Restore);
            case "import":
                return new Keyword(TokenType.Import);
            case "draw":
                return new Keyword(TokenType.Draw);
            case "arc":
                return new Keyword(TokenType.Arc);
            case "measure":
                return new Keyword(TokenType.Measure);
            case "intersect":
                return new Keyword(TokenType.Intersect);
            case "count":
                return new Keyword(TokenType.Count);
            case "randoms":
                return new Keyword(TokenType.Randoms);
            case "points":
                return new Keyword(TokenType.Points);
            case "samples":
                return new Keyword(TokenType.Samples);
            case "rest":
                return new Keyword(TokenType.Rest);
            case "underScore":
                return new Keyword(TokenType.UnderScore);
            case "and":
                return new Keyword(TokenType.And);
            case "or":
                return new Keyword(TokenType.Or);
            case "not":
                return new Keyword(TokenType.Not);
            case "undefined":
                return new Data(TokenType.Undefined, null!);
            default:
                return new Keyword(TokenType.Else);
        }
    }

    private Token Color(string identifier)
    {
        switch (identifier)
        {
            case "red":
                return new Data(TokenType.Color, "red");
            case "blue":
                return new Data(TokenType.Color, "blue");
            case "yellow":
                return new Data(TokenType.Color, "yellow");
            case "green":
                return new Data(TokenType.Color, "green");
            case "cyan":
                return new Data(TokenType.Color, "cyan");
            case "magenta":
                return new Data(TokenType.Color, "magenta");
            case "white":
                return new Data(TokenType.Color, "white");
            case "gray":
                return new Data(TokenType.Color, "gray");
            default:
                return new Data(TokenType.Color, "black");
        }
    }
}