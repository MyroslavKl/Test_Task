namespace EmployeeTask.API.ExceptionHandling.CustomExceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }
    }
}
