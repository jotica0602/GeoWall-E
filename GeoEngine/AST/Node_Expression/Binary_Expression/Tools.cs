using System.Security.Cryptography;

namespace GeoEngine;

public class Tools
{
    public static bool BooleanChecker(object nodeValue)
    {
        // System.Console.WriteLine(nodeValue.GetType());
        if
        (
            (nodeValue is Undefined) ||
            (nodeValue is Sequence && ((Sequence)nodeValue).Type is NodeType.EmptySequence) ||
            (nodeValue is double && (double)nodeValue is 0)
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

    public static bool IsSequence(Node node) => node.Type is NodeType.FiniteSequence || node.Type is NodeType.InfiniteSequence || node.Type is NodeType.EmptySequence;

    public static Sequence SequenceConcatenation(Sequence LeftNode, Node RightNode, int lineOfCode)
    {
        if (LeftNode.Type is NodeType.FiniteSequence && RightNode.Type is NodeType.FiniteSequence)
        {
            FiniteSequence result = new FiniteSequence(LeftNode.Elements.Concat(((Sequence)RightNode).Elements).ToList(), lineOfCode);
            return result;
        }
        else if (LeftNode.Type is NodeType.FiniteSequence && RightNode.Value is Undefined)
        {
            LeftNode.Elements.Add(new Undefined(lineOfCode));
            FiniteSequence result = new FiniteSequence(LeftNode.Elements, lineOfCode);
            return result;
        }
        else if (LeftNode.Type is NodeType.FiniteSequence && RightNode.Type is NodeType.InfiniteSequence)
        {
            InfiniteSequence result = new InfiniteSequence(LeftNode.Elements.Concat(((Sequence)RightNode).Elements).ToList(), ((InfiniteSequence)RightNode).LowerBound, lineOfCode);
            System.Console.WriteLine(result.LowerBound);
            return result;
        }
        else
        {
            return LeftNode;
        }
    }

}