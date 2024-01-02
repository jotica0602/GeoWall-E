using System.Security.Cryptography;

namespace GeoEngine;

public class Tools
{
    public static bool BooleanChecker(object nodeValue)
    {
        // System.Console.WriteLine(nodeValue.GetType());
        if
        (
            (nodeValue == null) ||
            (nodeValue is Sequence && ((Sequence)nodeValue).Type is NodeType.EmptySequence) ||
            (nodeValue is double && (double)nodeValue is 0) ||
            (nodeValue is string && (string)nodeValue == string.Empty)
        )

        { return false; }
        else { return true; }
    }
   
    public static void RunTimeNumberTypeChecker(Node LeftNode, string Operator, Node RightNode, int lineOfCode)
    {
        if (LeftNode.Type is not NodeType.Number || RightNode.Type is not NodeType.Number)
        {
            new Error
            (
                ErrorKind.RunTime,
                ErrorCode.invalid,
                $"operation, operator \"{Operator}\" cannot be applied between \"{LeftNode.Type}\" and \"{RightNode.Type}\"",
                lineOfCode
            );
        }
    }

    public static bool IsAnOperableSequence(Node node) => node.Type is NodeType.FiniteSequence || node.Type is NodeType.InfiniteSequence || node.Type is NodeType.EmptySequence;

    public static Sequence SequenceConcatenation(Sequence LeftNode, Sequence RightNode, int lineOfCode)
    {
        if (LeftNode.Type is NodeType.FiniteSequence && RightNode.Type is NodeType.FiniteSequence)
        {
            FiniteSequence result = new FiniteSequence(LeftNode.Elements.Concat(RightNode.Elements).ToList(), lineOfCode);
            return result;
        }
        else if (LeftNode.Type is NodeType.FiniteSequence && RightNode.Type is NodeType.InfiniteSequence)
        {
            InfiniteSequence result = new InfiniteSequence(LeftNode.Elements.Concat(RightNode.Elements).ToList(), ((InfiniteSequence)RightNode).LowerBound, lineOfCode);
            System.Console.WriteLine(result.LowerBound);
            return result;
        }
        else
        {
            return LeftNode;
        }
    }

}