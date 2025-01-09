using System.Text.RegularExpressions;

namespace MySqlParser;

public partial class Lexer
{
    private readonly Dictionary<string, TokenType> _keywords = new()
    {
        { "SELECT", TokenType.Keyword },
        { "FROM", TokenType.Keyword },
        { "WHERE", TokenType.Keyword },
        { "INSERT", TokenType.Keyword },
        { "INTO", TokenType.Keyword },
        { "VALUES", TokenType.Keyword },
        { "UPDATE", TokenType.Keyword },
        { "SET", TokenType.Keyword },
        { "DELETE", TokenType.Keyword }
    };

    private static readonly char[] _splitFlags = [' ', '\t', '\n', '\r', ',', ';'];
    public List<Token> Tokenize(string originSql)
    {
        var tokens = new List<Token>();
        string[] parts = SplitRegex().Split(originSql);

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part)) continue;

            if (_keywords.TryGetValue(part.ToUpper(), out _))
            {
                tokens.Add(Token.New(TokenType.Keyword, part.ToUpper()));
            }
            else if (DigitRegex().IsMatch(part))
            {
                tokens.Add(Token.New(TokenType.Literal, part));
            }
            else if (PunctuationRegex().IsMatch(part))
            {
                tokens.Add(Token.New(TokenType.Punctuation, part));
            }
            else
            {
                tokens.Add(Token.New(TokenType.Identifier, part));
            }
        }

        return tokens;
    }

    [GeneratedRegex(@"^\d+$", RegexOptions.Compiled)]
    private static partial Regex DigitRegex();

    [GeneratedRegex(@"(\s+|=|,|;|\(|\))", RegexOptions.Compiled)]
    private static partial Regex SplitRegex();

    [GeneratedRegex(@"[(),;]", RegexOptions.Compiled)]
    private static partial Regex PunctuationRegex();
}