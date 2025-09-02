using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;
using MiniMessenger.Domain.Interfaces.Repository_contracts;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;
using Spectre.Console;

namespace MiniMessenger.Application_Services.Services;

public class UserService() : IUserService
{
    private readonly IUserRepository _userRepository = new FileUserRepository(@"D:\Database.txt");


    public void Register(string userName, string password)
    {

        List<User> userList = _userRepository.GetAllUser() ?? new List<User>();
        foreach (var user in userList)
        {
            if (user.UserName == userName)
            {
                throw new Exception("this username is already taken");
            }
        }
        _userRepository.AddUser(userName, password);
    }

    public User Login(string userName, string password)
    {
        var user = _userRepository.GetUserByUserName(userName);
        if (user == null)
            throw new Exception("the username is not registered");

        if (!user.VerifyPassword(password))
            throw new Exception("The username or password in incorrect");

        return user;
    }

    public void ChangePassword(string userName, string oldPassword, string newPassword)
    {
        var user = _userRepository.GetUserByUserName(userName);
        user.ChangePassword(oldPassword, newPassword);
        _userRepository.UpdateUser(user);
    }

    public void ChangeStatus(int id, UserStatusEnum status)
    {
        var user = _userRepository.GetUserById(id);
        user.UserStatus = status;
        _userRepository.UpdateUser(user);
    }

    public List<User> Search(string userName)
    {
        return _userRepository.GetUsersStartWith(userName);
    }

    public User GetUserById(int userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public User GetUserByName(string userName)
    {
        return _userRepository.GetUserByUserName(userName);
    }

    public int GenerateId()
    {
        int max = 0;
        foreach (var user in _userRepository.GetAllUser())
        {
            if (user.Id> max)
            {
                max = user.Id;
            }
        }

        return max + 1;
    }
}