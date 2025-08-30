namespace MiniMessenger.Domain.Domain_Exceptions;

public class InvalidEmailException : ValidationException
{
    public InvalidEmailException()
    {
        
    }

    public InvalidEmailException(string message) : base(message)
    {
        
    }
}