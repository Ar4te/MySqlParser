
using System.Text;

namespace MySqlParser;

public class Parser
{
    private List<Token> _tokens;
    private SqlCorrector _corrector;
    private bool _hasCorrectSyntax = false;

    public List<Token> Tokens => _tokens;

    public Parser(List<Token> tokens)
    {
        _hasCorrectSyntax = false;
        _tokens = tokens;
        _corrector = new SqlCorrector();
    }

    public string Parse()
    {
        var tokens = _hasCorrectSyntax ? _tokens : CorrectSyntax();
        StringBuilder @stringBuilder = new();
        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Literal)
            {
                if (token.Value.GetType() == typeof(long))
                {
                    stringBuilder.Append($" {token.Value} ");
                }
                else
                {
                    stringBuilder.Append($" \"{token.Value}\" ");
                }
            }
            else
            {
                stringBuilder.Append($" {token.Value} ");
            }
        }

        return @stringBuilder.ToString();
    }

    public List<Token> CorrectSyntax()
    {
        _tokens = _corrector.CorrectTokens(_tokens);
        _hasCorrectSyntax = true;
        return _tokens;
    }
}
