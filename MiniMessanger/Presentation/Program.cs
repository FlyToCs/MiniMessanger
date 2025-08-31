// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using MiniMessenger.Application_Services.Services;
using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
using MiniMessenger.Infrastructure;
using Spectre.Console;


IUserService userService =
    new UserService(new FileRepository(@"D:\Database.txt"),
        new Session());
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
                userService.Login(input.Parameters["username"], input.Parameters["password"]);
                Console.WriteLine("you were login successfully");
                Console.ReadKey();
                break;

            case "ChangeStatus":
                break;

            case "ChangePassword":
                break;

            case "SearchUserName":
                break;

            case "SendMessage":
                break;

            case "Inbox":
                break;

            case "SendBox":
                break;

            case "Logout":
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