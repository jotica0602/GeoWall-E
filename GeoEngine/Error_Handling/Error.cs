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

    public static void CheckErrors()
    {
        if (Error.diagnostics.Count != 0)
        {
            Error.ShowErrors();
        }
    }

    public static void ShowErrors()
    {
        foreach (Error error in diagnostics)
        {
            Console.WriteLine($"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.");
        }
    }

    //This method is for returning one string with all errors
    public static string GetErrors()
    {
        string errors = "";
        foreach (Error error in diagnostics)
        {
            switch (error.errorKind)
            {
                case ErrorKind.Semantic:
                    errors += $"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.<br />";
                    break;

                case ErrorKind.Syntax:
                    errors += $"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.<br />";
                    break;

                case ErrorKind.Lexycal:
                    errors += $"!{error.errorKind} Error: {error.errorCode} {error.argument} in line {error.location}.<br />";
                    break;
            }
        }
        return errors;
    }
}


