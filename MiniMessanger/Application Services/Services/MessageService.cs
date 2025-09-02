using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Repository_contracts;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;

namespace MiniMessenger.Application_Services.Services;

public class MessageService : IMessageService
{

    private readonly IMessageRepository _messageRepository = new FileMessageRepository(@"D:\Database.txt");
    private IUserService _userService = new UserService();
    public void SendMessage(int senderId, int receiverId, string message)
    {
        var sender = _userService.GetUserById(senderId);
        var receiver = _userService.GetUserById(receiverId);
        _messageRepository.CreateMessage(sender,receiver, message );
    }

    public List<Message> ShowInBox(int userId)
    {
        return _messageRepository.ShowInBox(userId);
    }

    public List<Message> ShowSendBox(int userId)
    {
        return _messageRepository.ShowSendBox(userId);
    }
}