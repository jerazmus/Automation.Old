using Application.Database.Dapper;
using Microsoft.Extensions.Configuration;

namespace Application.Database
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var dapper = new DapperRepository(
                DapperProvider.GetSqlConnection(config.GetConnectionString("MasterDb")),
                DapperProvider.GetSqlConnection(config.GetConnectionString("ApplicationDb")));
            dapper.GenerateDatabase();
        }
    }
}