
using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;

namespace MiniMessenger.Domain.Interfaces.Service_Contracts;

public interface IUserService
{
    public void Register(string userName, string password);
    public User Login(string userName, string password);
    public void ChangePassword(string userName, string oldPassword, string newPassword);
    public void ChangeStatus(int id, UserStatusEnum status);
    public List<User> Search(string userName);
    public User GetUserById(int userId);
    public User GetUserByName(string userName);

}