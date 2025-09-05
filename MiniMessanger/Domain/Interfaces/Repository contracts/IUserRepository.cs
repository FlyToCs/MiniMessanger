using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Domain.Interfaces.Repository_contracts;

public interface IUserRepository
{
    public User GetUserByUserName(string username);
    public User GetUserById(int id);
    public List<User> GetUsersStartWith(string username);
    public List<User> GetAllUser();

    //return id user
    public void AddUser(string username, string password);
    public void UpdateUser(User user);
    public void DeleteUser(string username);

}