using EmployeeTask.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmplyeeByID(int id, CancellationToken ct) 
        {
            var result = await employeeService.GetEmployeeByID(id, ct);
            return Ok(new { Employee = result });
        }

        [HttpPost("{id}/enable")]
        public async Task<IActionResult> EnableEmployee(int id, [FromBody] bool enable, CancellationToken ct)
        {
            var success = await employeeService.EnableEmployee(id, enable, ct);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
