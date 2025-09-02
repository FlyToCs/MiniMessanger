using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Domain.Interfaces.Service_Contracts;

public interface IMessageService
{
    void SendMessage(int senderId, int receiverId, string message);
    List<Message> ShowInBox(int userId);
    List<Message> ShowSendBox(int userId);

}