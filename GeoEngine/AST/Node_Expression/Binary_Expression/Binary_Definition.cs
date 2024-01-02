namespace GeoEngine;
public abstract class BinaryExpression : Expression
{
    protected Node LeftNode;
    protected Node RightNode;
    protected string Operator;

    public BinaryExpression(Node leftNode, Node rightNode, int lineOfCode) : base(lineOfCode)
    {
        LeftNode = leftNode;
        RightNode = rightNode;
    }

    public override bool CheckSemantic()
    {
        if
        (
            LeftNode.Type is not NodeType.Number &&
            LeftNode.Type is not NodeType.Temporal ||
            RightNode.Type is not NodeType.Number &&
            RightNode.Type is not NodeType.Temporal
        )
        {
            new Error
            (
                ErrorKind.Semantic, ErrorCode.invalid,
                $"operation, operator \"{Operator}\" cannot be applied between \"{LeftNode.Type}\" and \"{RightNode.Type}\"",
                LineOfCode
            );
            return false;
        }
        return true;
    }
}