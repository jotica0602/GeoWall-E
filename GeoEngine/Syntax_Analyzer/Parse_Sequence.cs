namespace GeoEngine;
public partial class ASTBuilder
{
    Node BuildSequence(Scope scope)
    {
        HandlingSequence = true;
        List<Node> sequenceNodes = new List<Node>();
        int sequenceLine = currentLine;
        bool isFinite;
        bool hasTriplePoints;

        if (nextToken.Type is TokenType.RightCurlyBracket)
        {
            HandlingSequence = false;
            MoveNext(2);
            // System.Console.WriteLine("vacia");
            return new FiniteSequence(sequenceNodes, currentLine);
        }

        MoveNext();
        GetSequenceElement(scope, ref sequenceNodes, out isFinite, out hasTriplePoints);

        if (isFinite && hasTriplePoints)
        {
            HandlingSequence = false;
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
            HandlingSequence = false;
            // System.Console.WriteLine("finita normal");
            return new FiniteSequence(sequenceNodes, sequenceLine);
        }

        else
        {
            HandlingSequence = false;
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
}