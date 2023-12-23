namespace GeoEngine;
public class Print : Node
{
    public Print(object value)
    {
        Value = value;
    }

    public override bool BooleanValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Evaluate()
    {
        Console.WriteLine(Value);
    }
}
