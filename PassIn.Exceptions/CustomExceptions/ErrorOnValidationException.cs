using PassIn.Exceptions.ApplicationExcpetions;

namespace PassIn.Exceptions.CustomExceptions;
public class ErrorOnValidationException : PassInException
{
    public ErrorOnValidationException(string message) : base(message)
    {
    }
}
