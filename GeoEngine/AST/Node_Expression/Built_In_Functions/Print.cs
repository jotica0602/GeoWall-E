namespace GeoEngine;
public class Print : Expression
{   
    public static List<string> Logs { get; set; } = new List<string>();
    Node Argument { get; set; }
    public Print(Node argument, int lineOfCode) : base(lineOfCode)
    {
        Argument = argument;
    }

    public override bool CheckSemantic()
    {
        if (Argument is not Expression)
        {
            new Error(ErrorKind.Semantic, ErrorCode.invalid, "argument type, it must be an expression", LineOfCode);
            return false;
        }

        return true;
    }

    public override void Evaluate()
    {
        Argument.Evaluate();
        Value = Argument.Value;
        Logs.Add(Value.ToString()!);
        // System.Console.WriteLine(Value);
    }

    public static string GetAllLogs()
    {
        string allLogs = "";

        foreach (string log in Logs)
        {
            allLogs += $"{log}.<br />";
        }

        return allLogs;
    }
}