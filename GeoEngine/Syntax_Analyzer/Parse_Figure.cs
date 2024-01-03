namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuildPoint(Scope scope)
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

    Node BuildLine(Scope scope)
    {
        int lineOfCode = currentLine;

        switch (nextToken.Type)
        {
            case TokenType.Identifier:
                // generar recta random
                return null!;

            case TokenType.LeftParenthesis:
                HandlingFunction = true;
                MoveNext(2);
                Node p1 = BuildLevel1(scope);
                Expect(TokenType.Comma);
                Node p2 = BuildLevel1(scope);
                Expect(TokenType.RightParenthesis);
                HandlingFunction = false;
                return new LineFunction(p1, p2, lineOfCode);

            default:
                return null!;
                // line (let a = 42 in a, 3+3);
        }
    }

    Node BuildCircle(Scope scope)
    {
        int lineOfCode = currentLine;

        switch (nextToken.Type)
        {
            case TokenType.Identifier:
                // generar recta random
                return null!;

            case TokenType.LeftParenthesis:
                HandlingFunction = true;
                MoveNext(2);
                Node radius = BuildLevel1(scope);
                Expect(TokenType.Comma);
                Node center = BuildLevel1(scope);
                Expect(TokenType.RightParenthesis);
                HandlingFunction = false;
                return new CircleFunction(center, radius, lineOfCode);

            default:
                return null!;
                // line (let a = 42 in a, 3+3);
        }
    }
}