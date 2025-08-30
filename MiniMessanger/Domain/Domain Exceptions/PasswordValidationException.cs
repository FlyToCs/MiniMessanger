namespace MiniMessenger.Domain.Domain_Exceptions;

public class PasswordValidationException : ValidationException
{
    public PasswordValidationException()
    {
        
    }

    public PasswordValidationException(string message): base(message) 
    {
        
    }
}