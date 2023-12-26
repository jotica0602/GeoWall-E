public abstract class Token
{
    public TokenType Type { get; set; }

    public int LineOfCode { get; set; }

    public Token(TokenType type, int currentLine)
    {
        Type = type;
        LineOfCode = currentLine;
    }

    public TokenType Gettype() => Type;

    public void Settype(TokenType type) => Type = type;

    public abstract string GetName();

    public abstract void SetName(string name);

    public abstract object GetValue();

    public abstract void SetValue(object value);


    public override string ToString() => $"{Type}";
}

public class Keyword : Token
{
    public Keyword(TokenType type,int currentLine) : base(type,currentLine)
    {
        Type = type;
    }

    public override string GetName() => Type.ToString();

    public override object GetValue() => throw new NotImplementedException();

    public override void SetName(string name) => throw new NotImplementedException();

    public override void SetValue(object value) => throw new NotImplementedException();

}

public class CommonToken : Token
{
    public string Representation { get; set; }

    public CommonToken(TokenType type, string representation, int currentLine) : base(type,currentLine)
    {
        Representation = representation;
    }
    public override string GetName() => Representation;

    public override void SetName(string name) => Representation = name;

    public override object GetValue() => throw new NotImplementedException();

    public override void SetValue(object value) => throw new NotImplementedException();

    public override string ToString() => $"{base.Type}: {Representation}";
}

public class Data : Token
{
    public object Value { get; set; }

    public Data(TokenType type, object value, int currentLine) : base(type,currentLine)
    {
        Value = value;
    }

    public override string GetName() => Value.ToString()!;

    public override void SetName(string name) => throw new NotImplementedException();

    public override object GetValue() => Value;

    public override void SetValue(object value) => Value = value;

    public override string ToString() => $"{base.Type}: {Value}";
}
