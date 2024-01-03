namespace GeoEngine;
using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

public class DrawEngine
{
    public static IJSRuntime _jsRuntime;
    static Lexer lexer;
    public static string Text;
    public static string ErrorMessage { get; set; }
    public static string Logs { get; set; }
    public static Stack<string> stackColor = new Stack<string>();
    public static void DrawMotor(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        // try
        // {
        Main();
        // }
        // catch (Exception ex)
        // {
        //     ErrorMessage = Error.GetErrors();
        // }
    }
    public static void Main()
    {
        lexer = new Lexer(Text);
        List<Token> tokens = lexer.Tokenize();
        stackColor = new Stack<string>();

        //if there are any errors at the end of the lexer stop the program and show them
        if (Error.diagnostics.Count > 0)
        {
            //we got some lexycal errors
            throw new Exception();
        }
        //else we process to pase
        else
        {
            Scope scope = new Scope();
            ASTBuilder parser = new ASTBuilder(tokens);
            List<Node> nodes = parser.BuildNodes(scope);

            //now we look for syntax errors
            if (Error.diagnostics.Count > 0)
            {
                //we got some syntax errors
                throw new Exception();
            }
            // we look for semantic errors
            else
            {
                foreach (Expression node in nodes.Where(node => node is Expression))
                    node.CheckSemantic();

                if (Error.diagnostics.Count > 0)
                {
                    throw new Exception();
                }
                //finally we evaluate
                else
                {
                    foreach (Expression node in nodes.Where(node => node is Expression))
                    {
                        node.Evaluate();
                    }

                    //we look for run time error at the end of evaluation
                    if (Error.diagnostics.Count > 0)
                    {
                        throw new Exception();
                    }
                }
            }
        }

    }

    public static void UpdateErrors()
    {
        ErrorMessage = Error.GetErrors();
    }

    public static void UpdateLogs()
    {
        Logs = Print.GetAllLogs();
    }

    public static void ClearLogs()
    {
        Print.Logs = new List<string>();
    }
}