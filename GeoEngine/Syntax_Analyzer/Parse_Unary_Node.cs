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
}