using System.Runtime.Serialization;

namespace Ecomerce.Application.Exceptions;

public class AuthenticationErrorException : Exception
{
    public AuthenticationErrorException() : base("Authentication Error")
    {
    }

    protected AuthenticationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public AuthenticationErrorException(string? message) : base(message)
    {
    }

    public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}