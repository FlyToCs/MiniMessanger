// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using MiniMessenger.Application_Services.Services;
using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;
using Restaurant_Management_System.Presentation;
using Spectre.Console;


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
                Console.WriteLine("you registered successfully");
                Console.ReadKey();
                break;

            case "Login":
                var user = userService.Login(input.Parameters["username"], input.Parameters["password"]);
                Console.WriteLine("you were login successfully");
                session.Login(user);
                Console.ReadKey();
                break;

            case "ChangeStatus":

                if (session.IsLogin)
                {
                    userService.ChangeStatus(session.CurrentUser.Id, (UserStatusEnum)Convert.ToInt32(input.Parameters["status"]));
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
                    userService.ChangePassword(session.CurrentUser.UserName,oldPassword, newPassword );
                }
                Console.ReadKey();
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
                    ConsolePainter.WriteTable(messageService.ShowInBox(session.CurrentUser.Id));
                }
                Console.ReadKey();
                break;

            case "SendBox":
                if (session.IsLogin)
                {
                    ConsolePainter.WriteTable(messageService.ShowSendBox(session.CurrentUser.Id));
                }
                
                break;

            case "Log":
                break;

            case "Logout":
                session.Logout();
                flag = false;
                break;

        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.ReadKey();
    }

}



static Command ParseCommand(string input)
{
    var userInputParts = input.Split(' ');
    var command = new Command
    {
        Instruction = userInputParts[0],
        Parameters = new Dictionary<string, string>()
    };

    for (int i = 1; i < userInputParts.Length; i++)
    {
        if (userInputParts[i].StartsWith("--"))
        {
            string key = userInputParts[i].Substring(2).ToLower();
            string value = "";
            if (i + 1 < userInputParts.Length && !userInputParts[i + 1].StartsWith("--"))
            {
                value = userInputParts[i + 1];
                i++;
            }
            command.Parameters[key] = value;
        }
    }
    return command;
}

Console.ReadKey();