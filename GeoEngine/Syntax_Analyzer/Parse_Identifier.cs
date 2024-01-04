namespace GeoEngine;
public partial class ASTBuilder
{
    private Node HandleIdentifier(Scope scope)
    {
        string identifier = currentToken.GetName();
        int idLine = currentLine;
        
        if (IsABuiltInFunction(identifier))
            return BuiltInFunction(identifier, scope, idLine);

        if (nextToken.Type is TokenType.Assignment)
        {
            MoveNext(2);
            ConstantDeclaration constant =
            new ConstantDeclaration(identifier, null!, scope, idLine);
            constant.CheckSemantic();
            scope.Constants.Add(constant);
            constant.Expression = BuildLevel1(scope);
            // Expect(TokenType.Semicolon);
            return null!;
        }
        else if (nextToken.Type is TokenType.LeftParenthesis)
        {
            HandlingFunction = true;
            // Initializing args list
            List<Node> arguments = new List<Node>();
            MoveNext(2);
            GetArguments(ref arguments, scope);
            Expect(TokenType.RightParenthesis);
            
            HandlingFunction =false;

            // at this point all arguments have been gathered
            // being an equals token means it is a function declaration

            if (IsAFunctionDeclaration())
            {
                FunctionDeclaration function =
                new FunctionDeclaration(identifier, arguments, scope, currentLine);
                GetBody(function.Body, scope);
                // Expect(TokenType.Semicolon);
                scope.Functions.Add(function);
                HandlingFunction = false;
                return null!;
            }

            // otherwise it means it is a function invocation
            FunctionInvocation functionCallNode =
            new FunctionInvocation(identifier, arguments, scope, idLine);
            HandlingFunction = false;
            return functionCallNode;
        }

        else if (nextToken.Type is TokenType.Comma && !HandlingFunction && !HandlingSequence)
        {
            List<string> constantNames = new List<string>();
            constantNames.Add(currentToken.GetName());
            MoveNext(2);
            GetConstants(ref constantNames);
            Node sequence = BuildLevel1(scope);

            // this expression or this expression result must be a sequence
            // System.Console.WriteLine(string.Join('\n', constantNames));
            for (int i = 0; i < constantNames.Count; i++)
            {
                if (constantNames[i] is "_")
                    continue;

                scope.Constants.Add
                (
                    new ConstantsDeclarationForSequences
                    (
                        constantNames[i],
                        null!,
                        sequence,
                        i,
                        i == constantNames.Count - 1,
                        scope,
                        idLine
                    )
                );
            }
            return null!;
        }

        else
        {
            Constant node = new Constant(scope, identifier, idLine);
            MoveNext();
            return node;
        }
    }

    void GetArguments(ref List<Node> arguments, Scope scope)
    {

        if (currentToken.Type is TokenType.RightParenthesis)
            return;

        arguments.Add(BuildLevel1(scope));
        while (currentToken.Type is TokenType.Comma)
        {
            MoveNext();
            arguments.Add(BuildLevel1(scope));
        }
    }

    bool IsAFunctionDeclaration() => currentToken.Type is TokenType.Assignment;

    void GetBody(List<Token> body, Scope scope)
    {
        MoveNext();
        int bodyStartingIndex = currentTokenIndex;
        Node _body = BuildLevel1(scope);

        if (_body is not Expression)
            new Error
            (
                ErrorKind.Syntax,
                ErrorCode.invalid,
                $"function body, it must be an expression",
                currentLine
            );

        int bodyEndingIndex = currentTokenIndex;
        for (int i = bodyStartingIndex; i <= bodyEndingIndex; i++)
            body.Add(tokens[i]);
        body.Add(new Keyword(TokenType.EndOfFile, body.Last().LineOfCode));
    }


    void GetConstants(ref List<string> constantNames)
    {

        Expect(TokenType.Identifier);
        string constantName = tokens[currentTokenIndex - 1].GetName();
        constantNames.Add(constantName);

        if (currentToken.Type is TokenType.Comma)
        {
            MoveNext();
            GetConstants(ref constantNames);
        }

        else if (currentToken.Type is TokenType.Assignment)
            MoveNext();

        else
        {
            new Error
            (
                ErrorKind.Syntax,
                ErrorCode.unexpected,
                $"token \"{currentToken}\", \"{TokenType.Identifier}\" or \"{TokenType.Assignment}\" expected.",
                currentLine
            );
        }
    }
}