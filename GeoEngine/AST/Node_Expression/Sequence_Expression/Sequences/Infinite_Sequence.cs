namespace GeoEngine;

public class InfiniteSequence : Sequence
{
    public Node LowerBound { get; set; }

    public InfiniteSequence(List<Node> elements, Node lowerBound, int lineOfCode) : base(elements, lineOfCode)
    {
        Type = NodeType.InfiniteSequence;
        LowerBound = lowerBound;
        Value = this;
    }

    public override bool CheckSemantic()
    {
        LowerBound.Evaluate();
        double startValue = double.Parse(LowerBound.Value.ToString()!);
        if (!IsInt(startValue))
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "sequence element, it must be an integer",
                LowerBound.LineOfCode
            );
            return false;
        }

        return true;
    }

    bool IsInt(object element)
    {
        // Verify is object is a number
        if (element is int || element is double || element is float || element is decimal)
        {
            // Verifying if object is int
            double valor = Convert.ToDouble(element);
            return valor % 1 == 0;
        }

        return false;
    }

    public override void Evaluate()
    {
        foreach (var element in Elements)
            element.Evaluate();
    }

    public override string ToString()
    {
        string value = "";

        foreach (var element in Elements)
            value += element.Value + " ";

        return "{ " + value + "..."+" }";
    }

}