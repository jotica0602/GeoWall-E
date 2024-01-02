namespace GeoEngine;

//This class is for keeping track of every error in the GeoWallE
//Errors are base on 4 statements, the error code, the error kind, the error message and the error location

public class Error
{
    public static List<Error> diagnostics = new List<Error>();
    private ErrorKind errorKind;
    private ErrorCode errorCode;
    private string argument = "";
    private int location = 0;

    public Error(ErrorKind _errorKind, ErrorCode _errorCode, string _argument, int _location)
    {
        errorKind = _errorKind;
        errorCode = _errorCode;
        argument = _argument;
        location = _location;
        diagnostics.Add(this);
    }

    public static void CheckErrors(ErrorKind kind)
    {
        if (Error.diagnostics.Count != 0)
        {
            Error.ShowErrors();
            Environment.Exit(0);
        }
        // else
        // {
        //     System.Console.WriteLine($"Clean of {kind} errors!");
        // }
    }

    static void ShowErrors()
    {
        foreach (Error error in diagnostics)
        {
            switch (error.errorKind)
            {
                case ErrorKind.Semantic:
                    Console.WriteLine($"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.");
                    break;

                case ErrorKind.Syntax:
                    Console.WriteLine($"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.");
                    break;

                case ErrorKind.Lexycal:
                    Console.WriteLine($"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.");
                    break;
                    
                default:
                    Console.WriteLine($"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.");
                    break;
            }
        }
    }
}

