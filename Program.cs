// See https://aka.ms/new-console-template for more information
using MySqlParser;

//string sql = "SELECT \"name\", [age] FROM users WHERE age > 18;";
//var lexer = new Lexer();
//var tokens = lexer.Tokenize(sql);

//foreach (var token in tokens)
//{
//    Console.WriteLine(token);
//}

//var parser = new Parser(tokens);
//try
//{
//    var astNode = parser.Parse();
//    var converter = new SqlConverter("MySQL");
//    string convertedSql = converter.Convert(astNode);
//    Console.WriteLine("Converted SQL:");
//    Console.WriteLine(convertedSql);
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Error parsing SQL:");
//    Console.WriteLine(ex.Message);
//}

//string sqlQuery = "SELECT * FORM users WHERE id = 1";
string sqlQuery = "SELCT [id as [ID], [username] as [UserName] FORM users WHERE [id]= 1";

var lexer = new Lexer();
var tokens = lexer.Tokenize(sqlQuery);

Console.WriteLine("Original Tokens:");
foreach (var token in tokens)
{
    Console.WriteLine(token);
}

var parser = new Parser(tokens);
parser.Parse();

string correctedQuery = Parser.CorrectSyntax(tokens);
Console.WriteLine("\nCorrected SQL Query:");
Console.WriteLine(correctedQuery);

Console.ReadLine();