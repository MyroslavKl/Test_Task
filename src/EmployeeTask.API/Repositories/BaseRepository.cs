using Microsoft.Data.SqlClient;

namespace EmployeeTask.API.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        protected BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("Connection string is not found");
        }

        protected async Task<SqlConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
