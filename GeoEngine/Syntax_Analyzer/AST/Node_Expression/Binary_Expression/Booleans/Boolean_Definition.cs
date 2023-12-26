namespace GeoEngine;
public abstract class BooleanExpression : BinaryExpression
{
    public BooleanExpression(Node LeftNode, Node RightNode, int lineOfCode) : base(LeftNode, RightNode, lineOfCode) { }
}