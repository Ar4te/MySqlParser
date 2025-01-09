namespace MySqlParser;

public class Token(TokenType type, string value)
{
    public TokenType Type { get; set; } = type;
    public string Value { get; set; } = value;

    public override string ToString() => $"{Type}: {Value}";
}

public enum TokenType
{
    Keyword,
    Identifier,
    Operator,
    Literal,
    Punctuation,
    Unknown
}