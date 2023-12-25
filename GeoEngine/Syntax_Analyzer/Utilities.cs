namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuildBinaryNode(Node leftNode, TokenType operation, Node rightNode)
    {
        switch (operation)
        {
            case TokenType.And:
                BinaryExpression andNode = new And(leftNode, rightNode);
                leftNode = andNode;
                break;

            case TokenType.Or:
                BinaryExpression orNode = new Or(leftNode, rightNode);
                leftNode = orNode;
                break;

            case TokenType.GreaterOrEquals:
                BinaryExpression greaterOrEqualsNode = new GreaterOrEquals(leftNode, rightNode);
                leftNode = greaterOrEqualsNode;
                break;

            case TokenType.GreaterThan:
                BinaryExpression greaterThanNode = new GreaterThan(leftNode, rightNode);
                leftNode = greaterThanNode;
                break;

            case TokenType.LessOrEquals:
                BinaryExpression lessOrEqualsNode = new LessOrEquals(leftNode, rightNode);
                leftNode = lessOrEqualsNode;
                break;

            case TokenType.LessThan:
                BinaryExpression lesserThanNode = new LessThan(leftNode, rightNode);
                leftNode = lesserThanNode;
                break;

            case TokenType.EqualsTo:
                BinaryExpression equalsTo = new EqualsTo(leftNode, rightNode);
                leftNode = equalsTo;
                break;
            
            case TokenType.NotEquals:
                BinaryExpression notEquals = new NotEquals(leftNode,rightNode);
                leftNode = notEquals;
                break;

            case TokenType.Addition:
                BinaryExpression additionNode = new Addition(leftNode, rightNode);
                leftNode = additionNode;
                break;

            case TokenType.Substraction:
                BinaryExpression difference = new Difference(leftNode, rightNode);
                leftNode = difference;
                break;

            case TokenType.Product:
                BinaryExpression productNode = new Product(leftNode, rightNode);
                leftNode = productNode;
                break;

            case TokenType.Quotient:
                BinaryExpression quotientNode = new Quotient(leftNode, rightNode);
                leftNode = quotientNode;
                break;

            case TokenType.Modulo:
                BinaryExpression moduloNode = new Modulo(leftNode, rightNode);
                leftNode = moduloNode;
                break;

            case TokenType.Power:
                BinaryExpression powerNode = new Power(leftNode, rightNode);
                leftNode = powerNode;
                break;
        }

        return leftNode;
    }

    private Node BuildTernaryNode()
    {
        MoveNext();
        Node condition = BuildLevel1();
        Expect(TokenType.Then);
        Node trueNode = BuildLevel1();
        Expect(TokenType.Else);
        Node falseNode = BuildLevel1();
        IfThenElse node = new IfThenElse(condition,trueNode,falseNode);
        return node;
    }

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
            Error error = new Error(ErrorKind.Syntax, ErrorCode.Expected, $"\"{expected}\"", currentLine);
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

    bool IsABuiltInFunction(Token Identifier)
    {
        List<string> builtInFunctions = new()
        {
            "print", "sin","cos","log","exp","sqrt"
        };

        return builtInFunctions.Contains(Identifier.GetName());
    }

}