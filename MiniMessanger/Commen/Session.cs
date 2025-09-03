using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Commen;

public class Session
{
    public User? CurrentUser { get; set; }
    public bool IsLogin { get; private set; }

    public void Login(User user)
    {
        IsLogin = true;
        CurrentUser = user;
    }

    public void Logout()
    {
        IsLogin = false;
        CurrentUser =null;
    }
}