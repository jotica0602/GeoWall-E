namespace GeoEngine;
public partial class ASTBuilder
{
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
}