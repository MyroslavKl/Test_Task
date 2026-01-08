using EmployeeTask.API.Models;
using EmployeeTask.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTask.API.Repositories
{
    public class EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> _logger) : BaseRepository(configuration), IEmployeeRepository
    {
        private const string QUERY_UPDATE_ENABLE_STATUS = @"
            update Employee 
            set 
              Enable = @enable 
            where 
                ID = @id
        ";

        private const string QUERY_GET_EMPLOYEE = @"
            with EmployeeTree as (
                select ID, Name, ManagerID, Enable
                from Employee
                where 
                    ID = @id
                union all
                select e.ID, e.Name, e.ManagerID, e.Enable
                from Employee e
                inner join EmployeeTree et on e.ManagerID = et.ID
            )
            select * 
            from EmployeeTree
        ";

        public async Task<bool> EnableEmployee(int id, bool enable, CancellationToken ct)
        {
            var result = new List<EmployeeModel>();

            using var connection = await CreateConnectionAsync();
            using var command = new SqlCommand(QUERY_UPDATE_ENABLE_STATUS, connection);
            command.Parameters.AddWithValue("@enable", enable);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync(ct) > 0;
        }

        public async Task<List<EmployeeModel>> GetRawData(int id, CancellationToken ct)
        {
            var result = new List<EmployeeModel>();

            try {
                using var connection = await CreateConnectionAsync();
                using var command = new SqlCommand(QUERY_GET_EMPLOYEE, connection);
                command.Parameters.AddWithValue("@RootID", id);

                using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync())
                {
                    result.Add(new EmployeeModel
                    {
                        Id = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        ManagerId = reader.IsDBNull("ManagerID") ? null : reader.GetInt32("ManagerID"),
                        Enable = reader.GetBoolean("Enable")
                    });
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Request to get tree for Id {id} was canceled", id);
                throw;
            }

            return result;
        }
    }
}
