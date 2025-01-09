namespace MySqlParser;

public class AstNode
{
    public string Type { get; set; }
    public string Value { get; set; }
    public List<AstNode> Children { get; } = [];

    public AstNode(string type, string? value = null)
    {
        Type = type;
        Value = value;
    }
}