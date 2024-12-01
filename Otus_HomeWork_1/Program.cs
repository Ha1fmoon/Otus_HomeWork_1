using System.Text;

namespace Otus_HomeWork_1;

internal class Program
{
    public static void Main(string[] args)
    {
        var work = true;
        var user = string.Empty;
        var echoMessage = string.Empty;

        WelcomeMessage();

        while (work)
        {
            var input = GetUserInput(user);

            if (!string.IsNullOrEmpty(input) && input.StartsWith("/echo") && input.IndexOf(' ') != -1)
            {
                echoMessage = input.Substring(input.IndexOf(' ') + 1);
                input = "/echo";
            }

            switch (input)
            {
                case "/start":
                    user = string.IsNullOrEmpty(user) ? Start() : Restart(ref user);
                    break;
                case "/help":
                    AvailableOptions(user);
                    break;
                case "/echo":
                    Echo(user, echoMessage);
                    break;
                case "/info":
                    BotInfo();
                    break;
                case "/exit":
                    work = false;
                    break;
                default:
                    DefaultAction();
                    break;
            }
        }
    }
    
    private static string GetUserInput(string user)
    {
        if (!string.IsNullOrEmpty(user)) Console.WriteLine($"Hey, {user}.");
        Console.WriteLine("Enter your command:");
        return Console.ReadLine() ?? string.Empty;
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to home work bot.");
        Console.WriteLine("Available options are: /start, /help, /info, /exit.");
        Console.WriteLine();
    }

    private static string Start()
    {
        string user;
        
        do
        {
            Console.Clear();
            Console.WriteLine("Please enter your name:");
            user = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(user)) continue;
            
            Console.WriteLine("Name cannot be empty. Press any key to retry.");
            Console.ReadKey();
        } while (string.IsNullOrEmpty(user));

        Console.Clear();
        return user;
    }

    private static string Restart(ref string user)
    {
        Console.Clear();

        while (true)
        {
            Console.WriteLine("Do you want to reintroduce yourself? Press 'y' for yes or 'n' for no.");
            var key = Console.ReadKey();

            switch (key.KeyChar)
            {
                case 'y':
                case 'Y':
                    user = Start();
                    return user;
                case 'n':
                case 'N':
                    Console.Clear();
                    return user;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please press 'y' for yes or 'n' for no.");
                    Console.ResetColor();
                    break;
            }
        }
    }
    
    private static void AvailableOptions(string? user)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("Available options are: ");
        sb.AppendLine("/start - Starts or restarts a dialogue.");
        sb.AppendLine("/help - Display this help message.");
        if (!string.IsNullOrEmpty(user)) sb.AppendLine("/echo - Echoes a message.");
        sb.AppendLine("/info - Display the information about this application.");
        sb.AppendLine("/exit - Exit the program.");
        
        Console.Clear();
        Console.WriteLine(sb.ToString());
    }

    private static void Echo(string user, string echoMessage)
    {
        Console.Clear();

        if (string.IsNullOrEmpty(user))
        {
            Console.WriteLine("Please introduce yourself first. Execute /start command.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine($"Echo message: {echoMessage}");
        Console.WriteLine();
    }

    private static void BotInfo()
    {
        var version = "1.1";
        var releaseDate = "16.11.2024";
        var patchNotes = new List<string>
        {
            "Version 1.0 [16.11.2024] - Initial release.",
            "Version 1.1 [01.12.2024] - Refactoring and code cleanup."
        };

        Console.Clear();
        
        var sb = new StringBuilder();
        sb.AppendLine($"Version: {version}");
        sb.AppendLine($"Release Date: {releaseDate}");
        sb.AppendLine();
        sb.AppendLine("Patch Notes:");

        foreach (var note in patchNotes)
        {
            sb.AppendLine($"* {note}");
        }

        Console.WriteLine(sb.ToString());
    }

    private static void DefaultAction()
    {
        Console.Clear();
        Console.WriteLine("Not a valid command. Type /help for a list of options.");
        Console.WriteLine();
    }
}
