namespace GeoEngine;
public abstract class Sequence : Expression
{
    public List<Node> Elements { get; set; }

    public Sequence(List<Node> elements, int lineOfCode) : base(lineOfCode)
    {
        Elements = elements;
        if (elements.Count is 0)
        {
            Type = NodeType.EmptySequence;
            Value = null!;
        }
    }
    public NodeType GetNodeType() => this.Type;
    public override string ToString()
    {
        string value = "";

        foreach (var element in Elements)
            value += element.Value + " ";

        return value;
    }
}