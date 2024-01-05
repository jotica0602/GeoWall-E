using System.Linq.Expressions;
namespace GeoEngine;
public class Draw : Expression
{
    Node Argument { get; set; }

    public Draw(Node argument, int lineOfCode) : base(lineOfCode)
    {
        Argument = argument;
    }

    public override bool CheckSemantic() => true;
    // {
    //     if (Argument is not FigureFunction && Argument is not Figure && Argument.Type is not NodeType.Temporal && Argument is not Sequence)
    //     {
    //         new Error(ErrorKind.Semantic, ErrorCode.invalid, $"argument type, it must be a figure", LineOfCode);
    //         return false;
    //     }

    //     return true;
    // }


    public override void Evaluate()
    {
        Argument.Evaluate();
        if (Argument.Value is not Sequence)
            ((Figure)Argument.Value).Draw();

        if (Argument.Value is Sequence)
            foreach (var element in ((Sequence)Argument.Value).Elements)
            {
                if (element is Figure)
                    ((Figure)element.Value).Draw();
                else
                {
                    var keepDrawing = new Draw(element,LineOfCode);
                    keepDrawing.Evaluate();
                }
            }

    }

}