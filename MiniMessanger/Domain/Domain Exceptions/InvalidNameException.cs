namespace MiniMessenger.Domain.Domain_Exceptions;

public class InvalidNameException : ValidationException
{
    public InvalidNameException()
    {
        
    }

    public InvalidNameException(string message) : base(message)
    {
        
    }
}