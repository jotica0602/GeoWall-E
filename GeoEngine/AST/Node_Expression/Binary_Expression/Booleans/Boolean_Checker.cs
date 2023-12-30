namespace GeoEngine;

public class BooleanValue
{
    public static bool Checker(object nodeValue)
    {
        // System.Console.WriteLine(nodeValue.GetType());
        if
        (
            (nodeValue == null) ||
            (nodeValue is double && (double)nodeValue is 0) ||
            (nodeValue is string && (string)nodeValue == string.Empty)
        )
        { return false; }
        else { return true; }

    }
}