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
	                        Id uniqueidentifier PRIMARY KEY,
	                        Name varchar(255),
	                        Surname varchar(255),
	                        Email varchar(255),
                        );
                        INSERT INTO [dbo].[User] VALUES ('58e8974b-c358-4c38-b4d2-743b03d90045', 'TEST1', 'test1', 'email1@email.com');
                        INSERT INTO [dbo].[User] VALUES ('247dcd8a-ea88-4b1f-ad25-3cbb98e00587', 'TEST2', 'test2', 'email2@email.com');
                        INSERT INTO [dbo].[User] VALUES ('847f863c-543e-4ca6-99d9-8e750e080ee2', 'TEST3', 'test3', 'email3@email.com')";
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
