using EmployeeTask.API.Models;

namespace EmployeeTask.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeModel?> GetEmployeeByID(int id, CancellationToken ct);
        Task<bool> EnableEmployee(int id, bool enable, CancellationToken ct);
    }
}
