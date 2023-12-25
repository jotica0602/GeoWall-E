namespace GeoEngine;
public class Print : Node
{
    public Print(object value)
    {
        Value = value;
    }

    public override void Evaluate()
    {
        Console.WriteLine(Value);
    }
}
