using EmployeeTask.API.Models;

namespace EmployeeTask.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetRawData(int id, CancellationToken ct);
        Task<bool> EnableEmployee(int id, bool enable, CancellationToken ct);
    }
}
