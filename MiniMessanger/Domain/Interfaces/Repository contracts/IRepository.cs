using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Domain.Interfaces.Repository_contracts;

public interface IRepository
{
    public User GetUserByUserName(string username);
    public List<User> GetAllUser();
    public void AddUser(string username, string password);
    public void UpdateUser(User user);
    public void DeleteUser(string username);
}