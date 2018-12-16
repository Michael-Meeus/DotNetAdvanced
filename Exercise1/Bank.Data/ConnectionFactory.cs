using System;
using System.Configuration;
using System.Data.SqlClient;
using Bank.Data.Interfaces;

namespace Bank.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private static readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BankConnection"].ConnectionString;
        //"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BankDB_ADO_Test;Integrated Security=True";

        public SqlConnection CreateSqlConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}