using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoBlazorApp.Components.Data.DbContext
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public IDbConnection Connection { get; set; }

        public DbConnectionFactory(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            try
            {
                var connString = new SqlConnection(_connectionString);
                await connString.OpenAsync();
                return connString;
            }
            catch
            {
                throw;
            }
        }
        public IDbConnection GetOpenAPPConnection()
        {
            try
            {
                IDbConnection connection;
                connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch
            {
                throw;
            }
        }
        public IDbConnection GetOpenDBConnection(string connectionstring)
        {
            try
            {
                IDbConnection connection;
                connection = new SqlConnection(connectionstring);
                connection.Open();
                return connection;
            }
            catch
            {
                throw;
            }
        }
    }
}
