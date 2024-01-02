using System.IO.Compression;
using System.Xml;

namespace GeoEngine;
public partial class ASTBuilder
{
    Expression BuildUnaryNode(TokenType _operator, Scope scope)
    {
        switch (_operator)
        {
            case TokenType.Addition:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    PositiveNumber node = new PositiveNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel4(scope);
                    return node;
                }
                else
                {
                    PositiveNumber node = new PositiveNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }

            case TokenType.Substraction:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    NegativeNumber node = new NegativeNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel4(scope);
                    return node;
                }
                else
                {
                    NegativeNumber node = new NegativeNumber(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }

            default:
                if (nextToken.Type is TokenType.LeftParenthesis)
                {
                    Not node = new Not(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildLevel2(scope);
                    return node;
                }
                else
                {
                    Not node = new Not(null!, currentLine);
                    MoveNext();
                    node.Expression = BuildAtom(scope);
                    return node;
                }
        }
    }

    Node BuildBinaryNode(Node leftNode, TokenType operation, Node rightNode)
    {

        switch (operation)
        {
            case TokenType.And:
                BinaryExpression andNode = new And(leftNode, rightNode, currentLine);
                leftNode = andNode;
                break;

            case TokenType.Or:
                BinaryExpression orNode = new Or(leftNode, rightNode, currentLine);
                leftNode = orNode;
                break;

            case TokenType.GreaterOrEquals:
                BinaryExpression greaterOrEqualsNode = new GreaterOrEquals(leftNode, rightNode, currentLine);
                leftNode = greaterOrEqualsNode;
                break;

            case TokenType.GreaterThan:
                BinaryExpression greaterThanNode = new GreaterThan(leftNode, rightNode, currentLine);
                leftNode = greaterThanNode;
                break;

            case TokenType.LessOrEquals:
                BinaryExpression lessOrEqualsNode = new LessOrEquals(leftNode, rightNode, currentLine);
                leftNode = lessOrEqualsNode;
                break;

            case TokenType.LessThan:
                BinaryExpression lesserThanNode = new LessThan(leftNode, rightNode, currentLine);
                leftNode = lesserThanNode;
                break;

            case TokenType.EqualsTo:
                BinaryExpression equalsTo = new EqualsTo(leftNode, rightNode, currentLine);
                leftNode = equalsTo;
                break;

            case TokenType.NotEquals:
                BinaryExpression notEquals = new NotEquals(leftNode, rightNode, currentLine);
                leftNode = notEquals;
                break;

            case TokenType.Addition:
                BinaryExpression additionNode = new Addition(leftNode, rightNode, currentLine);
                leftNode = additionNode;
                break;

            case TokenType.Substraction:
                BinaryExpression difference = new Difference(leftNode, rightNode, currentLine);
                leftNode = difference;
                break;

            case TokenType.Product:
                BinaryExpression productNode = new Product(leftNode, rightNode, currentLine);
                leftNode = productNode;
                break;

            case TokenType.Quotient:
                BinaryExpression quotientNode = new Quotient(leftNode, rightNode, currentLine);
                leftNode = quotientNode;
                break;

            case TokenType.Modulo:
                BinaryExpression moduloNode = new Modulo(leftNode, rightNode, currentLine);
                leftNode = moduloNode;
                break;

            case TokenType.Power:
                BinaryExpression powerNode = new Power(leftNode, rightNode, currentLine);
                leftNode = powerNode;
                break;
        }

        return leftNode;
    }

    private Node BuildTernaryNode(Scope scope)
    {
        int expressionLine = currentLine;
        MoveNext();
        Node condition = BuildLevel1(scope);
        Expect(TokenType.Then);
        Node trueNode = BuildLevel1(scope);
        Expect(TokenType.Else);
        Node falseNode = BuildLevel1(scope);
        IfThenElse node = new IfThenElse(condition, trueNode, falseNode, expressionLine);
        return node;
    }

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

