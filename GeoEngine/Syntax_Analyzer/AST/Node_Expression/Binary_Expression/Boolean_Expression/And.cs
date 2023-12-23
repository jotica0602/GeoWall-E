public class And : BinaryExpression
{
    public And(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        this.LeftNode = leftNode;
        this.RightNode = rightNode;
    }


    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (bool)LeftNode.Value && (bool)RightNode.Value;
    }
}