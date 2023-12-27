namespace GeoEngine;
public abstract class ArithmeticExpression : BinaryExpression
{
    protected ArithmeticExpression(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode) { }
}