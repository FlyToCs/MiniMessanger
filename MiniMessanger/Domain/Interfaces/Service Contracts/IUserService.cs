
using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Domain.Interfaces.Service_Contracts;

public interface IUserService
{
    public void Register(string userName, string password);
    public User Login(string userName, string password);
    public void ChangePassword(string oldPassword, string newPassword);
    public void ChangeStatus(string status);
    public List<User> Search(string userName);
    public bool SendMessage(string toUsername, string message);
    public List<Message> ShowInbox();
    public List<Message> ShowSendBox();
    public bool Logout();
}