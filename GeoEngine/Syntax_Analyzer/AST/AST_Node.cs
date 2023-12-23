using System.Xml;

public abstract class Node
{
    public abstract NodeType Type { get; set; }
    public abstract object Value { get; set; }

    public abstract void Evaluate();
}