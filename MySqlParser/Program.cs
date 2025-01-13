using System;

namespace MySqlParser
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string sqlInsert = "Iert into TEST set (id,age,username) values (1,2,\"3\")";

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(sqlInsert);

            Console.WriteLine("Original Tokens:");
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine(token);
            //}

            var parser = new Parser(tokens);

            string correctedQuery = parser.Parse();
            Console.WriteLine("\nCorrected SQL Query:");
            Console.WriteLine(correctedQuery);

            Console.ReadLine();
        }
    }
}