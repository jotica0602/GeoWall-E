using System.Linq.Expressions;
namespace GeoEngine;
public class Draw : Expression
{
    Node Argument { get; set; }
    string Label { get; set; }

    public Draw(Node argument, string label, int lineOfCode) : base(lineOfCode)
    {
        Argument = argument;
        Label = label;
    }

    public override bool CheckSemantic() => true;

    public override void Evaluate()
    {
        Argument.Evaluate();
        if (Argument.Value is not Sequence)
            ((Figure)Argument.Value).Draw(Label);

        if (Argument.Value is Sequence)
            foreach (var element in ((Sequence)Argument.Value).Elements)
            {
                if (element is Figure)
<<<<<<< HEAD
                    ((Figure)element.Value).Draw();
                else
                {
                    var keepDrawing = new Draw(element,LineOfCode);
=======
                    ((Figure)element.Value).Draw(Label);
                else
                {
                    var keepDrawing = new Draw(element, Label, LineOfCode);
>>>>>>> development
                    keepDrawing.Evaluate();
                }
            }
    }

}