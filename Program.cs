using MySqlParser;

//string sqlQuery = "SELCT `age` as 'Age, [id as [ID], [username] as [UserName] FORM users WHERE [id]= 1";

string sqlInsert = """Iert into TEST set (id,age,username) values (1,2,"3")""";

var lexer = new Lexer();
var tokens = lexer.Tokenize(sqlInsert);

Console.WriteLine("Original Tokens:");
//foreach (var token in tokens)
//{
//    Console.WriteLine(token);
//}

var parser = new Parser(tokens);
parser.Parse();

string correctedQuery = parser.CorrectSyntax();
Console.WriteLine("\nCorrected SQL Query:");
Console.WriteLine(correctedQuery);

Console.ReadLine();