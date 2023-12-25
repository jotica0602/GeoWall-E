using System.Formats.Asn1;

namespace GeoEngine;
public partial class ASTBuilder
{
    List<Token> tokens;
    public List<Node> Nodes;
    public int currentTokenIndex;
    int currentLine;
    Token currentToken;
    Token nextToken { get => currentTokenIndex + 1 < tokens.Count ? tokens[currentTokenIndex + 1] : throw new Exception("Token Index out of bounds"); }
    Token previousToken { get => currentTokenIndex - 1 >= 0 ? tokens[currentTokenIndex - 1] : throw new Exception("Token Index out of bounds"); }

    public ASTBuilder(List<Token> tokens)
    {
        this.tokens = tokens;
        Nodes = new List<Node>();
        currentTokenIndex = 0;
        currentToken = tokens[currentTokenIndex];
        currentLine=1;
    }

    public List<Node> BuildNodes()
    {
        while (currentToken.Type is not TokenType.EndOfFile)
        {
            Node node = BuildLevel1();
            if (node.Type is not NodeType.LineBreak) { Expect(TokenType.Semicolon); }
            Nodes.Add(node);
            System.Console.WriteLine($"{node.GetType()} added.");
        }

        return Nodes;
    }

    private Node BuildLevel1()
    {
        Node leftNode = BuildLevel2();
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        Node node = leftNode;
        return leftNode;
    }

    private Node BuildLevel2()
    {
        Node leftNode = BuildLevel3();
        while (IsALevel2Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel3();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel3()
    {
        Node leftNode = BuildLevel4();
        while (IsALevel3Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel4();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel4()
    {
        Node leftNode = BuildLevel5();
        while (IsALevel4Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel5();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel5()
    {
        Node leftNode = BuildAtom();
        while (IsALevel5Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildAtom();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildAtom()
    {
        switch (currentToken.Type)
        {
            case TokenType.Number:
                Node numberNode = new Literal(currentToken.GetValue());
                MoveNext();
                return numberNode;

            case TokenType.String:
                Node stringNode = new Literal(currentToken.GetValue());
                MoveNext();
                return stringNode;

            case TokenType.If:
                Node ifThenElseNode = BuildTernaryNode();
                return ifThenElseNode;

            case TokenType.LeftParenthesis:
                MoveNext();
                Node expression = BuildLevel1();
                Expect(TokenType.RightParenthesis);
                return expression;

            case TokenType.LineBreak:
                Node lineBreakNode = new LineBreak(currentToken.GetValue());
                currentLine++;
                MoveNext();
                return lineBreakNode;

            default:
                Console.WriteLine("not implemented");
                MoveNext();
                return null!;
        }
    }
}

