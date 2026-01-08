using EmployeeTask.API.ExceptionHandling.CustomExceptions;
using EmployeeTask.API.Models;
using EmployeeTask.API.Repositories.Interfaces;
using EmployeeTask.API.Services.Interfaces;

namespace EmployeeTask.API.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepo, ILogger<EmployeeService> _logger) : IEmployeeService
    {
        public async Task<EmployeeModel?> GetEmployeeByID(int id, CancellationToken ct)
        {
            _logger.LogInformation("Start building a tree for employee with Id {id}", id);

            var rawData = await employeeRepo.GetRawData(id, ct);

            if (!rawData.Any())
            {
                _logger.LogWarning("Data for employee with Id {id} not found", id);
                throw new NotFoundException($"Data for employee with Id {id} not found");
            }

            var lookup = rawData.ToLookup(e => e.ManagerId);

            var root = rawData.FirstOrDefault(e => e.Id == id);

            if (root != null)
            {
                BuildTree(root, lookup);
            }

            _logger.LogInformation("Tree for employee with Id {id} is built", id);
            return root;
        }

        public async Task<bool> EnableEmployee(int id, bool enable, CancellationToken ct)
        {
            return await employeeRepo.EnableEmployee(id, enable, ct);
        }

        private void BuildTree(EmployeeModel parent, ILookup<int?, EmployeeModel> lookup)
        {
            parent.Employees = lookup[parent.Id].ToList();
            foreach (var child in parent.Employees)
            {
                BuildTree(child, lookup);
            }
        }
    }
}
