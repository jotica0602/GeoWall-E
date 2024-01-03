using System.Linq.Expressions;

namespace GeoEngine;
public abstract class FigureFunction : Expression
{
    protected FigureFunction(int lineOfCode) : base(lineOfCode)
    {
    }
}