        else if (nextToken.Type is TokenType.Comma && !HandlingFunction)
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
        // switch (nextToken.Type)
        // {
        //     case TokenType.Assignment:
        //         MoveNext(2);
        //         ConstantDeclaration constant =
        //         new ConstantDeclaration(identifier, null!, scope, idLine);
        //         constant.CheckSemantic();
        //         scope.Constants.Add(constant);
        //         constant.Expression = BuildLevel1(scope);
        //         // Expect(TokenType.Semicolon);
        //         return null!;

        //     case TokenType.LeftParenthesis:
        //         HandlingFunction = true;
        //         // Initializing args list
        //         List<Node> arguments = new List<Node>();
        //         MoveNext(2);
        //         GetArguments(ref arguments, scope);
        //         Expect(TokenType.RightParenthesis);

        //         // at this point all arguments have been gathered
        //         // being an equals token means it is a function declaration

        //         if (IsAFunctionDeclaration())
        //         {
        //             FunctionDeclaration function =
        //             new FunctionDeclaration(identifier, arguments, scope, currentLine);
        //             GetBody(function.Body, scope);
        //             // Expect(TokenType.Semicolon);
        //             scope.Functions.Add(function);
        //             return null!;
        //         }

        //         // otherwise it means it is a function invocation
        //         FunctionInvocation functionCallNode =
        //         new FunctionInvocation(identifier, arguments, scope, idLine);
        //         return functionCallNode;

        //     case TokenType.Comma:
        //         List<string> constantNames = new List<string>();
        //         constantNames.Add(currentToken.GetName());
        //         MoveNext(2);
        //         GetConstants(ref constantNames);
        //         Node sequence = BuildLevel1(scope);

        //         // this expression or this expression result must be a sequence
        //         // System.Console.WriteLine(string.Join('\n', constantNames));
        //         for (int i = 0; i < constantNames.Count; i++)
        //         {
        //             if (constantNames[i] is "_")
        //                 continue;

        //             scope.Constants.Add
        //             (
        //                 new ConstantsDeclarationForSequences
        //                 (
        //                     constantNames[i],
        //                     null!,
        //                     sequence,
        //                     i,
        //                     i == constantNames.Count - 1,
        //                     scope,
        //                     idLine
        //                 )
        //             );
        //         }
        //         return null!;

