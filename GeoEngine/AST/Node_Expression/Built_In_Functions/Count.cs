using System.Linq.Expressions;
namespace GeoEngine;

public class Count : Expression
{
    Node Argument { get; set; }

    public Count(Node argument, int lineOfCode) : base(lineOfCode)
    {
        Argument = argument;
    }

    public override bool CheckSemantic()
    {
        Argument.CheckSemantic();

        if (Argument is not Sequence)
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "function call, parameter must be a sequence",
                LineOfCode
            );

            return false;
        }

        return true;
    }

    public override void Evaluate()
    {
        Argument.Evaluate();
        // if (Argument.Value is not Sequence)
        // {
        //     new Error
        //     (
        //         ErrorKind.RunTime,
        //         ErrorCode.invalid,
        //         "function parameter must be a sequence",
        //         LineOfCode
        //     );
        //     Error.CheckErrors();
        // }

        if (Argument is InfiniteSequence)
        {
            Value = null!; /* new Literal(NodeType.Undefined, LineOfCode)! */ 
            Type = NodeType.Undefined;
        }

        else if (Argument.Type is NodeType.FiniteSequence)
        {
            Value = ((Sequence)Argument).Elements.Count;
            Type = NodeType.Number;
        }
    }
}