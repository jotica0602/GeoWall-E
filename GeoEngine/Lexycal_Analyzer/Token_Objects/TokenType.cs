public enum TokenType
{
    Identifier,

    // KeyWords
    Let,

    In,

    If, 

    Then,

    Else, 

    Point,

    Line,

    Segment,

    Ray,
    
    Circle,

    Sequence,

    Restore,

    Import,

    ColorKeyWord,

    Draw,

    Arc,

    Measure,

    Intersect,

    Count,

    Randoms,

    Points,

    Samples,

    Rest,

    // Data Types
    String,

    Number,

    Color,

    Undefined,

    // Arithmetic Operators
    Addition,

    Substraction,

    Product,

    Division,

    Power,

    Modulus,

    // Boolean Operators

    And,

    Or,

    Not,


    Equals,

    EqualsTo,

    LessOrEquals,

    LessThan,

    GreaterOrEquals,

    GreaterThan,

    NotEquals,


    // Punctuators
    LeftParenthesis,

    RightParenthesis,

    LeftCurlyBracket,
    
    RightCurlyBracket,

    TriplePoint,

    Comma,

    Semicolon,

    FullStop,

    Quote,

    // Utility 

    UnderScore,

    EndOfFile,

    LineBreak,

    Unknown,
}