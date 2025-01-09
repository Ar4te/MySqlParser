
namespace MySqlParser;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _position;
    private SqlCorrector _corrector;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
        _corrector = new SqlCorrector();
    }

    public void Parse()
    {
        while (_position < _tokens.Count)
        {
            var token = _tokens[_position];
            Console.WriteLine($"Token: {token.Type},Value: {token.Value}");
            _position++;
        }
    }

    public string CorrectSyntax()
    {
        var tokens = _corrector.CorrectTokens(_tokens);
        return string.Join(" ", tokens.Select(t => t.Value));
    }
}
