namespace GeoEngine;
public partial class ASTBuilder
{
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
}