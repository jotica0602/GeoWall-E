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

    public override void Evaluate()
    {
        foreach(var node in Instructions)
            node.Evaluate();
            
        InNode.Evaluate();
        Value = InNode.Value;
    }
}