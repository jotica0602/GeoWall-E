namespace GeoEngine;
public abstract class Node
{
    public NodeType Type { get; protected set; }
    public object Value { get; set; }
    public int LineOfCode { get; set; }

    public Node(int lineOfCode)
    {
        LineOfCode = lineOfCode;
    }

    public abstract void Evaluate();
    public abstract bool CheckSemantic();

    // public override string ToString() => this.Type is NodeType.Undefined ? "undefined" : Value.ToString()!;
}