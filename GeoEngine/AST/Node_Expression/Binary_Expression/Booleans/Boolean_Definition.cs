namespace GeoEngine;
public abstract class BooleanExpression : BinaryExpression
{
    public BooleanExpression(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode) { }
}