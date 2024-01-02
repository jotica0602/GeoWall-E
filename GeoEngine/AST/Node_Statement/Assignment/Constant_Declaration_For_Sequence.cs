namespace GeoEngine;
public class ConstantsDeclarationForSequences : ConstantDeclaration
{
    Node _Sequence { get; set; }
    Node Expression { get; set; }
    int Position { get; set; }
    bool IsLast { get; set; }

    public ConstantsDeclarationForSequences(string name, Node expression, Node sequence, int position, bool isLast, Scope scope, int lineOfCode) : base(name, expression, scope, lineOfCode)
    {
        _Sequence = sequence;
        Expression = null!;
        Position = position;
        IsLast = isLast;
        CheckSemantic();
    }

    public override bool CheckSemantic()
    {
        bool isNotRedifining = IsNotRedifining();
        return isNotRedifining;
    }

    bool IsNotRedifining()
    {
        for (var actualScope = Scope; actualScope is not null; actualScope = actualScope.Parent)
            if (actualScope.Constants.Exists(x => x.Name == this.Name))
            {
                new Error
                (
                    ErrorKind.Semantic,
                    ErrorCode.invalid,
                    $"constant \"{Name}\" cannot be redefined.",
                    LineOfCode
                );
                return false;
            }

        return true;
    }

    public override void Evaluate()
    {

        _Sequence.Evaluate();
        _Sequence.CheckSemantic();

        if (_Sequence.Value is Sequence)
            _Sequence = (Sequence)_Sequence.Value;

        // Finite Sequences
        if (_Sequence is FiniteSequence || _Sequence is FiniteGeneratedSequence)
        {
            Sequence seq = (Sequence)_Sequence;

            if (Position < seq.Elements.Count && !IsLast)
                Expression = seq.Elements[Position];

            if (Position < seq.Elements.Count && IsLast)
            {
                List<Node> newElements = new List<Node>();

                for (int i = Position; i < seq.Elements.Count; i++)
                    newElements.Add(seq.Elements[i]);

                Expression = new FiniteSequence(newElements, LineOfCode);
            }

            if (Position >= seq.Elements.Count && IsLast)
                Expression = new FiniteSequence(new List<Node>(), LineOfCode);

            if (Position >= seq.Elements.Count && !IsLast)
                Expression = new Literal(NodeType.Undefined, null!, LineOfCode);

            Expression.Evaluate();
            Value = Expression.Value;
        }

        else
        {
            InfiniteSequence seq = (InfiniteSequence)_Sequence;
            if (Position < seq.Elements.Count && !IsLast)
                Expression = seq.Elements[Position];

            if (Position < seq.Elements.Count && IsLast)
            {
                List<Node> nodes = new List<Node>();
                nodes.Add(seq.LowerBound);
                Expression = new InfiniteSequence(nodes, seq.LowerBound, LineOfCode);
            }

            if (Position >= seq.Elements.Count && !IsLast)
            {
                Literal number = new Literal((double)seq.LowerBound.Value + (Position - seq.Elements.Count) + 1, LineOfCode);
                Expression = number;
            }

            if (Position >= seq.Elements.Count && IsLast)
            {
                Literal newLowerBound = new Literal((double)seq.LowerBound.Value + (Position - seq.Elements.Count) + 1, LineOfCode);
                List<Node> nodes = new List<Node>();
                nodes.Add(newLowerBound);
                Expression = new InfiniteSequence(nodes, newLowerBound, LineOfCode);
                // System.Console.WriteLine((newLowerBound.Value));
            }

            Expression.Evaluate();
            Value = Expression.Value;
        }
    }
}