using System.Collections.Generic;
using MySqlParser.CorrectionRules.Interfaces;

namespace MySqlParser.CorrectionRules.Implements
{
    public class SquareBracketCorrectionRule : ISqlCorrectionRule
    {
        public bool AppliesTo(Token token, List<Token> context, int position)
        {
            return token.Type == TokenType.Identifier && (token.Value.StartsWith("[") || token.Value.EndsWith("]"));
        }

        public Token Correct(Token token)
        {
            return Token.New(TokenType.Identifier, token.Value.Trim('[', ']'));
        }
    }
}
