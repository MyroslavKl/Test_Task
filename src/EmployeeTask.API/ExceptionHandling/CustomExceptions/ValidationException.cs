namespace EmployeeTask.API.ExceptionHandling.CustomExceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message) : base(message) { }
    }
}
