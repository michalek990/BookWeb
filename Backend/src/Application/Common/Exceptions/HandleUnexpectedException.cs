namespace Application.Common.Exceptions;

public class HandleUnexpectedException : Exception
{
    public HandleUnexpectedException(string message) : base(message) { }
}