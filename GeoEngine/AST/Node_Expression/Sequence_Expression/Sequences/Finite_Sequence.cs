namespace GeoEngine;

public class FiniteSequence : Sequence
{

    public FiniteSequence(List<Node> elements, int lineOfCode) : base(elements, lineOfCode)
    {
        Value = this;
        Type = NodeType.FiniteSequence;
        if (Elements.Count is 0)
        {
            Type = NodeType.EmptySequence;
            Value = this;
        }
    }

    public override bool CheckSemantic()
    {
        if (Elements.Where(x => x is not Expression).Count() > 0)
        {
            new Error
            (
                ErrorKind.Semantic,
                ErrorCode.invalid,
                "sequence Expression, elements must be Expressions",
                LineOfCode
            );
            return false;
        }

        return true;
    }

    public override void Evaluate()
    {
        if (Type is NodeType.EmptySequence)
            System.Console.WriteLine("Empty Sequence.");

        for (int i = 0; i < Elements.Count; i++)
        {
            if (Elements[i].Value is null)
            {
                Elements[i].Evaluate();
            }
        }
    }
}