// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using MiniMessenger.Application_Services.Services;
using MiniMessenger.Commen;
using MiniMessenger.Domain.Entities;
using MiniMessenger.Domain.Enums;
using MiniMessenger.Domain.Interfaces.Service_Contracts;
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
    ConsolePainter.CyanMessage("=============================================================================");
    ConsolePainter.YellowMessage(FiggleFonts.Standard.Render("Mini - Messenger"));
    ConsolePainter.CyanMessage("=============================================================================");
    Console.Write("\n🔶 Enter a command: ");
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
                    userService.ChangeStatus(session.CurrentUser.Id, (UserStatusEnum)Convert.ToInt32(input.Parameters["status"]));
                else
                    ConsolePainter.RedMessage("First you need to log in to your account.");

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
                else
                    ConsolePainter.RedMessage("First you need to log in to your account.");

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
                else
                    ConsolePainter.RedMessage("First you need to log in to your account.");


                Console.ReadKey();
                break;


            case "SendMessage":
                if (session.IsLogin)
                {
                    var sentTo = userService.GetUserByName(input.Parameters["username"]);
                    messageService.SendMessage(session.CurrentUser.Id, sentTo.Id, input.Parameters["message"]);
                }
                else
                    ConsolePainter.RedMessage("First you need to log in to your account.");
                break;

            case "Inbox":
                if (session.IsLogin)
                {
                    var inbox = messageService.ShowInBox(session.CurrentUser.Id);

                    if (inbox.Any())
                    {
                        var inboxTable = new Table();
                        inboxTable.Border = TableBorder.Rounded;

                        inboxTable.AddColumn(new TableColumn("[bold yellow]📬 From[/]"));
                        inboxTable.AddColumn(new TableColumn("[bold cyan]✉️ Message[/]"));

                        foreach (var message in inbox)
                        {
                            inboxTable.AddRow(
                                $"[green]{message.SendFrom.UserName}[/]",
                                $"[white]{message.TextMessage}[/]"
                            );
                        }

                        AnsiConsole.Write(inboxTable);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]⚠️ Inbox is empty.[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]❌ First you need to log in to your account.[/]");
                }

                Console.ReadKey();
                break;

            case "Sendbox":

                if (session.IsLogin)
                {
                    var sendBox = messageService.ShowSendBox(session.CurrentUser.Id);

                    if (sendBox.Any())
                    {
                        var sendTable = new Table();
                        sendTable.Border = TableBorder.Rounded;
                        sendTable.AddColumn("[bold yellow]📤 Sent To[/]");
                        sendTable.AddColumn("[bold cyan]✉️ text[/]");

                        foreach (var message in sendBox)
                        {
                            sendTable.AddRow(
                                $"[green]{message.SendTo.UserName}[/]",
                                $"[white]{message.TextMessage}[/]"
                            );
                        }

                        AnsiConsole.Write(sendTable);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]⚠️ No messages in your SendBox.[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]❌ First you need to log in to your account.[/]");
                }


                Console.ReadKey();
                break;

            case "Log":
                break;

            case "Profile":
                Console.WriteLine();
                if (session.IsLogin)
                {
                    session.CurrentUser = userService.GetUserById(session.CurrentUser.Id);

                    var fullName = $"{session.CurrentUser.FirstName} {session.CurrentUser.LastName}";
                    if (fullName == "empty empty")
                        fullName = "---";

                    var profilePanel = new Panel(
                        $"[bold yellow]🔰 Name:[/] [white]{fullName}[/]\n" +
                        $"[bold yellow]🪪 UserName:[/] [green]{session.CurrentUser.UserName}[/]\n" +
                        $"[bold yellow]⚙️ Status:[/] [cyan]{session.CurrentUser.UserStatus}[/]"
                    )
                    {
                        Header = new PanelHeader("[bold green]👤 User Profile[/]"),
                        Border = BoxBorder.Rounded,
                        BorderStyle = new Style(Color.Blue)
                    };

                    AnsiConsole.Write(profilePanel);
                    Console.ReadKey();
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]❌ First you need to log in to your account.[/]");
                }


                break;

            case "Help":
            case "help":



                AnsiConsole.MarkupLine("\n[bold green]📖 Help Menu[/] \n");

                var table = new Table();
                table.Border = TableBorder.Rounded;
                table.AddColumn(new TableColumn("[bold yellow]Command[/]").Centered());
                table.AddColumn(new TableColumn("[bold cyan]Description[/]"));

                table.AddRow(
                    "[bold white]Register[/]",
                    "--username=[yellow][[username]][/] --password=[yellow][[password]][/]"
                );

                table.AddRow(
                    "[bold white]Login[/]",
                    "--username=[yellow][[username]][/] --password=[yellow][[password]][/]"
                );

                table.AddRow(
                    "[bold white]ChangePassword[/]",
                    "--username=[yellow][[username]][/] --password=[yellow][[password]][/]"
                );

                table.AddRow(
                    "[bold white]ChangeName[/]",
                    "--firstname=[yellow][[firstname]][/] --lastname=[yellow][[lastname]][/]"
                );

                table.AddRow(
                    "[bold white]ChangeStatus[/]",
                    "--status=[yellow][[available/UnAvailable]][/]"
                );

                table.AddRow(
                    "[bold white]SendMessage[/]",
                    "--username=[yellow][[toUsername]][/] --message=[yellow][[message]][/]"
                );

                table.AddRow(
                    "[bold white]Inbox[/]",
                    "[dim]Show all received messages[/]"
                );

                table.AddRow(
                    "[bold white]Sandbox[/]",
                    "[dim]Show all sent messages[/]"
                );

                table.AddRow(
                    "[bold white]Profile[/]",
                    "[dim]Show user profile information[/]"
                );

                AnsiConsole.Write(table);



                Console.ReadKey();

                break;
            case "Logout":
                if (!session.IsLogin)
                    ConsolePainter.RedMessage("You are not login. what hell are you doing?");
                else
                {
                    session.Logout();
                    ConsolePainter.GreenMessage("You are logged out of your account.");
                }

                //flag = false;
                Console.ReadKey();
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