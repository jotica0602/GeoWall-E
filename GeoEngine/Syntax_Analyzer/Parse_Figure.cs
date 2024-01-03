namespace GeoEngine;
public partial class ASTBuilder
{
    private Node BuildPoint(Scope scope)
    {
        // point p1(number,number) "label";
        MoveNext();
        bool hasCoordenates = false;
        bool hasLabel = false;
        int pointLine = currentLine;

        Expect(TokenType.Identifier);
        string name = previousToken.GetName();
        string label = string.Empty;

        (double, double) point = (0, 0);

        if (currentToken.Type is TokenType.LeftParenthesis)
        {
            hasCoordenates = true;
            MoveNext();
            Expect(TokenType.Number);
            point.Item1 = (double)previousToken.GetValue();
            Expect(TokenType.Comma);
            Expect(TokenType.Number);
            point.Item2 = (double)previousToken.GetValue();
            Expect(TokenType.RightParenthesis);

        }

        if (currentToken.Type is TokenType.String)
        {
            hasLabel = true;
            label = currentToken.GetName();
            MoveNext();
        }

        if (hasLabel && hasCoordenates)
        {
            Point p = new Point(label, point.Item1, point.Item2, pointLine);
            ConstantDeclaration a = new ConstantDeclaration(name, p, scope, pointLine);
            scope.Constants.Add(a);
            return null!;
        }
        else if (hasLabel)
        {
            Point p = new Point(label, pointLine);
            ConstantDeclaration a = new ConstantDeclaration(name, p, scope, pointLine);
            scope.Constants.Add(a);
            return null!;
        }
        else if (hasCoordenates)
        {
            Point p = new Point(point.Item1, point.Item2, pointLine);
            ConstantDeclaration a = new ConstantDeclaration(name, p, scope, pointLine);
            scope.Constants.Add(a);
            return null!;
        }
        else
        {
            Point p = new Point(pointLine);
            ConstantDeclaration a = new ConstantDeclaration(name, p, scope, pointLine);
            scope.Constants.Add(a);
            return null!;
        }
    }
}