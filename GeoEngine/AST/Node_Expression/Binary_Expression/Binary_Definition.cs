namespace GeoEngine;
public abstract class BinaryExpression : Expression
{
    public Node LeftNode;
    public Node RightNode;

    public BinaryExpression(Node leftNode, Node rightNode, int lineOfCode) : base(lineOfCode)
    {
        LeftNode = leftNode;
        RightNode = rightNode;
    }
}