﻿namespace ClassLibrary;
// A Lexer walks through the code and splits it into Tokens
// So, Lexer:
// Recieves <code> ------- returns Tokens
public class Lexer
{
    #region  Lexer Object

    public readonly string sourceCode;
    private int currentPosition;

    public Lexer(string sourceCode)
    {
        this.sourceCode = sourceCode;
        currentPosition = 0;
    }

    #endregion

    #region Lexer Main Function: Tokenize

    // We need to split the Tokens
    // So I created a function named Tokenize, wich returns a List of Tokens.

    public List<Token> Tokenize()
    {
        // Initialize List
        List<Token> tokens = new();

        while (currentPosition < sourceCode.Length)
        {
            // The character we are looking at is the current position of the sourceCode
            char currentChar = sourceCode[currentPosition];

            // If there's any white space we move on
            if (char.IsWhiteSpace(currentChar))
            {
                currentPosition++;
                continue;
            }

            // Add identifier
            else if (IsLetter(currentChar))
            {
                tokens.Add(IdKind());
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
                Console.WriteLine($"!lexical error: \"{tokens.Last()}\" is not a valid token.");
                currentPosition++;
            }
        }

        if (tokens.Last().GetName() != ";")
        {
            Console.WriteLine("!syntax error: expression must end with \";\".");
            throw new Exception();
        }
        return tokens;
    }

    #endregion

    #region TokenKind Adder Functions

    private Token Number()
    {

        string number = "";

        while ((currentPosition < sourceCode.Length) && (char.IsDigit(sourceCode[currentPosition]) || sourceCode[currentPosition] == '.'))
        {
            number += sourceCode[currentPosition];

            if (IsLetter(LookAhead(1)))
            {
                Console.WriteLine($"!lexical error: \"{number + LookAhead(1)}\" is not a valid token.");
                throw new Exception();
            }

            currentPosition++;
        }

        return new Data(TokenKind.Number, Double.Parse(number));
    }

    private Token IdKind()
    {

        string idkind = "";

        while (currentPosition < sourceCode.Length && (IsLetterOrDigit(sourceCode[currentPosition]) || sourceCode[currentPosition] == '_'))
        {
            idkind += sourceCode[currentPosition];
            currentPosition++;
        }

        if (IsKeyWord(idkind))
        {
            return KeyWord(idkind);
        }

        else
        {
            return new CommonToken(TokenKind.Identifier, idkind);
        }
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

        if (_operator == '+')
        {
            MoveNext();
            return new CommonToken(TokenKind.Addition, _operator.ToString());
        }

        else if (_operator == '-')
        {
            MoveNext();
            return new CommonToken(TokenKind.Substraction, _operator.ToString());
        }

        else if (_operator == '*')
        {
            MoveNext();
            return new CommonToken(TokenKind.Product, _operator.ToString());
        }

        else if (_operator == '/')
        {
            MoveNext();
            return new CommonToken(TokenKind.Division, _operator.ToString());
        }

        else if (_operator == '^')
        {
            MoveNext();
            return new CommonToken(TokenKind.Power, _operator.ToString());
        }

        else if (_operator == '%')
        {
            MoveNext();
            return new CommonToken(TokenKind.Modulus, _operator.ToString());
        }


        else if (_operator == '<' && LookAhead(1) == '=')
        {
            MoveNext(2);
            return new CommonToken(TokenKind.LessOrEquals, "<=");
        }

        else if (_operator == '<')
        {
            MoveNext();
            return new CommonToken(TokenKind.LessThan, _operator.ToString());
        }

        else if (_operator == '>' && LookAhead(1) == '=')
        {
            MoveNext(2);
            return new CommonToken(TokenKind.GreaterOrEquals, ">=");
        }

        else if (_operator == '>')
        {
            MoveNext();
            return new CommonToken(TokenKind.GreaterThan, _operator.ToString());
        }

        else if (_operator == '!' && LookAhead(1) == '=')
        {
            MoveNext(2);
            return new CommonToken(TokenKind.NotEquals, "!=");
        }

        else if (_operator == '=' && LookAhead(1) == '=')
        {
            MoveNext(2);
            return new CommonToken(TokenKind.EqualsTo, "==");
        }

        else if (_operator == '=')
        {
            MoveNext();
            return new CommonToken(TokenKind.Equals, _operator.ToString());
        }

        else
        {
            MoveNext();
            return new CommonToken(TokenKind.Or, _operator.ToString());
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

            case '"':
                MoveNext();
                return new CommonToken(TokenKind.Quote, punctuator.ToString());

            default:
                MoveNext();
                return new CommonToken(TokenKind.FullStop, punctuator.ToString());
        }
    }

    private Token KeyWord(string idkind)
    {
        switch (idkind)
        {
            case "let":
                return new Keyword(TokenKind.Let);

            case "in":
                return new Keyword(TokenKind.In);

            case "if":
                return new Keyword(TokenKind.If);

            default:
                return new Keyword(TokenKind.Else);

        }
    }

    #endregion

    #region Utility Functions 

    private void MoveNext(int positions)
    {
        currentPosition += positions;
    }

    private void MoveNext()
    {
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
                '=','<','>',
            };

        return Operators.Contains(currentChar);
    }

    private static bool IsPunctuator(char currentChar)
    {
        List<char> Punctuators = new()
            {
                '(', ')', ';', ',','.','"','{','}'
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

    #endregion
}