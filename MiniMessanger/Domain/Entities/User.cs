using System.ComponentModel.DataAnnotations;
using MiniMessenger.Domain.Domain_Exceptions;
using MiniMessenger.Domain.Enums;
using Newtonsoft.Json;

namespace MiniMessenger.Domain.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public string? UserName { get; private set; }
    [JsonProperty]
    private string? Password { get; set; }
    public UserStatusEnum UserStatus { get; set; } = UserStatusEnum.Active;
    public UserRoleEnum UserRole { get; private set; } = UserRoleEnum.User;


    public User(string firstName, string lastName, string email, string userName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        Password = password;

    }

    [JsonConstructor]
    public User(string userName, string password) : 
        this("empty", "empty", "test@gmail.com", userName,  password)
    {
        
    }




    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (Password != oldPassword)
            throw new PasswordValidationException("The old password didn't match");
        if (newPassword.Length < 6)
            throw new PasswordValidationException("The new password must be at least 6 characters");
        Password = newPassword;
    }
    private void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new PasswordValidationException("Password cannot be null or empty.");

        if (password.Length < 6)
            throw new PasswordValidationException("Password must be at least 6 characters long.");

        Password = password;
    }

    public bool VerifyPassword(string password)
    {
        return Password == password;
    }


    public void ChangeFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidNameException("First name cannot be empty or whitespace.");

        if (firstName.Length < 3)
            throw new InvalidNameException("First name must be at least 3 characters long.");

        FirstName = firstName;
    }

    public void ChangeLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidNameException("Last name cannot be empty or whitespace.");

        if (lastName.Length < 3)
            throw new InvalidNameException("Last name must be at least 3 characters long.");

        LastName = lastName;
    }


    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidEmailException("Email cannot be null or empty.");

        int atCount = 0;
        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@')
                atCount++;
        }

        if (atCount != 1)
            throw new InvalidEmailException("Email must contain exactly one '@' character.");

        bool hasDot = false;
        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '.')
            {
                hasDot = true;
                break;
            }
        }

        if (!hasDot)
            throw new InvalidEmailException("Email must contain at least one '.' character.");

        Email = email;
    }

    public void ChangeUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new InvalidUserNameException("Username cannot be null or empty.");

        if (userName.Length < 3)
            throw new InvalidUserNameException("Username must be at least 3 characters long.");

       
        if (char.IsDigit(userName[0]))
            throw new InvalidUserNameException("Username cannot start with a digit.");

        
        for (int i = 0; i < userName.Length; i++)
        {
            char c = userName[i];
            if (!(char.IsLetterOrDigit(c) || c == '_'))
                throw new InvalidUserNameException("Username can only contain letters, digits, or underscore (_).");
        }

        UserName = userName;
    }





}