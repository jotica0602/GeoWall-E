
namespace ClassLibrary;

public class ASTBuilder
{
    int line;
    List<Token> tokens;
    int currentTokenIndex;
    Token currentToken;

    public ASTBuilder(List<Token> tokens)
    {
        this.tokens = tokens;
        currentTokenIndex = 0;
        currentToken = tokens[currentTokenIndex];
    }

    // public Node Build()
    // {
    //     Node ast = BuildLevel1();
    // }

    // private Node BuildLevel1()
    // {
    //     Node leftNode = BuildLevel2();
    // }

    // private Node BuildLevel2()
    // {
    //     Node leftNode = BuildLevel3();
    // }

    // private Node BuildLevel3()
    // {
    //     Node leftNode = BuildLevel4();
    // }

    // private Node BuildLevel4()
    // {
    //     Node leftNode = BuildLevel5();
    // }

    // private Node BuildLevel5()
    // {
    //     Node leftNode = BuildAtom();
    // }

    // private Node BuildAtom()
    // {
    //     switch (currentToken.Type)
    //     {
    //         case TokenKind.Number
    //     }
    // }
}

