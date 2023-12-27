using System.Formats.Asn1;

namespace GeoEngine;
public partial class ASTBuilder
{
    List<Token> tokens;
    int currentTokenIndex;
    int currentLine { get => currentToken.LineOfCode; }
    Token currentToken;
    Token nextToken { get => currentTokenIndex + 1 < tokens.Count ? tokens[currentTokenIndex + 1] : throw new Exception("Token Index out of bounds"); }
    Token previousToken { get => currentTokenIndex - 1 >= 0 ? tokens[currentTokenIndex - 1] : throw new Exception("Token Index out of bounds"); }

    public ASTBuilder(List<Token> tokens)
    {
        this.tokens = tokens;
        currentTokenIndex = 0;
        currentToken = tokens[currentTokenIndex];
    }

    public List<Node> BuildNodes(Scope scope)
    {
        List<Node> nodes = new List<Node>();
        while (currentToken.Type is not TokenType.EndOfFile)
        {
            Node node = BuildLevel1(scope);
            if (node is not null)
            {
                nodes.Add(node);
                Expect(TokenType.Semicolon);
                System.Console.WriteLine($"{node.GetType()} added.");
            }
        }

        return nodes;
    }

    private Node BuildLevel1(Scope scope)
    {
        Node leftNode = BuildLevel2(scope);
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2(scope);
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        Node node = leftNode;
        return leftNode;
    }

    private Node BuildLevel2(Scope scope)
    {
        Node leftNode = BuildLevel3(scope);
        while (IsALevel2Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel3(scope);
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel3(Scope scope)
    {
        Node leftNode = BuildLevel4(scope);
        while (IsALevel3Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel4(scope);
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel4(Scope scope)
    {
        Node leftNode = BuildLevel5(scope);
        while (IsALevel4Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel5(scope);
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel5(Scope scope)
    {
        Node leftNode = BuildAtom(scope);
        while (IsALevel5Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildAtom(scope);
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildAtom(Scope scope)
    {
        switch (currentToken.Type)
        {
            case TokenType.Number:
                Node numberNode = new Literal(currentToken.GetValue(), currentLine);
                MoveNext();
                return numberNode;

            case TokenType.String:
                Node stringNode = new Literal(currentToken.GetValue(), currentLine);
                MoveNext();
                return stringNode;

            case TokenType.Identifier:
                return HandleIdentifier(scope);

            case TokenType.Addition:
                Node positiveNumber = BuildUnaryNode(TokenType.Addition, scope);
                return positiveNumber;

            case TokenType.Substraction:
                Node negativeNumber = BuildUnaryNode(TokenType.Substraction, scope);
                return negativeNumber;

            case TokenType.Not:
                Node notNode = BuildUnaryNode(TokenType.Not, scope);
                return notNode;

            case TokenType.LeftParenthesis:
                MoveNext();
                Node expression = BuildLevel1(scope);
                Expect(TokenType.RightParenthesis);
                return expression;

            case TokenType.If:
                Node ifThenElseNode = BuildTernaryNode(scope);
                return ifThenElseNode;

            case TokenType.Let:
                Node node = BuildLetNode(scope);
                return node;

            default:
                Console.WriteLine("not implemented");
                Error.ShowErrors();
                throw new Exception();
                return null!;
        }
    }
}

