public enum TokenKind
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

    Color,

    Restore,

    Import,

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

    Colour,

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

    Unknown,
}