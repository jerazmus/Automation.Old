using Dapper;
using Microsoft.Data.SqlClient;

namespace Application.Database.Dapper
{
    public class DapperRepository
    {
        private SqlConnection _masterConnection;
        public SqlConnection _appConnection;

        public DapperRepository(SqlConnection masterConnection, SqlConnection appConnection)
        {
            _masterConnection = masterConnection;
            _appConnection= appConnection;
        }

        public void GenerateDatabase()
        {
            CreateDatabase();
            CreateTable();
        }

        private void CreateDatabase()
        {
            var sql = @"USE MASTER;
                        DROP DATABASE IF EXISTS Application;
                        CREATE DATABASE Application";
            using(_masterConnection)
            {
                foreach(var statement in sql.Split(";"))
                {
                    _masterConnection.Execute(statement);
                }
            }
        }

        private void CreateTable()
        {
            var sql = @"USE Application;
                        CREATE TABLE [dbo].[User] ( 
	                        Guid varchar(255) PRIMARY KEY,
	                        Name varchar(255),
	                        Surname varchar(255),
	                        Email varchar(255),
                        )";
            using (_appConnection)
            {
                foreach (var statement in sql.Split(";"))
                {
                    _appConnection.Execute(statement);
                }
            }
        }
    }
}
