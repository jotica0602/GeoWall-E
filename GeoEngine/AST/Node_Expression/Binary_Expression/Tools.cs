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

    public static void NumberTypeChecker(Node LeftNode, string Operator, Node RightNode, int lineOfCode)
    {
        if (LeftNode.Type is not NodeType.Number || LeftNode.Type is not NodeType.Number)
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

    public static FiniteSequence FiniteSequenceHandler(Sequence LeftNode, Sequence RightNode, object value, int lineOfCode)
    {
        FiniteSequence result = new FiniteSequence(LeftNode.Elements.Concat(RightNode.Elements).ToList(), lineOfCode);
        return result;
    }

}