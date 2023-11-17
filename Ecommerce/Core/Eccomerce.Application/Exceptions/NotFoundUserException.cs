using System.Runtime.Serialization;

namespace Ecomerce.Application.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException()
    {
    }

    protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundUserException(string? message) : base(message)
    {
    }

    public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}