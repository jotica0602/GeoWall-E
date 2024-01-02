namespace GeoEngine;

public class Addition : ArithmeticExpression
{
    public Addition(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
        Operator = "+";
    }

    public override bool CheckSemantic()
    {
        bool leftSemantic = LeftNode.CheckSemantic();
        bool rightSemantic = RightNode.CheckSemantic();

        return leftSemantic && rightSemantic;
    }
    // {
    //     if
    //     (
    //         LeftNode.Type is NodeType.
    //         LeftNode.Type is not NodeType.Temporal &&
    //         LeftNode.Type is not NodeType.Number &&
    //         LeftNode.Type is not NodeType.FiniteSequence &&
    //         LeftNode.Type is not NodeType.InfiniteSequence &&
    //         LeftNode.Type is not NodeType.Undefined &&
    //         RightNode.Type is not NodeType.Temporal &&
    //         RightNode.Type is not NodeType.Number &&
    //         RightNode.Type is not NodeType.FiniteSequence &&
    //         RightNode.Type is not NodeType.InfiniteSequence &&
    //         RightNode.Type is not NodeType.Undefined
    //     )
    //     {
    //         new Error
    //         (
    //             ErrorKind.Semantic,
    //             ErrorCode.invalid,
    //             $"operation, operator \"{Operator}\" cannot be applied between \"{LeftNode.Type}\" and \"{RightNode.Type}\"",
    //             LineOfCode
    //         );
    //         return false;
    //     }

    //     return true;
    // }

    public override void Evaluate()
    {

        LeftNode.Evaluate();
        RightNode.Evaluate();

        if
        (
            (LeftNode.Type is NodeType.FiniteSequence || LeftNode.Type is NodeType.InfiniteSequence || LeftNode.Type is NodeType.InfiniteSequence) &&
            (RightNode is FiniteSequence || RightNode is FiniteGeneratedSequence)
        )
            Value = Tools.FiniteSequenceHandler((Sequence)LeftNode, (Sequence)RightNode, Value, LineOfCode);

        else Value = (double)LeftNode.Value + (double)RightNode.Value;
    }
}