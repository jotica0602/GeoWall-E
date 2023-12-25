namespace GeoEngine;
public abstract class BooleanExpression : BinaryExpression
{
    public BooleanExpression(Node LeftNode, Node RightNode) : base(LeftNode, RightNode) { }
}