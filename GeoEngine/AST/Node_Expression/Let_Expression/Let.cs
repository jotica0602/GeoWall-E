namespace GeoEngine;

public class Let : Expression
{
    public List<Node> Instructions { get; set; }
    public Node InNode { get; set; }
    public Let(int lineOfCode) : base(lineOfCode)
    {
        Instructions = new List<Node>();
        Type = NodeType.Temporal;
    }

    public override bool CheckSemantic()
    {
        bool isOk = false;
        foreach (var node in Instructions)
            isOk = isOk && node.CheckSemantic();

        isOk = isOk && InNode.CheckSemantic();
        return isOk;
    }

    public override void Evaluate()
    {
        foreach (var node in Instructions)
            node.Evaluate();

        InNode.Evaluate();
        Value = InNode.Value;
        Type = InNode.Type;
    }
}