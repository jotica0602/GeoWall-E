using System.Formats.Asn1;

namespace GeoEngine;
public partial class ASTBuilder
{
    public List<Token> tokens;
    public List<Node> Nodes;
    public int currentTokenIndex;
    Token currentToken;

    public ASTBuilder(List<Token> tokens)
    {
        this.tokens = tokens;
        Nodes = new List<Node>();
        currentTokenIndex = 0;
        currentToken = tokens[currentTokenIndex];
    }

    public void BuildNodes()
    {
        while (currentToken.Type is not TokenType.EndOfFile)
        {
            Node node = BuildNode();
            Expect(TokenType.Semicolon);
            Nodes.Add(node);
        }
    }

    public Node BuildNode()
    {
        Node leftNode = BuildLevel1();
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        Node node = leftNode;
        return node;
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
        return leftNode;
    }

    private Node BuildLevel2()
    {
        Node leftNode = BuildLevel3();
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel3()
    {
        Node leftNode = BuildLevel4();
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2();
            leftNode = BuildBinaryNode(leftNode, operation, rightNode);
        }
        return leftNode;
    }

    private Node BuildLevel4()
    {
        Node leftNode = BuildLevel5();
        while (IsALevel1Operator(currentToken.Type))
        {
            TokenType operation = currentToken.Type;
            MoveNext();
            Node rightNode = BuildLevel2();
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
            Node rightNode = BuildLevel2();
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

            // case TokenType.LineBreak:
                

            default:
                Node stringNode = new Literal(currentToken.GetValue());
                MoveNext();
                return stringNode;
        }
    }
}

