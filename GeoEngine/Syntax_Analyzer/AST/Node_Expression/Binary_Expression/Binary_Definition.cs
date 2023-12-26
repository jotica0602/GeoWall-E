namespace GeoEngine;
public abstract class BinaryExpression : Node
{
    public Node LeftNode;
    public Node RightNode;

    public BinaryExpression(Node LeftNode, Node RightNode, int lineOfCode) : base(lineOfCode)
    {
        this.LeftNode = LeftNode;
        this.RightNode = RightNode;
    }
}