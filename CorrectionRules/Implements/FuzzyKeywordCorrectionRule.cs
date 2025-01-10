using MySqlParser.CorrectionRules.Interfaces;

namespace MySqlParser.CorrectionRules.Implements;

public class FuzzyKeywordCorrectionRule : ISqlCorrectionRule
{
    private const double Threshold = 0.5;

    public bool AppliesTo(Token token, List<Token> context, int position)
    {
        if (token.Type != TokenType.Identifier) return false;
        bool isKeywordContext = position > 0 && context[position - 1].Type == TokenType.AssociateKeyword;
        if (isKeywordContext) return false;

        string tokenValue = token.Value.ToUpper();
        var _bestSimilarity = 0.0;
        foreach (var keyword in Lexer._keywords.Keys)
        {
            double similarity = CalculateSimilarity(tokenValue, keyword);
            _bestSimilarity = similarity > _bestSimilarity ? similarity : _bestSimilarity;
            if (similarity > Threshold)
            {
                return true;
            }
        }

        return false;
    }

    public Token Correct(Token token)
    {
        string tokenValue = token.Value.ToUpper();
        string bestMatch = tokenValue;
        double bestSimilarity = 0.0;
        foreach (var keyword in Lexer._keywords.Keys)
        {
            double similarity = CalculateSimilarity(tokenValue, keyword);
            if (similarity > bestSimilarity)
            {
                bestSimilarity = similarity;
                bestMatch = keyword;
            }
        }

        return Token.New(TokenType.Keyword, bestMatch);
    }

    private static double CalculateSimilarity(string source, string target)
    {
        int distance = LevenshteinDistance(source, target);
        return 1.0 - (double)distance / Math.Max(source.Length, target.Length);
    }

    public static int LevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return target?.Length ?? 0;

        if (string.IsNullOrEmpty(target))
            return source.Length;

        int sourceLength = source.Length;
        int targetLength = target.Length;
        int[,] distance = new int[sourceLength + 1, targetLength + 1];

        for (int i = 0; i <= sourceLength; distance[i, 0] = i++) { }
        for (int j = 0; j <= targetLength; distance[0, j] = j++) { }

        for (int i = 1; i <= sourceLength; i++)
        {
            for (int j = 1; j <= targetLength; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceLength, targetLength];
    }
}