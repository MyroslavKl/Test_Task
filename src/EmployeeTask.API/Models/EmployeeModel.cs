namespace EmployeeTask.API.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        public bool Enable { get; set; }
        public List<EmployeeModel> Employees { get; set; } = new();
    }
}
