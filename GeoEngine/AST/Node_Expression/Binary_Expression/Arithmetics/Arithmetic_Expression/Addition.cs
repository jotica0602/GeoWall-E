namespace GeoEngine;

public class Addition : ArithmeticExpression
{
    public Addition(Node leftNode, Node rightNode, int lineOfCode) : base(leftNode, rightNode, lineOfCode)
    {
        Type = NodeType.Number;
        Operator = "+";
    }

    public override bool CheckSemantic()
    {
        bool leftSemantic = LeftNode.CheckSemantic();
        bool rightSemantic = RightNode.CheckSemantic();

        return leftSemantic && rightSemantic;
    }

    public override void Evaluate()
    {

        LeftNode.Evaluate();
        RightNode.Evaluate();



        if (Tools.IsSequence(LeftNode) && (Tools.IsSequence(RightNode) || RightNode is Undefined))
        {
            LeftNode = (Sequence)LeftNode.Value;
            RightNode = (Sequence)RightNode.Value;
            // System.Console.WriteLine("both are sequences");
            Value = Tools.SequenceConcatenation((Sequence)LeftNode, (Sequence)RightNode, LineOfCode);
            // System.Console.WriteLine(((Node)Value).Type);
            Type = ((Node)Value).Type;
        }

        else if (LeftNode.Value is Undefined || RightNode is Undefined)
        {
            Value = new Undefined(LineOfCode);
            return;
        }

        else
        {

            double leftNodeValue;
            double rightNodeValue;
            if (Double.TryParse(LeftNode.Value.ToString(), out leftNodeValue) && Double.TryParse(RightNode.Value.ToString(), out rightNodeValue))
            {
                Value = leftNodeValue + rightNodeValue;
            }
            else
            {
                new Error
                (
                    ErrorKind.RunTime,
                    ErrorCode.invalid,
                    $"operation, operator \"{Operator}\" cannot be applied between \"{LeftNode.Type}\" and \"{RightNode.Type}\"",
                    LineOfCode
                );
                Error.CheckErrors();
                throw new Exception();
            }
        }
    }

}