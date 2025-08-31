using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Repository_contracts;
using Newtonsoft.Json;

namespace MiniMessenger.Infrastructure;

public class FileRepository(string path) : IRepository
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


    public void UpdateUser(string userName, User user)
    {
        var users = LoadUsersFromFile();
        bool updated = false;

        foreach (var myUser in users)
        {
            if (myUser.UserName == userName)
            {
                if (user.FirstName != null)
                    myUser.ChangeFirstName(user.FirstName);
                if (user.LastName != null)
                    myUser.ChangeLastName(user.LastName);
                if (user.Email != null)
                    myUser.ChangeEmail(user.Email);

                updated = true;
                break; 
            }
        }

        if (!updated)
            throw new InvalidOperationException("No user found with this username.");

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