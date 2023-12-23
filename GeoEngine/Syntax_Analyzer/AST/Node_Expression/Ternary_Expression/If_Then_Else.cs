namespace GeoEngine;

public class IfThenElse : Node
{
    public Node Condition;
    public Node TrueNode;
    public Node FalseNode;
    
    public IfThenElse(Node condition, Node trueNode, Node falseNode)
    {
        Condition = condition;
        TrueNode = trueNode;
        FalseNode = falseNode;
    }

    public override bool BooleanValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Evaluate()
    {
        Condition.Evaluate();
        if (Condition.Value is 1)
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