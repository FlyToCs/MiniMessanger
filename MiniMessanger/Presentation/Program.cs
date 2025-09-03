// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using MiniMessenger.Application_Services.Services;
using MiniMessenger.Commen;
using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;
using Restaurant_Management_System.Presentation;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;


IUserService userService = new UserService();
IMessageService messageService = new MessageService();
Session session = new Session();


bool flag = true;

while (flag)
{
    Console.Clear();
    Console.WriteLine(FiggleFonts.Standard.Render("Mini - Messenger CLI"));
    Console.Write("Enter a command: ");
    var input = ParseCommand(Console.ReadLine()!);

    try
    {
        switch (input.Instruction)
        {
            case "Register":
                userService.Register(
                    input.Parameters["username"],
                    input.Parameters["password"]
                );
                ConsolePainter.GreenMessage("you registered successfully");
                Console.ReadKey();
                break;

            case "Login":
                var user = userService.Login(input.Parameters["username"], input.Parameters["password"]);
                ConsolePainter.GreenMessage("you were login successfully");
                session.Login(user);
                Console.ReadKey();
                break;

            case "ChangeStatus":

                if (session.IsLogin)
                {
                    userService.ChangeStatus(session.CurrentUser.Id, (UserStatusEnum)Convert.ToInt32(input.Parameters["status"]));
                    //session.CurrentUser = userService.GetUserById(session.CurrentUser.Id);
                }
                Console.ReadKey();
                break;

            case "ChangePassword":
                if (session.IsLogin)
                {
                    Console.Write("Enter Old Password: ");
                    string oldPassword = Console.ReadLine()!;
                    Console.Write("Enter new Password: ");
                    string newPassword = Console.ReadLine()!;
                    userService.ChangePassword(session.CurrentUser.UserName, oldPassword, newPassword);
                    //session.CurrentUser = userService.GetUserById(session.CurrentUser.Id);
                }
                Console.ReadKey();
                break;

            case "ChangeName":

                if (session.IsLogin)
                {
                    
                }
                break;

            case "Search":
                if (session.IsLogin)
                {
                    ConsolePainter.WriteTable(userService.Search(input.Parameters["username"]), ConsoleColor.Yellow, ConsoleColor.Cyan);
                }

                Console.ReadKey();
                break;


            case "SendMessage":
                var sentTo = userService.GetUserByName(input.Parameters["username"]);
                messageService.SendMessage(session.CurrentUser.Id, sentTo.Id, input.Parameters["message"]);
                break;

            case "Inbox":
                if (session.IsLogin)
                {
                    var inbox = messageService.ShowInBox(session.CurrentUser.Id);
                    foreach (var message in inbox)
                    {
                        Console.WriteLine($"📬 [Receive from]: {message.SendFrom.UserName:-15}   [Message]: {message.TextMessage} ");
                    }
                }
                Console.ReadKey();
                break;

            case "Sendbox":
                if (session.IsLogin)
                {
                    var sendBox = messageService.ShowSendBox(session.CurrentUser.Id);
                    foreach (var message in sendBox)
                    {
                        Console.WriteLine($"📩 [Sent to]: {message.SendTo.UserName:-15}  [Message]: {message.TextMessage}");
                    }
                }

                Console.ReadKey();
                break;

            case "Log":
                break;

            case "Profile":
                session.CurrentUser = userService.GetUserById(session.CurrentUser.Id);
                if (session.IsLogin)
                {
                    var fullName = $"{session.CurrentUser.FirstName} {session.CurrentUser.LastName}";
                    if (fullName == "empty empty")
                        fullName = "---";

                    Console.WriteLine($"\n🔰 FirstName: {fullName}");
                    Console.WriteLine($"🪪 UserName: {session.CurrentUser.UserName}");
                    Console.WriteLine($"⚙️ Status: {session.CurrentUser.UserStatus}");
                    Console.ReadKey();
                }

                break;

            case "Help":
                Console.WriteLine("\nRegister --username[username] --password[password]");
                Console.WriteLine("Login --username[username] --password[password]");
                Console.WriteLine("ChangePassword --username[username] --password[password]");
                Console.WriteLine("ChangeName --firstname[firstname] --lastname[lastname]");
                Console.WriteLine("ChangeStatus --status[available/UnAvailable]");
                Console.WriteLine("SendMessage --username[toUsername] --message[message]");
                Console.WriteLine("Inbox");
                Console.WriteLine("Sandbox");
                Console.WriteLine("Profile");
                Console.ReadKey();

                break;
            case "Logout":
                session.Logout();
                //flag = false;
                break;
            default:
                ConsolePainter.RedMessage("Invalid Command");
                Console.ReadKey();
                break;

        }
    }
    catch (Exception e)
    {
        ConsolePainter.RedMessage(e.Message);
        Console.ReadKey();
    }

}



// static Command ParseCommand(string input)
// {
//     var userInputParts = input.Split(' ');
//     var command = new Command
//     {
//         Instruction = userInputParts[0],
//         Parameters = new Dictionary<string, string>()
//     };
//
//     for (int i = 1; i < userInputParts.Length; i++)
//     {
//         if (userInputParts[i].StartsWith("--"))
//         {
//             string key = userInputParts[i].Substring(2).ToLower();
//             string value = "";
//             if (i + 1 < userInputParts.Length && !userInputParts[i + 1].StartsWith("--"))
//             {
//                 value = userInputParts[i + 1];
//                 i++;
//             }
//             command.Parameters[key] = value;
//         }
//     }
//     return command;
// }
//


static Command ParseCommand(string input)
{
    var command = new Command
    {
        Parameters = new Dictionary<string, string>()
    };

    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    command.Instruction = parts[0];

    string? currentKey = null;
    var currentValue = new List<string>();

    for (int i = 1; i < parts.Length; i++)
    {
        var part = parts[i];

        if (part.StartsWith("--"))
        {

            if (currentKey != null)
            {
                command.Parameters[currentKey] = string.Join(" ", currentValue);
                currentValue.Clear();
            }

            currentKey = part.Substring(2).ToLower();
        }
        else
        {
            currentValue.Add(part);
        }
    }


    if (currentKey != null)
    {
        command.Parameters[currentKey] = string.Join(" ", currentValue);
    }

    return command;
}



Console.ReadKey();