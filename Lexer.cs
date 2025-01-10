using System.Text.RegularExpressions;

namespace MySqlParser;

public partial class Lexer
{
    public static readonly Dictionary<string, TokenType> _keywords = new()
    {
        { "SELECT", TokenType.Keyword },
        { "FROM", TokenType.Keyword },
        { "WHERE", TokenType.Keyword },
        { "INSERT", TokenType.Keyword },
        { "INTO", TokenType.Keyword },
        { "VALUES", TokenType.Keyword },
        { "UPDATE", TokenType.Keyword },
        { "SET", TokenType.Keyword },
        { "DELETE", TokenType.Keyword },
        { "AS", TokenType.AssociateKeyword },
    };

    private static readonly char[] _splitFlags = [' ', '\t', '\n', '\r', ',', ';', '[', ']', '"'];

    public List<Token> Tokenize(string originSql)
    {
        var tokens = new List<Token>();
        string[] parts = SplitRegex().Split(originSql);

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part)) continue;

            if (CommentRegex().IsMatch(part))
            {
                tokens.Add(Token.New(TokenType.Comment, part));
                continue;
            }

            if (StringLiteralRegex().IsMatch(part))
            {
                var _part = part.Trim(_splitFlags);
                tokens.Add(Token.New(TokenType.Literal, _part));
                continue;
            }

            if (_keywords.TryGetValue(part.ToUpper(), out var tokenType))
            {
                tokens.Add(Token.New(tokenType, part.ToUpper()));
                continue;
            }

            if (DigitRegex().IsMatch(part))
            {
                var _part = part.Trim(_splitFlags);
                tokens.Add(Token.New(TokenType.Literal, _part));
                continue;
            }

            if (PunctuationRegex().IsMatch(part))
            {
                tokens.Add(Token.New(TokenType.Punctuation, part));
                continue;
            }

            tokens.Add(Token.New(TokenType.Identifier, part));
        }

        return tokens;
    }

    [GeneratedRegex(@"^\d+$", RegexOptions.Compiled)]
    private static partial Regex DigitRegex();

    [GeneratedRegex(@"(\s+|=|,|;|\(|\))", RegexOptions.Compiled)]
    private static partial Regex SplitRegex();

    [GeneratedRegex(@"[(),;=]$", RegexOptions.Compiled)]
    private static partial Regex PunctuationRegex();

    [GeneratedRegex(@"^[`'""\[\]\w]+[`'""\[\]]$", RegexOptions.Compiled)]
    private static partial Regex StringLiteralRegex();

    [GeneratedRegex(@"--\s*(.*?)\n*", RegexOptions.Compiled)]
    private static partial Regex CommentRegex();
}