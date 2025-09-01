using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;
using Spectre.Console;

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
        var user = _repo.GetUserByUserName(userName);
        if (user == null)
            throw new Exception("the username is not registered");

        if (!user.VerifyPassword(password))
            throw new Exception("The username or password in incorrect");

        return user;
    }

    public void ChangePassword(string userName, string oldPassword, string newPassword)
    {
        var user = _repo.GetUserByUserName(userName);
        user.ChangePassword(oldPassword, newPassword);
        _repo.UpdateUser(user);
    }

    public void ChangeStatus(int id, UserStatusEnum status)
    {
        var user = _repo.GetUserById(id);
        user.UserStatus = status;
        _repo.UpdateUser(user);
    }

    public List<User> Search(string userName)
    {
        return _repo.GetUsersStartWith(userName);
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