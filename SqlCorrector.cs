using MySqlParser.CorrectionRules.Implements;
using MySqlParser.CorrectionRules.Interfaces;

namespace MySqlParser;

public class SqlCorrector
{
    private readonly List<ISqlCorrectionRule> _rules = new();
    public SqlCorrector()
    {
        _rules.Add(new FuzzyKeywordCorrectionRule());
        _rules.Add(new SquareBracketCorrectionRule());
    }

    public List<Token> CorrectTokens(List<Token> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            foreach (var rule in _rules)
            {
                if (rule.AppliesTo(tokens[i], tokens, i))
                {
                    tokens[i] = rule.Correct(tokens[i]);
                    break;
                }
            }
        }

        return tokens;
    }
}