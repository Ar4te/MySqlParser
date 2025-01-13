
using System;

namespace MySqlParser
{
    public sealed class Token
    {
        private Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Type}: {Value}";

        public static Token New(TokenType type, string value)
        {
            return new Token(type, value);
        }
    }

    [Flags]
    public enum TokenType
    {
        Keyword,
        Identifier,
        Operator,
        Literal,
        Punctuation,
        Unknown,
        AssociateKeyword,
        Comment
    }
}