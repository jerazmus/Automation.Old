using Microsoft.Data.SqlClient;

namespace Application.Database.Dapper
{
    public static class DapperProvider
    {
        public static SqlConnection GetSqlConnection(string connectionString)
            => new SqlConnection(connectionString);
    }
}
