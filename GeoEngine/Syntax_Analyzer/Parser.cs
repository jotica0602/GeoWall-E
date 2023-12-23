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
}

