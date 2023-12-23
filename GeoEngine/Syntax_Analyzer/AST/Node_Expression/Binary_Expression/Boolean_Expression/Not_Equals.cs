public class NotEquals : BinaryExpression
{
    public NotEquals(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }


    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = LeftNode.Value != RightNode.Value;
    }
}