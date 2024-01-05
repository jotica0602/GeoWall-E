using System.IO.Compression;
using System.Xml;

namespace GeoEngine;
public partial class ASTBuilder
{
    private void MoveNext()
    {
        currentTokenIndex++;

        if (currentTokenIndex < tokens.Count)
        {
            currentToken = tokens[currentTokenIndex];
        }
    }

    private void MoveNext(int positions)
    {
        currentTokenIndex += positions;

        if (currentTokenIndex < tokens.Count)
        {
            currentToken = tokens[currentTokenIndex];
        }
    }

    private void Expect(TokenType expected)
    {

        if (currentToken.Type != expected)
        {
            if (previousToken.LineOfCode != currentLine)
            {
                new Error(ErrorKind.Syntax, ErrorCode.expected, $"\"{expected}\", \"{currentToken}\" were found", currentToken.LineOfCode - 1);
            }
            else
            {
                new Error(ErrorKind.Syntax, ErrorCode.expected, $"\"{expected}\", \"{currentToken}\" were found", currentToken.LineOfCode);
            }
        }

        MoveNext();
    }

    //  ==> (And Or) <==
    bool IsALevel1Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.And,
                TokenType.Or
            };

        return operators.Contains(operation);
    }

    // ==> (< <= >= > == !=) <==
    bool IsALevel2Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.LessThan,
                TokenType.LessOrEquals,
                TokenType.GreaterThan,
                TokenType.GreaterOrEquals,
                TokenType.EqualsTo,
                TokenType.NotEquals
            };

        return operators.Contains(operation);
    }

    // ==> (+ -) <==
    bool IsALevel3Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Addition,
                TokenType.Substraction,
            };

        return operators.Contains(operation);
    }

    // ==> (* / %) <==
    bool IsALevel4Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Product,
                TokenType.Quotient,
                TokenType.Modulo
            };

        return operators.Contains(operation);
    }

    // ==> (^) <==
    bool IsALevel5Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Power
            };

        return operators.Contains(operation);
    }

    bool IsABuiltInFunction(string identifier)
    {
        List<string> builtInFunctions = new()
        {
            "print", "import", "draw", "intersect", ""
        };

        return builtInFunctions.Contains(identifier);
    }

}