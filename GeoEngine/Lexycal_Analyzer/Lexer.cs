namespace GeoEngine;

// A Lexer walks through the code and splits it into Tokens
// So, Lexer:
// Recieves <code> ------- returns Tokens
public partial class Lexer
{
    #region  Lexer Object
    public readonly string sourceCode;
    private int currentPosition;
    bool Empty { get; set; }

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


        //clean error every time sourceCode is modified
        Error.diagnostics.Clear();

        //check if its empty
        if ((sourceCode == string.Empty) || (sourceCode.Length < 1) || (sourceCode is null))
        {
            Error error = new Error(ErrorKind.Lexycal, ErrorCode.empty, "source code", 0);
            Empty = true;
        }
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
            if (currentChar is '\n')
            {
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
                tokens.Add(new CommonToken(TokenType.Unknown, currentChar.ToString(), currentLine));
                Error newError = new Error(ErrorKind.Lexycal, ErrorCode.unknown, $"token: {currentChar}", currentLine);
                MoveNext();
            }
        }
        if (!Empty)
            tokens.Add(new Keyword(TokenType.EndOfFile, tokens[tokens.Count - 1].LineOfCode));
            
        return tokens;
    }


    #endregion

    private Token Number()
    {

        string number = "";

        while ((currentPosition < sourceCode.Length) && (char.IsDigit(sourceCode[currentPosition]) || (sourceCode[currentPosition] == '.' && sourceCode[currentPosition + 1] is not '.')))
        {
            if (IsLetter(LookAhead(1)))
            {
                string badToken = (string)GetIdentifier().GetName();
                Error newError = new Error(ErrorKind.Lexycal, ErrorCode.invalid, $"token \"{number}{badToken}\"", currentLine);
                break;
            }
            number += sourceCode[currentPosition];


            currentPosition++;
        }

        number = number.Replace('.', ',');

        return new Data(TokenType.Number, Double.Parse(number), currentLine);
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

        else if (IsKeyWord(identifier))
            return KeyWord(identifier);

        else
            return new CommonToken(TokenType.Identifier, identifier, currentLine);
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
        return new Data(TokenType.String, str, currentLine);
    }

    private Token Operator()
    {
        char _operator = sourceCode[currentPosition];

        switch (_operator)
        {
            case '+':
                MoveNext();
                return new CommonToken(TokenType.Addition, _operator.ToString(), currentLine);

            case '-':
                MoveNext();
                return new CommonToken(TokenType.Substraction, _operator.ToString(), currentLine);

            case '*':
                MoveNext();
                return new CommonToken(TokenType.Product, _operator.ToString(), currentLine);

            case '/':
                MoveNext();
                return new CommonToken(TokenType.Quotient, _operator.ToString(), currentLine);

            case '^':
                MoveNext();
                return new CommonToken(TokenType.Power, _operator.ToString(), currentLine);

            case '%':
                MoveNext();
                return new CommonToken(TokenType.Modulo, _operator.ToString(), currentLine);

            case '=':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.EqualsTo, "==", currentLine);
                }

                MoveNext();
                return new CommonToken(TokenType.Assignment, "=", currentLine);

            case '!':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.NotEquals, "!=", currentLine);
                }
                MoveNext();
                return new CommonToken(TokenType.Not, "!", currentLine);

            case '>':
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.GreaterOrEquals, ">=", currentLine);
                }
                MoveNext();
                return new CommonToken(TokenType.GreaterThan, ">", currentLine);

            default:
                if (LookAhead(1) is '=')
                {
                    MoveNext(2);
                    return new CommonToken(TokenType.LessOrEquals, "<=", currentLine);
                }
                MoveNext();
                return new CommonToken(TokenType.LessThan, "<", currentLine);
        }
    }

    private Token Punctuator()
    {
        char punctuator = sourceCode[currentPosition];
        switch (punctuator)
        {
            case '(':
                MoveNext();
                return new CommonToken(TokenType.LeftParenthesis, punctuator.ToString(), currentLine);
            case ')':
                MoveNext();
                return new CommonToken(TokenType.RightParenthesis, punctuator.ToString(), currentLine);
            case ',':
                MoveNext();
                return new CommonToken(TokenType.Comma, punctuator.ToString(), currentLine);
            case ';':
                MoveNext();
                return new CommonToken(TokenType.Semicolon, punctuator.ToString(), currentLine);
            case '{':
                MoveNext();
                return new CommonToken(TokenType.LeftCurlyBracket, punctuator.ToString(), currentLine);
            case '}':
                MoveNext();
                return new CommonToken(TokenType.RightCurlyBracket, punctuator.ToString(), currentLine);
            case '.':
                if (LookAhead(1) is '.' && LookAhead(2) is '.')
                {
                    MoveNext(3);
                    return new CommonToken(TokenType.TriplePoint, "...", currentLine);
                }
                else
                {
                    MoveNext();
                    Token unknown = new CommonToken(TokenType.Unknown, ".", currentLine);
                    new Error(ErrorKind.Lexycal, ErrorCode.invalid, "token \".\"", currentLine);
                    return unknown!;
                }
            default:
                MoveNext();
                return new CommonToken(TokenType.Quote, punctuator.ToString(), currentLine);
        }
    }

    private Token KeyWord(string identifier)
    {
        switch (identifier)
        {
            case "let":
                return new Keyword(TokenType.Let, currentLine);
            case "in":
                return new Keyword(TokenType.In, currentLine);
            case "if":
                return new Keyword(TokenType.If, currentLine);
            case "then":
                return new Keyword(TokenType.Then, currentLine);
            case "point":
                return new Keyword(TokenType.Point, currentLine);
            case "line":
                return new Keyword(TokenType.Line, currentLine);
            case "segment":
                return new Keyword(TokenType.Segment, currentLine);
            case "ray":
                return new Keyword(TokenType.Ray, currentLine);
            case "circle":
                return new Keyword(TokenType.Circle, currentLine);
            case "sequence":
                return new Keyword(TokenType.Sequence, currentLine);
            case "color":
                return new Keyword(TokenType.ColorKeyWord, currentLine);
            case "restore":
                return new Keyword(TokenType.Restore, currentLine);
            case "import":
                return new Keyword(TokenType.Import, currentLine);
            case "draw":
                return new Keyword(TokenType.Draw, currentLine);
            case "arc":
                return new Keyword(TokenType.Arc, currentLine);
            case "measure":
                return new Keyword(TokenType.Measure, currentLine);
            case "intersect":
                return new Keyword(TokenType.Intersect, currentLine);
            case "count":
                return new Keyword(TokenType.Count, currentLine);
            case "randoms":
                return new Keyword(TokenType.Randoms, currentLine);
            case "points":
                return new Keyword(TokenType.Points, currentLine);
            case "samples":
                return new Keyword(TokenType.Samples, currentLine);
            case "rest":
                return new Keyword(TokenType.Rest, currentLine);
            case "and":
                return new Keyword(TokenType.And, currentLine);
            case "or":
                return new Keyword(TokenType.Or, currentLine);
            case "not":
                return new Keyword(TokenType.Not, currentLine);
            case "undefined":
                return new Data(TokenType.Undefined, null!, currentLine);
            default:
                return new Keyword(TokenType.Else, currentLine);
        }
    }

    private Token Color(string identifier)
    {
        switch (identifier)
        {
            case "red":
                return new Data(TokenType.Color, "red", currentLine);
            case "blue":
                return new Data(TokenType.Color, "blue", currentLine);
            case "yellow":
                return new Data(TokenType.Color, "yellow", currentLine);
            case "green":
                return new Data(TokenType.Color, "green", currentLine);
            case "cyan":
                return new Data(TokenType.Color, "cyan", currentLine);
            case "magenta":
                return new Data(TokenType.Color, "magenta", currentLine);
            case "white":
                return new Data(TokenType.Color, "white", currentLine);
            case "gray":
                return new Data(TokenType.Color, "gray", currentLine);
            default:
                return new Data(TokenType.Color, "black", currentLine);
        }
    }
}