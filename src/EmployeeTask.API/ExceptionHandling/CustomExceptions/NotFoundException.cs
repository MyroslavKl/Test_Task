namespace EmployeeTask.API.ExceptionHandling.CustomExceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
