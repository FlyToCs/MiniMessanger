namespace MiniMessenger.Domain.Entities;

public class Message : BaseEntity
{
    public User? SendFrom { get; set; }
    public User? SendTo { get; set; }
    public DateTime SendTime { get; set; }
    public string? TextMessage { get; set; }

    public Message(User sendFrom, User sendTo, string textMessage)
    {
        SendFrom = sendFrom;
        SendTo = sendTo;
        TextMessage = textMessage;
        SendTime = DateTime.Now;
    }

}