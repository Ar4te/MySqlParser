namespace MySqlParser.CorrectionRules.Interfaces;

public interface ISqlCorrectionRule
{
    bool AppliesTo(Token token, List<Token> context, int position);
    Token Correct(Token token);
}