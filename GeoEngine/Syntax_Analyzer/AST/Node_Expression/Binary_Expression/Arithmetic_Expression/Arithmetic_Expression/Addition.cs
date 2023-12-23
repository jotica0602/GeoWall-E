using GeoEngine;

public class Addition : BinaryExpression
{
    public Addition(Node leftNode, Node rightNode) : base(leftNode, rightNode)
    {
        Type = NodeType.Number;
    }

    // public override bool CheckSemantic(int line)
    // {
    //     if
    //     (
    //        LeftNode.Type is not NodeType.Number && LeftNode.Type is not NodeType.Temporal &&
    //        RightNode.Type is not NodeType.Number && RightNode.Type is not NodeType.Temporal
    //     )
    //     {
    //         for(var position = )
    //         Error error = new Error(ErrorKind.Semantic, ErrorCode.Invalid, $" operation: cannot operate {LeftNode.Type} and {RightNode.Type}.")
    //     }
    // }

    public override void Evaluate()
    {
        RightNode.Evaluate();
        RightNode.Evaluate();
        Value = (double)LeftNode.Value + (double)RightNode.Value;
    }
}