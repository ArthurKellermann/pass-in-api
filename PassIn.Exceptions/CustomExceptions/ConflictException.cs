using PassIn.Exceptions.ApplicationExcpetions;

namespace PassIn.Exceptions.CustomExceptions;
public class ConflictException : PassInException
{
    public ConflictException(string message) : base(message)
    {
    }
}
