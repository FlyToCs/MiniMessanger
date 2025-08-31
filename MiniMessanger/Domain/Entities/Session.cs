namespace MiniMessenger.Domain.Entities;

public class Session
{
    public User? CurrentUser { get; set; }

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser =null;
    }
}