        //     default:
        //         Constant node = new Constant(scope, identifier, idLine);
        //         MoveNext();
        //         return node;
        // }
    }

    private Node BuildLetNode(Scope scope)
    {
        Let node = new Let(currentLine);
        Scope child = scope.MakeChild();
        MoveNext();

        while (currentToken.Type is not TokenType.In)
        {
            Node instruction = BuildLevel1(child);
            Expect(TokenType.Semicolon);
            if (instruction is not null)
                node.Instructions.Add(instruction!);
        }

        MoveNext();
        node.InNode = BuildLevel1(child);
        if (node.InNode is not Expression)
            new Error
            (
                ErrorKind.Syntax,
                ErrorCode.invalid,
                $"let node instruction, it must be an expression",
                currentLine
            );
        return node;
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

    Node BuildSequence(Scope scope)
    {
        List<Node> sequenceNodes = new List<Node>();
        int sequenceLine = currentLine;
        bool isFinite;
        bool hasTriplePoints;

        if (nextToken.Type is TokenType.RightCurlyBracket)
        {
            MoveNext(2);
            // System.Console.WriteLine("vacia");
            return new FiniteSequence(sequenceNodes, currentLine);
        }

        MoveNext();
        GetSequenceElement(scope, ref sequenceNodes, out isFinite, out hasTriplePoints);

        if (isFinite && hasTriplePoints)
        {
            // System.Console.WriteLine("finita 3 puntos");
            return new FiniteGeneratedSequence
            (
                sequenceNodes,
                sequenceNodes[sequenceNodes.Count - 2],
                sequenceNodes[sequenceNodes.Count - 1],
                sequenceLine
            );
        }

        if (isFinite && !hasTriplePoints)
        {
            // System.Console.WriteLine("finita normal");
            return new FiniteSequence(sequenceNodes, sequenceLine);
        }

        else
        {

            // System.Console.WriteLine("infinita");
            return new InfiniteSequence
            (
                sequenceNodes,
                sequenceNodes[sequenceNodes.Count - 1],
                sequenceLine
            );
        }
    }

    void GetSequenceElement(Scope scope, ref List<Node> sequenceNodes, out bool isFinite, out bool hasTriplePoints)
    {
        Node node = BuildLevel1(scope);
        sequenceNodes.Add(node);
        if (currentToken.Type is TokenType.Comma)
        {
            MoveNext();
            GetSequenceElement(scope, ref sequenceNodes, out isFinite, out hasTriplePoints);
        }
        else if (currentToken.Type is TokenType.TriplePoint && nextToken.Type is TokenType.RightCurlyBracket)
        {
            MoveNext(2);
            isFinite = false;
            hasTriplePoints = true;
            return;
        }
        else if (currentToken.Type is TokenType.TriplePoint && nextToken.Type is not TokenType.RightCurlyBracket)
        {
            MoveNext(1);
            Node upperBound = BuildLevel1(scope);
            sequenceNodes.Add(upperBound);
            Expect(TokenType.RightCurlyBracket);
            isFinite = true;
            hasTriplePoints = true;
            return;
        }
        else
        {
            MoveNext();
            isFinite = true;
            hasTriplePoints = false;
            return;
        }
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

    Node BuiltInFunction(string functionName, Scope scope, int idLine)
    {
        Node builtInFunction = null!;
        MoveNext();

        switch (functionName)
        {
            case "print":
                Node argument = BuildLevel1(scope);
                builtInFunction = new Print(argument, idLine);
                break;
        }

        return builtInFunction;
    }

    private void MoveNext()
    {
        currentTokenIndex++;

        if (currentTokenIndex < tokens.Count)
        {
            currentToken = tokens[currentTokenIndex];
        }
    }

    private void MoveNext(int positions)
    {
        currentTokenIndex += positions;

        if (currentTokenIndex < tokens.Count)
        {
            currentToken = tokens[currentTokenIndex];
        }
    }

    private void Expect(TokenType expected)
    {

        if (currentToken.Type != expected)
        {
            if (previousToken.LineOfCode != currentLine)
            {
                new Error(ErrorKind.Syntax, ErrorCode.expected, $"\"{expected}\"", currentToken.LineOfCode - 1);
            }
            else
            {
                new Error(ErrorKind.Syntax, ErrorCode.expected, $"\"{expected}\"", currentToken.LineOfCode);
            }
        }

        MoveNext();
    }

    //  ==> (And Or) <==
    bool IsALevel1Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.And,
                TokenType.Or
            };

        return operators.Contains(operation);
    }

    // ==> (< <= >= > == !=) <==
    bool IsALevel2Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.LessThan,
                TokenType.LessOrEquals,
                TokenType.GreaterThan,
                TokenType.GreaterOrEquals,
                TokenType.EqualsTo,
                TokenType.NotEquals
            };

        return operators.Contains(operation);
    }

    // ==> (+ -) <==
    bool IsALevel3Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Addition,
                TokenType.Substraction,
            };

        return operators.Contains(operation);
    }

    // ==> (* / %) <==
    bool IsALevel4Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Product,
                TokenType.Quotient,
                TokenType.Modulo
            };

        return operators.Contains(operation);
    }

    // ==> (^) <==
    bool IsALevel5Operator(TokenType operation)
    {
        List<TokenType> operators = new List<TokenType>()
            {
                TokenType.Power
            };

        return operators.Contains(operation);
    }

    bool IsABuiltInFunction(string identifier)
    {
        List<string> builtInFunctions = new()
        {
            "print", "sin","cos","log","exp","sqrt"
        };

        return builtInFunctions.Contains(identifier);
    }

}