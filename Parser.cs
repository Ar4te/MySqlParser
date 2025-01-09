
namespace MySqlParser;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _position;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
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

    public static string CorrectSyntax(List<Token> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].Type == TokenType.Identifier && tokens[i].Value.Equals("SELCT", StringComparison.OrdinalIgnoreCase))
            {
                tokens[i].Value = "SELECT";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Did you mean 'SELECT'?");
                Console.ResetColor();
            }
            if (tokens[i].Type == TokenType.Identifier && tokens[i].Value.Equals("FORM", StringComparison.OrdinalIgnoreCase))
            {
                tokens[i].Value = "FROM";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Did you mean 'FROM'?");
                Console.ResetColor();
            }
        }

        return string.Join(" ", tokens.Select(t => t.Value));
    }
}
