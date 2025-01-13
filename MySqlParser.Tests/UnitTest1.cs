using Xunit.Abstractions;

namespace MySqlParser.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void InsertSqlTest()
    {
        string sqlInsert = """Isert ito TEST set (id,age,username) values (1,2,"insert") """ + "-- TestComment";

        var lexer = new Lexer();
        var tokens = lexer.Tokenize(sqlInsert);

        _testOutputHelper.WriteLine("Original Tokens:");
        foreach (var token in tokens)
        {
            _testOutputHelper.WriteLine(token.ToString());
        }

        var corrector = new SqlCorrector();
        var correctedTokens = corrector.CorrectTokens(tokens);
        var sql = string.Join(" ", correctedTokens.Select(t => t.Value));

        _testOutputHelper.WriteLine("\nCorrected SQL Query:");
        _testOutputHelper.WriteLine(sql);
        _testOutputHelper.WriteLine("≤‚ ‘Ω· ¯");
    }

    [Fact]
    public void SelectSqlTest()
    {
        string sql = "sELct selCreateTime as selct,ere as \"where\" FORM users ere WHERE [id]= 1";
        DealTest(sql);
    }

    [Fact]
    public void UpdateSqlTest()
    {
        string sql = "updat UserInfo set [username] = \"ABC\" ere where = 1";
        DealTest(sql);
        //Assert.True("UPDATE UserInfo SET [username] = \"ABC\" WHERE [id] = 1".Equals(_sql), "There are some errors occured!");
    }

    private void DealTest(string sql)
    {
        _testOutputHelper.WriteLine("Original Sql:");
        _testOutputHelper.WriteLine(sql);

        var lexer = new Lexer();
        var tokens = lexer.Tokenize(sql);

        _testOutputHelper.WriteLine("\nOriginal Tokens:");
        foreach (var token in tokens)
        {
            _testOutputHelper.WriteLine(token.ToString());
        }

        var parser = new Parser(tokens);
        var _sql = parser.Parse();
        _testOutputHelper.WriteLine("\nCorrected Tokens:");
        foreach (var token in parser.Tokens)
        {
            _testOutputHelper.WriteLine(token.ToString());
        }

        _testOutputHelper.WriteLine("\nCorrected PGSQL Query:");
        _testOutputHelper.WriteLine(_sql);
        _testOutputHelper.WriteLine("≤‚ ‘Ω· ¯");
    }
}