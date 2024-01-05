using System.Data;

namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuildPoint(Scope scope)
    {
        // point p1(number,number) "label";
        // point(100,100);
        int pointLine = currentLine;
        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> points = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                points.Add(new Point(idLine));
            }
            Sequence idSequence = new FiniteSequence(points, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            MoveNext();
            scope.Constants.Add(a);
            return null!;
        }
        if (nextToken.Type is TokenType.LeftParenthesis)
        {
            MoveNext(2);
            Node x = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node y = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            return new PointFunction(x, y, pointLine);
        }

        else
        {

            MoveNext();
            bool hasCoordenates = false;
            bool hasLabel = false;
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
                System.Console.WriteLine("has coord");
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

    Node BuildLine(Scope scope)
    {
        int lineOfCode = currentLine;
        bool hasLabel = false;
        string lineName = string.Empty;
        // bool isRandom = false;
        string label = string.Empty;

        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> lines = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                lines.Add(new Line(idLine));
            }
            Sequence idSequence = new FiniteSequence(lines, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            scope.Constants.Add(a);
            MoveNext();
            return null!;
        }

        if (nextToken.Type is TokenType.Identifier)
        {
            // isRandom = true;
            MoveNext(2);
            lineName = previousToken.GetName();
            if (currentToken.Type is TokenType.String)
            {
                hasLabel = true;
                label = currentToken.GetName();
                MoveNext();
            }
        }

        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            MoveNext(2);
            Node p1 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node p2 = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            HandlingFunction = false;
            return new LineFunction(p1, p2, lineOfCode);
        }

        if (!hasLabel)
        {
            Line randomLine = new Line(lineOfCode);
            ConstantDeclaration line = new ConstantDeclaration(lineName, randomLine, scope, lineOfCode);
            scope.Constants.Add(line);
            return null!;
        }
        else
        {
            Line randomLine = new Line(label, lineOfCode);
            ConstantDeclaration line = new ConstantDeclaration(lineName, randomLine, scope, lineOfCode);
            scope.Constants.Add(line);
            return null!;
        }
    }

    Node BuildCircle(Scope scope)
    {
        int lineOfCode = currentLine;
        bool hasLabel = false;
        string circleName = string.Empty;
        // bool isRandom = false;
        string label = string.Empty;
        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> circles = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                circles.Add(new Circle(idLine));
            }
            Sequence idSequence = new FiniteSequence(circles, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            scope.Constants.Add(a);
            MoveNext();
            return null!;
        }

        if (nextToken.Type is TokenType.Identifier)
        {
            // isRandom = true;
            MoveNext(2);
            circleName = previousToken.GetName();
            if (currentToken.Type is TokenType.String)
            {
                hasLabel = true;
                label = currentToken.GetName();
                MoveNext();
            }
        }

        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            MoveNext(2);
            Node radius = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node center = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            HandlingFunction = false;
            return new CircleFunction(center, radius, lineOfCode);
        }

        if (!hasLabel)
        {
            Circle randomCircle = new Circle(lineOfCode);
            ConstantDeclaration circle = new ConstantDeclaration(circleName, randomCircle, scope, lineOfCode);
            scope.Constants.Add(circle);
            return null!;
        }
        else
        {
            Circle randomCircle = new Circle(label, lineOfCode);
            ConstantDeclaration circle = new ConstantDeclaration(circleName, randomCircle, scope, lineOfCode);
            scope.Constants.Add(circle);
            return null!;
        }
    }

    Node BuildSegment(Scope scope)
    {
        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> segments = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                segments.Add(new Segment(idLine));
            }
            Sequence idSequence = new FiniteSequence(segments, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            scope.Constants.Add(a);
            MoveNext();
            return null!;
        }
        int lineOfCode = currentLine;
        bool hasLabel = false;
        string label = string.Empty;
        string segmentName = string.Empty;
        // bool isRandom = false;

        if (nextToken.Type is TokenType.Identifier)
        {
            // isRandom = true;
            MoveNext(2);
            segmentName = previousToken.GetName();
            if (currentToken.Type is TokenType.String)
            {
                label = currentToken.GetName();
                hasLabel = true;
                MoveNext();
            }
        }

        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            MoveNext(2);
            Node p1 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node p2 = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            HandlingFunction = false;
            return new SegmentFunction(p1, p2, lineOfCode);
        }

        if (!hasLabel)
        {
            Segment randomSegment = new Segment(lineOfCode);
            ConstantDeclaration segment = new ConstantDeclaration(segmentName, randomSegment, scope, lineOfCode);
            scope.Constants.Add(segment);
            return null!;
        }
        else
        {
            System.Console.WriteLine(label);
            Segment randomSegment = new Segment(label, lineOfCode);
            ConstantDeclaration segment = new ConstantDeclaration(segmentName, randomSegment, scope, lineOfCode);
            scope.Constants.Add(segment);
            return null!;
        }
    }

    Node BuildRay(Scope scope)
    {
        int lineOfCode = currentLine;
        bool hasLabel = false;
        string label = string.Empty;
        string rayName = string.Empty;
        // bool isRandom = false;

        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> rays = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                rays.Add(new Ray(idLine));
            }
            Sequence idSequence = new FiniteSequence(rays, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            scope.Constants.Add(a);
            MoveNext();
            return null!;
        }

        if (nextToken.Type is TokenType.Identifier)
        {
            // isRandom = true;
            MoveNext(2);
            rayName = previousToken.GetName();
            if (currentToken.Type is TokenType.String)
            {
                label = currentToken.GetName();
                hasLabel = true;
                MoveNext();
            }
        }

        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            MoveNext(2);
            Node p1 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node p2 = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            HandlingFunction = false;
            return new RayFunction(p1, p2, lineOfCode);
        }

        if (!hasLabel)
        {
            Ray randomRay = new Ray(lineOfCode);
            ConstantDeclaration ray = new ConstantDeclaration(rayName, randomRay, scope, lineOfCode);
            scope.Constants.Add(ray);
            return null!;
        }
        else
        {
            System.Console.WriteLine(label);
            Ray randomRay = new Ray(label, lineOfCode);
            ConstantDeclaration ray = new ConstantDeclaration(rayName, randomRay, scope, lineOfCode);
            scope.Constants.Add(ray);
            return null!;
        }
    }

    Node BuildArc(Scope scope)
    {

        if (nextToken.Type is TokenType.Sequence)
        {
            MoveNext(2);
            string idName = currentToken.GetName();
            int idLine = currentToken.LineOfCode;
            Random rnd = new Random();
            int random = rnd.Next(1, 50);
            List<Node> arcs = new List<Node>();
            for (int i = 0; i < random; i++)
            {
                arcs.Add(new Arc(idLine));
            }
            Sequence idSequence = new FiniteSequence(arcs, idLine);
            ConstantDeclaration a = new ConstantDeclaration(idName, idSequence, scope, idLine);
            scope.Constants.Add(a);
            MoveNext();
            return null!;
        }
        int lineOfCode = currentLine;
        bool hasLabel = false;
        string label = string.Empty;
        string arcName = string.Empty;
        // bool isRandom = false;

        if (nextToken.Type is TokenType.Identifier)
        {
            // isRandom = true;
            MoveNext(2);
            arcName = previousToken.GetName();
            if (currentToken.Type is TokenType.String)
            {
                label = currentToken.GetName();
                hasLabel = true;
                MoveNext();
            }
        }

        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            MoveNext(2);
            Node p1 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node p2 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node p3 = BuildLevel1(scope);
            Expect(TokenType.Comma);
            Node radius = BuildLevel1(scope);
            Expect(TokenType.RightParenthesis);
            HandlingFunction = false;
            return new ArcFunction(p1, p2, p3, radius, lineOfCode);
        }

        if (!hasLabel)
        {
            Arc randomArc = new Arc(lineOfCode);
            ConstantDeclaration arc = new ConstantDeclaration(arcName, randomArc, scope, lineOfCode);
            scope.Constants.Add(arc);
            return null!;
        }
        else
        {
            System.Console.WriteLine(label);
            Arc randomArc = new Arc(label, lineOfCode);
            ConstantDeclaration Arc = new ConstantDeclaration(arcName, randomArc, scope, lineOfCode);
            scope.Constants.Add(Arc);
            return null!;
        }
    }
}