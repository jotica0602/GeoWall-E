using System.Numerics;
using GeoEngine;

public class Scope
{
    public Scope Parent { get; set; }
    public List<ConstantDeclaration> Constants { get; set; }
    public List<FunctionDeclaration> Functions { get; set; }
    public Scope()
    {
        Constants = new List<ConstantDeclaration>();
        Functions = new List<FunctionDeclaration>();
    }

    public Scope MakeChild()
    {
        Scope child = new Scope();
        child.Parent = this;
        return child;
    }
}