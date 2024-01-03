namespace GeoEngine;
public partial class ASTBuilder
{
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
}