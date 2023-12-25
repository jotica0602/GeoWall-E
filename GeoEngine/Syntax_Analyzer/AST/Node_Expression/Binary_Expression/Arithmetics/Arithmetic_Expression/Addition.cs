namespace GeoEngine;

public class Addition : ArithmeticExpression
{
    public Addition(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        Type = NodeType.Number;
    }


    public override void Evaluate()
    {
        LeftNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value + (double)RightNode.Value;
    }
}