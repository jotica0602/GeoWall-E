public abstract class Node
{
    public NodeType Type { get; set; }
    public object Value { get; set; }
    public abstract void Evaluate();
    // public abstract bool CheckSemantic(int line);
}