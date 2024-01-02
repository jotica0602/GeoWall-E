namespace GeoEngine;

public class FiniteGeneratedSequence : Sequence
{
    Node LowerBound { get; set; }
    Node UpperBound { get; set; }
    public FiniteGeneratedSequence(List<Node> elements, Node lowerBound, Node upperBound, int lineOfCode) : base(elements, lineOfCode)
    {
        Type = NodeType.Number;
        LowerBound = lowerBound;
        UpperBound = upperBound;
        Value = this;
    }

    public override bool CheckSemantic()
    {
        bool flag = true;
        LowerBound.Evaluate();
        UpperBound.Evaluate();

        double lowerBoundValue = Double.Parse(LowerBound.Value.ToString()!);
        double upperBoundValue = Double.Parse(UpperBound.Value.ToString()!);

        if (!IsInt(lowerBoundValue) && !IsInt(upperBoundValue))
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "sequence lower and upper bounds must be integers",
                LineOfCode
            );

            flag = false;
        }

        if (lowerBoundValue >= upperBoundValue)
        {
            flag = false;
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "sequence range lower bound must be lower than the upper bound",
                LineOfCode
            );
        }

        return flag;
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
        // System.Console.WriteLine($"Count: {Elements.Count}");
        if (Value is null)
        {
            double startValue = double.Parse(LowerBound.Value.ToString()!);
            double endValue = double.Parse(UpperBound.Value.ToString()!);

            for (var i = startValue + 1; i < endValue; i++)
                Elements.Insert(Elements.Count - 1, new Literal(i, LineOfCode));

            foreach (var element in Elements)
            {
                element.Evaluate();
                // Console.Write(element.Value);
            }
            Value = Elements;
        }

    }
}