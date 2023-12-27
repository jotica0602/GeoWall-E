using System.Xml;

namespace GeoEngine;
public partial class ASTBuilder
{
    Expression BuildUnaryNode(TokenType _operator, Scope scope)
    {
        switch (_operator)
        {
            case TokenType.Addition:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    PositiveNumber node = new PositiveNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel4(scope);
                    return node;
                }
                else
                {
                    PositiveNumber node = new PositiveNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }

            case TokenType.Substraction:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    NegativeNumber node = new NegativeNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel4(scope);
                    return node;
                }
                else
                {
                    NegativeNumber node = new NegativeNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }

            default:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    Not node = new Not(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel2(scope);
                    return node;
                }
                else
                {
                    Not node = new Not(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }
        }
    }

    Node BuildBinaryNode(Node leftNode, TokenType operation, Node rightNode)
    {

        switch (operation)
        {
            case TokenType.And:
                BinaryExpression andNode = new And(leftNode, rightNode, currentLine);
                leftNode = andNode;
                break;

            case TokenType.Or:
                BinaryExpression orNode = new Or(leftNode, rightNode, currentLine);
                leftNode = orNode;
                break;

            case TokenType.GreaterOrEquals:
                BinaryExpression greaterOrEqualsNode = new GreaterOrEquals(leftNode, rightNode, currentLine);
                leftNode = greaterOrEqualsNode;
                break;

            case TokenType.GreaterThan:
                BinaryExpression greaterThanNode = new GreaterThan(leftNode, rightNode, currentLine);
                leftNode = greaterThanNode;
                break;

            case TokenType.LessOrEquals:
                BinaryExpression lessOrEqualsNode = new LessOrEquals(leftNode, rightNode, currentLine);
                leftNode = lessOrEqualsNode;
                break;

            case TokenType.LessThan:
                BinaryExpression lesserThanNode = new LessThan(leftNode, rightNode, currentLine);
                leftNode = lesserThanNode;
                break;

            case TokenType.EqualsTo:
                BinaryExpression equalsTo = new EqualsTo(leftNode, rightNode, currentLine);
                leftNode = equalsTo;
                break;

            case TokenType.NotEquals:
                BinaryExpression notEquals = new NotEquals(leftNode, rightNode, currentLine);
                leftNode = notEquals;
                break;

            case TokenType.Addition:
                BinaryExpression additionNode = new Addition(leftNode, rightNode, currentLine);
                leftNode = additionNode;
                break;

            case TokenType.Substraction:
                BinaryExpression difference = new Difference(leftNode, rightNode, currentLine);
                leftNode = difference;
                break;

            case TokenType.Product:
                BinaryExpression productNode = new Product(leftNode, rightNode, currentLine);
                leftNode = productNode;
                break;

            case TokenType.Quotient:
                BinaryExpression quotientNode = new Quotient(leftNode, rightNode, currentLine);
                leftNode = quotientNode;
                break;

            case TokenType.Modulo:
                BinaryExpression moduloNode = new Modulo(leftNode, rightNode, currentLine);
                leftNode = moduloNode;
                break;

            case TokenType.Power:
                BinaryExpression powerNode = new Power(leftNode, rightNode, currentLine);
                leftNode = powerNode;
                break;
        }

        return leftNode;
    }

    private Node BuildTernaryNode(Scope scope)
    {
        int expressionLine = currentLine;
        MoveNext();
        Node condition = BuildLevel1(scope);
        Expect(TokenType.Then);
        Node trueNode = BuildLevel1(scope);
        Expect(TokenType.Else);
        Node falseNode = BuildLevel1(scope);
        IfThenElse node = new IfThenElse(condition, trueNode, falseNode, expressionLine);
        return node;
    }

    private Node HandleIdentifier(Scope scope)
    {
        string idName = currentToken.GetName();
        int idLine = currentLine;
        switch (nextToken.Type)
        {
            case TokenType.Equals:
                MoveNext(2);
                ConstantDeclaration constant = new ConstantDeclaration(BuildLevel1(scope), idLine);
                for (var actualScope = scope; actualScope is not null; actualScope = actualScope.Parent)
                    if (actualScope.Constants.ContainsKey(idName))
                    {
                        Error error = new Error(ErrorKind.Semantic, ErrorCode.Invalid, $"operation, cannot redefine constant \"{idName}\"", idLine);
                        Error.ShowErrors();
                    }
                scope.Constants.Add(idName, constant);
                return null!;

            default:
                Constant node = new Constant(scope, idName, idLine);
                MoveNext();
                return node;
        }
    }

    private Node BuildLetNode(Scope scope)
    {
        Let node = new Let(currentLine);
        Scope child = scope.MakeChild();
        MoveNext();

        while (currentToken.Type is not TokenType.In)
        {
            Node instruction = BuildLevel1(child);
            if (instruction is not null)
                node.Instructions.Add(instruction);
            Expect(TokenType.Semicolon);
        }

        MoveNext();
        node.InNode = BuildLevel1(child);
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
            Error error = new Error(ErrorKind.Syntax, ErrorCode.Expected, $"token \"{expected}\"", currentToken.LineOfCode);
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