namespace GeoEngine;
public abstract class ArithmeticExpression : BinaryExpression
{
    public ArithmeticExpression(Node LeftNode, Node RightNode) : base(LeftNode, RightNode){ }
}