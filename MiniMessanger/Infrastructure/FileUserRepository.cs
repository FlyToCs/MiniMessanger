using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Repository_contracts;
using Newtonsoft.Json;

namespace MiniMessenger.Infrastructure;

public class FileUserRepository(string path) : IUserRepository
{
    private List<User> LoadUsersFromFile()
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<User>>(json)!;
    }

    private void SaveUsersToFile(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users);
        File.WriteAllText(path, json);
    }

    public User GetUserByUserName(string username)
    {

        foreach (var user in LoadUsersFromFile())
        {
            if (user.UserName == username)
                return user;
        }
        throw new InvalidOperationException("No any users found with this username");
    }

    public User GetUserById(int id)
    {
        foreach (var user in LoadUsersFromFile())
        {
            if (user.Id == id)
                return user;
        }
        throw new InvalidOperationException("No any users found with this username");
    }

    public List<User> GetUsersStartWith(string username)
    {
        List<User> filterUser = new();
        foreach (var user in LoadUsersFromFile())
        {
            if (user.UserName.StartsWith(username))
                filterUser.Add(user);
        }
        return filterUser;
    }

    public List<User> GetAllUser()
    {
        var userList =  LoadUsersFromFile();
        return userList;
    }

    public void AddUser(string username, string password)
    {
        var newList = LoadUsersFromFile() ?? new List<User>();

        newList.Add(new User(username, password));
        SaveUsersToFile(newList);
    }


    public void UpdateUser(User user)
    {
        var users = LoadUsersFromFile();
        foreach (var oldUser in users)
        {
            if (oldUser.UserName == user.UserName)
            {
                users.Remove(oldUser);
                break;
            }


        }
        users.Add(user);

        SaveUsersToFile(users);
    }


    public void DeleteUser(string username)
    {
        var users = LoadUsersFromFile();
        User? userToRemove = null;

        foreach (var user in users)
        {
            if (user.UserName == username)
            {
                userToRemove = user;
                break;
            }
        }

        if (userToRemove == null)
            throw new InvalidOperationException("No user found to delete.");

        users.Remove(userToRemove);
        SaveUsersToFile(users);
    }

}