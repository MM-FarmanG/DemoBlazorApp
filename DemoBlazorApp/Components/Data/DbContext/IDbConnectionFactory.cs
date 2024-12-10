using System.Data;

namespace DemoBlazorApp.Components.Data.DbContext
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenAPPConnection();
        IDbConnection GetOpenDBConnection(string connectionstring);
        Task<IDbConnection> CreateConnectionAsync();
    }
}
