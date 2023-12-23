namespace ClassLibrary;
public partial class ASTBuilder
{
    public List<Token> tokens;
    public int currentTokenIndex;
    Token currentToken;

    public ASTBuilder(List<Token> tokens)
    {
        this.tokens = tokens;
        currentTokenIndex = 0;
        currentToken = tokens[currentTokenIndex];
    }

    public Node Build()
    {
        Node ast = BuildLevel1();
        return ast;
    }

    private Node BuildLevel1()
    {
        Node leftNode = BuildLevel2();
        return leftNode;
    }

    private Node BuildLevel2()
    {
        Node leftNode = BuildLevel3();
        return leftNode;
    }

    private Node BuildLevel3()
    {
        Node leftNode = BuildLevel4();
        return leftNode;
    }

    private Node BuildLevel4()
    {
        Node leftNode = BuildLevel5();
        return leftNode;
    }

    private Node BuildLevel5()
    {
        Node leftNode = BuildAtom();
        while (IsALevel5Operator(currentToken.Type))
        {
            Node rightNode = BuildAtom();
        }
        return leftNode;
    }

    private Node BuildAtom()
    {
        switch (currentToken.Type)
        {
            case TokenType.Number:
                Node numberNode = new Literal(currentToken.GetValue());
                return numberNode;

            default:
                Node stringNode = new Literal(currentToken.GetValue());
                return stringNode;
        }
    }
}

