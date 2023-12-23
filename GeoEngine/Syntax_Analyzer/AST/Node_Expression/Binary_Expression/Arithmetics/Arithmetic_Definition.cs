namespace GeoEngine;
public abstract class ArithmeticExpression : BinaryExpression
{
    public override bool BooleanValue
    {
        get { return Value is not 0; }
        set { }
    }
    public ArithmeticExpression(Node LeftNode, Node RightNode) : base(LeftNode, RightNode){ }

}