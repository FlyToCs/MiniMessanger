using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Repository_contracts;
using Newtonsoft.Json;
using System.IO;

namespace MiniMessenger.Infrastructure;

public class FileMessageRepository(string path) : IMessageRepository
{

    private readonly string _path = path;

    private List<Message> LoadMessagesFromFile()
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Message>>(json)!;
    }

    private void SaveMessageToFile(List<Message> messages)
    {
        var json = JsonConvert.SerializeObject(messages);
        File.WriteAllText(path, json);
    }
    public List<Message> ShowInBox(int userId)
    {
        List<Message> userInbox = new();
        var messages = LoadMessagesFromFile();
        foreach (var message in messages)
        {
            if (message.SendTo.Id == userId && message.SendFrom.Id != userId)
            {
                userInbox.Add(message);
            }
        }
        return userInbox;
    }

    public List<Message> ShowSendBox(int userId)
    {
        List<Message> sendBox = new();
        var messages = LoadMessagesFromFile();
        foreach (var message in messages)
        {
            if (message.SendTo.Id != userId && message.SendFrom.Id == userId )
            {
                sendBox.Add(message);
            }
        }
        return sendBox;
    }

    public void CreateMessage(User sender, User receiver, string text)
    {
        var messages = LoadMessagesFromFile() ?? new List<Message>(); ;
        messages.Add(new Message(sender, receiver, text));
        SaveMessageToFile(messages);
    }
}