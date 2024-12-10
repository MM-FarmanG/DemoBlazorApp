using Dapper;
using DemoBlazorApp.Components.Data.DbContext;
using DemoBlazorApp.Components.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorApp.Components.Data
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAuthentication(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _connectionFactory = dbConnectionFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> RegisterDomain(string name, string domain)
        {
            using (var db = _connectionFactory.GetOpenAPPConnection())
            {
                var DbconnectionString = $"Data Source={_configuration["ServerConfig:ServerName"]};Initial Catalog={domain};User Id={_configuration["ServerConfig:UserName"]};Password={_configuration["ServerConfig:Password"]};TrustServerCertificate=True;";
                var query = $"insert into client (Name,Subdomain,DBConnection) values ('{name}','{domain}','{DbconnectionString}')";
                await db.QueryFirstOrDefaultAsync(query);
                return true;
            }
        }
        public async Task<bool> LoginUser(string name)
        {
            using (var db = _connectionFactory.GetOpenAPPConnection())
            {
                var query = $"select Name from Client where Name = '{name}'";
                var Data = await db.QueryFirstOrDefaultAsync<string>(query);
                if (Data != null && Data == name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<StudentData>> GetStudent(string key)
        {
            var keydata = GetHostName();
            if (string.IsNullOrEmpty(keydata))
            {
                return null; 
            }
            else
            {
                var connection = string.Empty;
                using (var db = _connectionFactory.GetOpenAPPConnection())
                {
                    var query = $"select DBConnection from client where Subdomain = '{keydata}'";
                    connection = await db.QueryFirstOrDefaultAsync<string>(query);
                }

                if (string.IsNullOrEmpty(connection))
                {
                    return null;
                }

                using (var db = _connectionFactory.GetOpenDBConnection(connection))
                {
                    var query = $"select *from student";
                    var Data = await db.QueryAsync<StudentData>(query);
                    if (Data != null)
                    {
                        return Data.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
           
        }

        public async Task<bool> AddStudent(StudentData student, string key)
        {
            var keydata = GetHostName();
            if (string.IsNullOrEmpty(keydata))
            {
                return false;
            }
            else
            {
                var connection = string.Empty;
                using (var db = _connectionFactory.GetOpenAPPConnection())
                {
                    var query = $"select DBConnection from client where Subdomain = '{keydata}'";
                    connection = await db.QueryFirstOrDefaultAsync<string>(query);
                }

                if (string.IsNullOrEmpty(connection))
                {
                    return false;
                }

                using (var db = _connectionFactory.GetOpenDBConnection(connection))
                {
                    var query = $"Insert into student(Name,Age,ClassName) Values ('{student.Name}',{student.Age},'{student.ClassName}')";
                    var Data = await db.QueryFirstOrDefaultAsync(query);
                }
                return true;
            }

        }

        private string GetHostName()
        {
            var key = _contextAccessor?.HttpContext?.Request.Host.Host.ToString();
            int index = key.IndexOf('.');
            if (index > 0)
            {
                return key.Substring(0, index).ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
