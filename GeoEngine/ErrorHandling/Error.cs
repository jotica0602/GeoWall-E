namespace GeoEngine;

//This class is for keeping track of every error in the GeoWallE
//Errors are base on 4 statements, the error code, the error kind, the error message and the error location

public class Error
{
    public static List<Error> allErrors = new List<Error>();
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
        allErrors.Add(this);
    }

    public static void ShowErrors()
    {
        foreach (Error error in allErrors)
        {
            Console.WriteLine($"! {error.errorKind} Error: {error.errorCode} {error.argument} in line number {error.location}");
        }
    }
}