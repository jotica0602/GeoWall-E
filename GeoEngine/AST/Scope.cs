public class Scope
{
    public Scope Parent { get; set; }
    public Dictionary<string, ConstantDeclaration> Constants { get; set; }
    public Scope()
    {
        Constants = new Dictionary<string, ConstantDeclaration>();
    }

    public Scope MakeChild()
    {
        Scope child = new Scope();
        child.Parent = this;
        return child;
    }
}