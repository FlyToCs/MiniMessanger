using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;

namespace MiniMessenger.Application_Services.Services;

public class UserService(FileRepository repo, Session session) : IUserService
{
    private readonly FileRepository _repo = repo;
    private readonly Session _session = session;

    public void Register(string userName, string password)
    {
        List<User> userList = _repo.GetAllUser() ?? new List<User>();
        foreach (var user in userList)
        {
            if (user.UserName == userName)
            {
                throw new Exception("this username is already taken");
            }
        }
        _repo.AddUser(userName, password);
    }

    public User Login(string userName, string password)
    {
        
        throw new NotImplementedException();
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public void ChangeStatus(string status)
    {
        throw new NotImplementedException();
    }

    public List<User> Search(string userName)
    {
        throw new NotImplementedException();
    }

    public bool SendMessage(string toUsername, string message)
    {
        throw new NotImplementedException();
    }

    public List<Message> ShowInbox()
    {
        throw new NotImplementedException();
    }

    public List<Message> ShowSendBox()
    {
        throw new NotImplementedException();
    }

    public bool Logout()
    {
        throw new NotImplementedException();
    }
}