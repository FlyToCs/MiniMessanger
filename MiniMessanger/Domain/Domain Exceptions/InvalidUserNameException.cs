namespace MiniMessenger.Domain.Domain_Exceptions;

public class InvalidUserNameException : ValidationException
{
    public InvalidUserNameException()
    {
        
    }

    public InvalidUserNameException(string message) : base(message)
    {
        
    }
}