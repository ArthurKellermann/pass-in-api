using PassIn.Exceptions.ApplicationExcpetions;

namespace PassIn.Exceptions.CustomExceptions;
public class NotFoundException : PassInException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
