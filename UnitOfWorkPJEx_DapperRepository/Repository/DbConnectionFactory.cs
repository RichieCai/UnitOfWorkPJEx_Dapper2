using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
