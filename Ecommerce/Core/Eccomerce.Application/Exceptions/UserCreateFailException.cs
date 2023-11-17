namespace Ecomerce.Application.Exceptions;

public class UserCreateFailException : Exception
{
    public UserCreateFailException() : base("User Create Fail")
    {
    }

    public UserCreateFailException(string? message) : base(message)
    {
    }

    public UserCreateFailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}