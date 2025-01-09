using System.Text.RegularExpressions;

namespace MySqlParser;

public class Lexer
{
    private static readonly Dictionary<string, TokenType> _keywords = new()
    {
        {"SELECT", TokenType.Keyword},
        {"FROM", TokenType.Keyword},
        {"WHERE", TokenType.Keyword},
        {"INSERT", TokenType.Keyword},
        {"INTO", TokenType.Keyword},
        {"VALUES", TokenType.Keyword},
        {"UPDATE", TokenType.Keyword},
        {"SET", TokenType.Keyword},
        {"DELETE", TokenType.Keyword}
    };

    private static readonly char[] _splitFlags = [' ', '\t', '\n', '\r', ',', ';'];
    public List<Token> Tokenize(string originSql)
    {
        var tokens = new List<Token>();
        //var parts = originSql.Split(_splitFlags, StringSplitOptions.RemoveEmptyEntries);
        string[] parts = Regex.Split(originSql, @"(\s+|=|,|;|\(|\))");

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part)) continue;

            if (_keywords.TryGetValue(part.ToUpper(), out _))
            {
                tokens.Add(new Token(TokenType.Keyword, part.ToUpper()));
            }
            else if (Regex.IsMatch(part, @"^\d+$"))
            {
                tokens.Add(new Token(TokenType.Literal, part));
            }
            else if (Regex.IsMatch(part, @"[(),;]"))
            {
                tokens.Add(new Token(TokenType.Punctuation, part));
            }
            else
            {
                tokens.Add(new Token(TokenType.Identifier, part));
            }
        }

        return tokens;
    }
}