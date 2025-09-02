using MiniMessenger.Domain.Entities;

namespace MiniMessenger.Domain.Interfaces.Repository_contracts;

public interface IMessageRepository
{
    List<Message> ShowInBox(int userId);
    List<Message> ShowSendBox(int userId);
    void CreateMessage(User sender, User receiver , string text);
}