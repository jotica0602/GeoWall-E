namespace GeoEngine;
public abstract class BooleanExpression : BinaryExpression
{
    public override bool BooleanValue
    {
        get { return Value is not 0; }
        set { }
    }
    public BooleanExpression(Node LeftNode, Node RightNode) : base(LeftNode, RightNode) { }
}