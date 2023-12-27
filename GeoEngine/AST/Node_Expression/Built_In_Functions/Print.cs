namespace GeoEngine;
public class Print : Expression
{
    public Print(object value, int lineOfCode): base (lineOfCode)
    {
        Value = value;
    }

    public override void Evaluate()
    {
        Console.WriteLine(Value);
    }
}
