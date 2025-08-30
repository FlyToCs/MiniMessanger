namespace MiniMessenger.Domain.Entities;

public class Message : BaseEntity
{
    public User? SendFrom { get; set; }
    public User? SendTo { get; set; }
    public DateTime SendTime { get; set; } = DateTime.Now;
    public string? Text { get; set; }


}