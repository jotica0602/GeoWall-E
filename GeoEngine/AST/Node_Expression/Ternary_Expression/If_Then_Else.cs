using System.Formats.Asn1;
using System.Runtime.InteropServices;

namespace GeoEngine;

public class IfThenElse : Expression
{
    public Node Condition;
    public Node TrueNode;
    public Node FalseNode;

    public IfThenElse(Node condition, Node trueNode, Node falseNode, int lineOfCode) : base(lineOfCode)
    {
        Condition = condition;
        TrueNode = trueNode;
        FalseNode = falseNode;
    }

    public override bool CheckSemantic() 
    => Condition.CheckSemantic() && TrueNode.CheckSemantic() && FalseNode.CheckSemantic();

    public override void Evaluate()
    {
        Condition.Evaluate();
        if (BooleanValue.Checker(Condition.Value))
        {
            TrueNode.Evaluate();
            Value = TrueNode.Value;
        }
        else
        {
            FalseNode.Evaluate();
            Value = FalseNode.Value;
        }
    }
}