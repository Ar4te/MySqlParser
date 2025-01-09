// See https://aka.ms/new-console-template for more information
using MySqlParser;

public class SqlConverter
{
    private readonly string _targetDbType;
    public SqlConverter(string targetDbType)
    {
        _targetDbType = targetDbType;
    }

    public string Convert(AstNode astNode)
    {
        return ConvertNode(astNode);
    }

    private string ConvertNode(AstNode astNode)
    {
        switch (astNode.Type)
        {
            case "SELECT_STATEMENT":
                return ConvertSelectStatement(astNode);
            case "FIELD_LIST":
                return string.Join(", ", astNode.Children.Select(ConvertNode));
            case "IDENTIFIER":
            case "NUMBER":
            case "STRING":
                return astNode.Value;
            case "CONDITION":
                return $"{ConvertNode(astNode.Children[0])} {astNode.Children[1].Value} {ConvertNode(astNode.Children[2])}";
            default:
                throw new Exception($"Unknown AST node type: {astNode.Type}");
        }
    }

    private string ConvertSelectStatement(AstNode astNode)
    {
        var fields = ConvertNode(astNode.Children[0]);
        var table = ConvertNode(astNode.Children[2]);
        var condition = ConvertNode(astNode.Children[4]);

        if (_targetDbType == "PostgreSQL")
        {
            return $"SELECT {fields} FROM {table} WHERE {condition}";
        }
        else if(_targetDbType == "SQLServer")
        {
            return $"SELECT {fields} FROM {table} WHERE {condition};";
        }
        else
        {
            return $"SELECT {fields} FROM {table} WHERE {condition};";
        }
    }
}