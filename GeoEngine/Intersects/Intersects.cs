namespace GeoEngine;

public static class Intersections
{
    public static object Intersection(Figure figure1, Figure figure2, int lineOfCode)
    {
        switch (figure1)
        {
            case Point point1:
                switch (figure2)
                {
                    case Point point2:
                        return PointsIntersect(point1, point2);
                    case Line line2:
                        return LinePointIntersect(point1, line2);
                    case Segment segment2:
                        return SegmentPointIntersect(point1, segment2);
                    case Ray ray2:
                        return RayPointIntersect(point1, ray2);
                    case Arc arc2:
                        return ArcPointIntersect(point1, arc2);
                    case Circle circle2:
                        return CirclePointIntersect(point1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
            case Line line1:
                switch (figure2)
                {
                    case Point point2:
                        return LinePointIntersect(point2, line1);
                    case Line line2:
                        return LinesIntersect(line1, line2);
                    case Segment segment2:
                        return LineSegmentIntersect(line1, segment2);
                    case Ray ray2:
                        return LineRayIntersect(line1, ray2);
                    case Arc arc2:
                        return LineArcIntersect(line1, arc2);
                    case Circle circle2:
                        return LineCircleIntersect(line1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
            case Segment segment1:
                switch (figure2)
                {
                    case Point point2:
                        return SegmentPointIntersect(point2, segment1);
                    case Line line2:
                        return LineSegmentIntersect(line2, segment1);
                    case Segment segment2:
                        return SegmentsIntersect(segment1, segment2);
                    case Ray ray2:
                        return SegmentRayIntersect(ray2, segment1);
                    case Arc arc2:
                        return SegmentArcIntersect(segment1, arc2);
                    case Circle circle2:
                        return SegmentCircleIntersect(segment1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
            case Ray ray1:
                switch (figure2)
                {
                    case Point point2:
                        return RayPointIntersect(point2, ray1);
                    case Line line2:
                        return LineRayIntersect(line2, ray1);
                    case Segment segment2:
                        return SegmentRayIntersect(ray1, segment2);
                    case Ray ray2:
                        return RaysIntersect(ray1, ray2);
                    case Arc arc2:
                        return RayArcIntersect(ray1, arc2);
                    case Circle circle2:
                        return RayCircleIntersect(ray1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
            case Circle circle1:
                switch (figure2)
                {
                    case Point point2:
                        return CirclePointIntersect(point2, circle1);
                    case Line line2:
                        return LineCircleIntersect(line2, circle1);
                    case Segment segment2:
                        return SegmentCircleIntersect(segment2, circle1);
                    case Ray ray2:
                        return RayCircleIntersect(ray2, circle1);
                    case Arc arc2:
                        return ArcCircleIntersect(arc2, circle1);
                    case Circle circle2:
                        return CirclesIntersect(circle1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
            case Arc arc1:
                switch (figure2)
                {
                    case Point point2:
                        return ArcPointIntersect(point2, arc1);
                    case Line line2:
                        return LineArcIntersect(line2, arc1);
                    case Segment segment2:
                        return SegmentArcIntersect(segment2, arc1);
                    case Ray ray2:
                        return RayArcIntersect(ray2, arc1);
                    case Arc arc2:
                        return ArcsIntersect(arc1, arc2);
                    case Circle circle2:
                        return ArcCircleIntersect(arc1, circle2);
                    default:
                        return new Literal(NodeType.Undefined, lineOfCode);
                }
        }
        return new Literal(NodeType.Undefined, lineOfCode);


        #region Point Intersections

        object PointsIntersect(Point point1, Point point2)
        {
            List<Node> intersection = new List<Node>();
            if (point1.Equals(point2))
            {
                intersection.Add(point1);
                return new FiniteSequence(intersection, lineOfCode);
            }
            else return new FiniteSequence(new List<Node>(), lineOfCode);
        }

        object LinePointIntersect(Point point1, Line line2)
        {
            List<Node> intersection = new List<Node>();
            if (IsInLine(point1, line2))
            {
                intersection.Add(point1);
                return new FiniteSequence(intersection, lineOfCode);
            }
            return new FiniteSequence(new List<Node>(), lineOfCode);

        }

        object SegmentPointIntersect(Point point1, Segment segment2)
        {
            List<Node> intersection = new List<Node>();
            if (IsInSegment(point1, segment2))
            {
                intersection.Add(point1);
                return new FiniteSequence(intersection, lineOfCode);
            }
            return new FiniteSequence(intersection, lineOfCode);
        }



        object ArcPointIntersect(Point point1, Arc arc2)
        {
            Circle circle2 = new Circle(arc2.Center, arc2.Radius, lineOfCode);
            List<Node> intersection = (List<Node>)CirclePointIntersect(point1, circle2);
            if (intersection.Any())
            {
                if (IsInArc(point1, arc2))
                {
                    return new FiniteSequence(intersection, lineOfCode);
                }
            }
            return new FiniteSequence(new List<Node>(), lineOfCode);
        }

        object CirclePointIntersect(Point point1, Circle circle2)
        {
            List<Node> intersection = new List<Node>();
            if (Math.Pow(circle2.Center.X - point1.X, 2) + Math.Pow(circle2.Center.Y - point1.Y, 2) == Math.Pow(circle2.Radius, 2))
            {
                intersection.Add(point1);
                return new FiniteSequence(intersection, lineOfCode);
            }
            return new FiniteSequence(new List<Node>(), lineOfCode);
        }

        #endregion

        #region Line Intersections

        object LinesIntersect(Line line1, Line line2)
        {
            var (m1, n1) = LineEquation(line1);
            var (m2, n2) = LineEquation(line2);
            if (m1 == m2 && n1 == n2)
            {
                return new Literal(NodeType.Undefined, lineOfCode);
            }
            if (m1 == m2 & n1 != n2)
            {
                return new FiniteSequence(new List<Node>(), lineOfCode);
            }
            double x1 = line1.P1.X;
            double y1 = line1.P1.Y;
            double x2 = line1.P2.X;
            double y2 = line1.P2.Y;

            double x3 = line2.P1.X;
            double y3 = line2.P1.Y;
            double x4 = line2.P2.X;
            double y4 = line2.P2.Y;
            double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4); //revisar aqui

            double x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
            double y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

            List<Node> intersection = new List<Node>();
            intersection.Add(new Point(x, y, lineOfCode));
            return new FiniteSequence(intersection, lineOfCode);
        }

        object LineSegmentIntersect(Line line1, Segment segment2)
        {
            var intersection = LinesIntersect(line1, new Line(segment2.P1, segment2.P2, lineOfCode));

            if (intersection is FiniteSequence)
            {
                if (((FiniteSequence)intersection).Type is NodeType.EmptySequence)
                {
                    return intersection;
                }
                else
                {
                    FiniteSequence _intersection = (FiniteSequence)intersection;
                    Point point = (Point)_intersection.Elements[0];
                    if (IsInSegment(point, segment2)) return _intersection;
                    return new FiniteSequence(new List<Node>(), lineOfCode);
                }
            }

            return intersection;
        }



        object LineCircleIntersect(Line line, Circle circle)
        {
            var distance = DistancePointLine(circle.Center, line);

            if (distance > circle.Radius) return new FiniteSequence(new List<Node>(), lineOfCode);
            else
            {

                var (m, c) = LineEquation(line);
                var center = circle.Center;
                var radius = circle.Radius;

                double centerX = center.X;
                double centerY = center.Y;

                // Discriminant equation: b^2 -4*a*c.
                double A = 1 + Math.Pow(m, 2);
                double B = 2 * (m * c - m * centerY - centerX);
                double C = Math.Pow(centerY, 2) - Math.Pow(radius, 2) + Math.Pow(centerX, 2) - 2 * c * centerY + Math.Pow(c, 2);

                double discriminant = Math.Pow(B, 2) - 4 * A * C;

                List<Node> nodes = new List<Node>();
                if (distance == radius)
                {
                    double x = (-B) / (2 * A);
                    double y = m * x + c;
                    nodes.Add(new Point(x, y, lineOfCode));
                    return new FiniteSequence(nodes, lineOfCode);
                }

                double x1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                double x2 = (-B - Math.Sqrt(discriminant)) / (2 * A);

                double y1 = m * x1 + c;
                double y2 = m * x2 + c;

                nodes.Add(new Point(x1, y1, lineOfCode));
                nodes.Add(new Point(x2, y2, lineOfCode));
                return new FiniteSequence(nodes, lineOfCode);
            }
        }


        object LineArcIntersect(Line line1, Arc arc2)
        {
            Circle circle = new Circle(arc2.Center, arc2.Radius, lineOfCode);
            Sequence intersect = (Sequence)LineCircleIntersect(line1, circle);
            if (intersect.Elements.Any())
            {
                List<Node> nodes = new();
                foreach (Point p in intersect.Elements)
                    if (IsInArc(p, arc2)) nodes.Add(p);

                return new FiniteSequence(nodes, lineOfCode);
            }
            return intersect;

        }

        #endregion

        #region Segment Intersections

        object SegmentsIntersect(Segment segment1, Segment segment2)
        {
            Line line = new Line(segment1.P1, segment1.P2, lineOfCode);
            var intersection = LineSegmentIntersect(line, segment2);

            if (intersection is Sequence)
            {
                Sequence _intersection = (Sequence)intersection;
                if (_intersection.Elements.Any())
                {
                    if (IsInSegment((Point)_intersection.Elements[0], segment1)) return _intersection;
                    else return new FiniteSequence(new List<Node>(), lineOfCode);
                }

                return intersection;
            }

            return intersection;
        }



        object SegmentArcIntersect(Segment segment1, Arc arc2)
        {
            Line line = new Line(segment1.P1, segment1.P2, lineOfCode);
            Sequence intersection = (Sequence)LineArcIntersect(line, arc2);
            List<Node> points = new List<Node>();

            if (intersection.Elements.Any())
            {
                foreach (Point p in intersection.Elements)
                    if (IsInSegment(p, segment1))
                        points.Add(p);
            }

            return new FiniteSequence(points, lineOfCode);
        }

        object SegmentCircleIntersect(Segment segment1, Circle circle2)
        {
            Line line = new Line(segment1.P1, segment1.P2, lineOfCode);
            Sequence intersection = (Sequence)LineCircleIntersect(line, circle2);
            List<Node> points = new List<Node>();

            if (intersection.Elements.Any())
            {
                foreach (Point p in intersection.Elements)
                    if (IsInSegment(p, segment1))
                        points.Add(p);
            }

            return new FiniteSequence(points, lineOfCode);
        }

        #endregion

        #region Ray Intersections
        object RaysIntersect(Ray ray1, Ray ray2) => throw new NotImplementedException();
        object RayPointIntersect(Point point1, Ray ray2) => throw new NotImplementedException();
        object LineRayIntersect(Line line1, Ray ray2) => throw new NotImplementedException();
        object SegmentRayIntersect(Ray ray2, Segment segment1) => throw new NotImplementedException();
        object RayArcIntersect(Ray ray1, Arc arc2) => throw new NotImplementedException();
        object RayCircleIntersect(Ray ray1, Circle circle2) => throw new Exception();

        #endregion

        #region  Circle Intersections
        object CirclesIntersect(Circle circle1, Circle circle2)
        {
            List<Node> intersections = new List<Node>();

            double x1 = circle1.Center.X;
            double y1 = circle1.Center.Y;
            double r1 = circle1.Radius;
            double x2 = circle2.Center.X;
            double y2 = circle2.Center.Y;
            double r2 = circle2.Radius;
            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            if (circle1.Equals(circle2))
                return new Undefined(lineOfCode);

            else if (distance > r1 + r2 || distance < Math.Abs(r1 - r2) || distance == 0)
                return new FiniteSequence(new List<Node>(), lineOfCode);

            // Circles do not intersect cause they are too far apart or one is contained in the other
            else if (distance == r1 + r2 || distance == Math.Abs(r1 - r2))
            {
                // Tangent circles that intersect in one point
                double x = (r1 * x2 + r2 * x1) / (r1 + r2);
                double y = (r1 * y2 + r2 * y1) / (r1 + r2);
                intersections.Add(new Point(x, y, lineOfCode));
            }
            else
            {
                // Circles intersect in two points
                double a = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(distance, 2)) / (2 * distance);
                double h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(a, 2));

                double x = x1 + (a * (x2 - x1)) / distance;
                double y = y1 + (a * (y2 - y1)) / distance;

                double intersectX1 = x + (h * (y2 - y1)) / distance;
                double intersectY1 = y - (h * (x2 - x1)) / distance;

                double intersectX2 = x - (h * (y2 - y1)) / distance;
                double intersectY2 = y + (h * (x2 - x1)) / distance;

                intersections.Add(new Point(intersectX1, intersectY1, lineOfCode));
                intersections.Add(new Point(intersectX2, intersectY2, lineOfCode));
            }
            return new FiniteSequence(intersections, lineOfCode);
        }

        object ArcCircleIntersect(Arc arc2, Circle circle1)
        {
            Circle circle = new Circle(arc2.Center, arc2.Radius, lineOfCode);
            var intersect = CirclesIntersect(circle, circle1);
            List<Node> points = new List<Node>();

            if (intersect is Sequence && ((Sequence)intersect).Elements.Any())
            {
                Sequence _intersect = (Sequence)intersect;
                foreach (Point p in _intersect.Elements)
                {
                    if (IsInArc(p, arc2))
                        points.Add(p);
                }

                return new FiniteSequence(points, lineOfCode);
            }

            return intersect;
        }

        #endregion

        #region Arc Intersections
        object ArcsIntersect(Arc arc1, Arc arc2)
        {
            Circle circle1 = new Circle(arc1.Center, arc1.Radius, lineOfCode);
            var intersect = ArcCircleIntersect(arc2, circle1);
            List<Node> points = new List<Node>();

            if (intersect is Sequence && ((Sequence)intersect).Elements.Any())
            {
                Sequence _intersect = (Sequence)intersect;
                foreach (Point p in _intersect.Elements)
                    if (IsInArc(p, arc1))
                        points.Add(p);

                return new FiniteSequence(points, lineOfCode);
            }

            return intersect;
        }
        #endregion
    }
    #region Auxiliar Methods
    static bool IsInLine(Point point, Line line)
    {
        if (line.P1.X == line.P2.X)
        {
            return line.P1.X == point.X;
        }
        else
        {
            var (m, n) = LineEquation(line);
            return line.P1.Y == m * line.P1.X + n;
        }
    }


    static (double, double) LineEquation(Line line)
    {
        double m = (line.P2.Y - line.P1.Y) / (line.P2.X - line.P1.X);
        double n = line.P1.Y - m * line.P1.X;

        return (m, n);
    }


    static bool IsInSegment(Point point, Segment segment)
    {
        double minX = Math.Min(segment.P1.X, segment.P2.X);
        double maxX = Math.Max(segment.P1.X, segment.P2.X);
        double minY = Math.Min(segment.P1.Y, segment.P2.Y);
        double maxY = Math.Max(segment.P1.Y, segment.P2.Y);

        // Check if point is inside the bounding box of the segment
        if (point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY)
        {
            // Parametric equation of the segment
            double t = ((point.X - segment.P1.X) * (segment.P2.X - segment.P1.X) +
                        (point.Y - segment.P1.Y) * (segment.P2.Y - segment.P1.Y)) /
                        (Math.Pow(segment.P2.X - segment.P1.X, 2) + Math.Pow(segment.P2.Y - segment.P1.Y, 2));

            // Check if point is inside the segment
            if (t >= 0 && t <= 1)
            {
                return true;
            }
        }
        return false;
    }

    static bool IsInArc(Point point, Arc arc)
    {
        double pointAngle = Math.Atan2(point.Y - arc.Center.Y, point.Y - arc.Center.X);
        double startAngle = Math.Atan2(arc.StartPoint.Y - arc.Center.Y, arc.StartPoint.X - arc.Center.X);
        double endAngle = Math.Atan2(arc.EndPoint.Y - arc.Center.Y, arc.EndPoint.X - arc.Center.X);

        if (startAngle < endAngle)
        {
            return startAngle <= pointAngle && pointAngle <= endAngle;
        }
        else
        {
            return startAngle <= pointAngle || pointAngle <= endAngle;
        }
    }

    static double DistancePointLine(Point point, Line line)
    {
        double x1 = line.P1.X;
        double y1 = line.P1.Y;
        double x2 = line.P2.X;
        double y2 = line.P2.Y;
        double x0 = point.X;
        double y0 = point.Y;

        return Math.Abs((y2 - y1) * x0 - (x2 - x1) * y0 + x2 * y1 - y2 * x1) / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
    }
    #endregion
}