using System.Text;

namespace Otus_HomeWork_1;

internal class Program
{
    public static void Main(string[] args)
    {
        var work = true;
        var user = string.Empty;
        var echoMessage = string.Empty;
        var taskList = new List<string>();

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
                case "/addtask":
                    AddTask(taskList, user);
                    break;
                case "/showtasks":
                    ShowTasks(taskList, user);
                    break;
                case "/removetask":
                    RemoveTask(taskList, user);
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
        var user = InputAndValidate("Please enter your name:", "Name");
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
                    HandleInvalidRestartInput();
                    break;
            }
        }
    }

    private static void HandleInvalidRestartInput()
    {
        Console.Clear();
        ShowAsErrorMessage("Invalid input. Please press 'y' for yes or 'n' for no.");
    }

    private static void AvailableOptions(string? user)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Available options are: ");
        sb.AppendLine().AppendLine("Common:").AppendLine();
        sb.AppendLine("/start - Starts or restarts a dialogue.");
        sb.AppendLine("/help - Display this help message.");
        if (!string.IsNullOrEmpty(user)) sb.AppendLine("/echo - Echoes a message.");
        sb.AppendLine("/info - Display the information about this application.");
        sb.AppendLine("/exit - Exit the program.");

        if (!string.IsNullOrEmpty(user))
        {
            sb.AppendLine().AppendLine("Tasks:").AppendLine();
            sb.AppendLine("/addtask - Adds a task to the list.");
            sb.AppendLine("/showtasks - Displays the task list.");
            sb.AppendLine("/removetask - Removes the task from the list.");
            sb.AppendLine();
        }

        Console.Clear();
        Console.WriteLine(sb.ToString());
    }

    private static void Echo(string user, string echoMessage)
    {
        Console.Clear();

        if (!CheckUserIsSet(user)) return;

        Console.WriteLine($"Echo message: {echoMessage}");
        Console.WriteLine();
    }

    private static void BotInfo()
    {
        var releaseDate = "16.11.2024";
        var releaseNotes = new Dictionary<string, string>
        {
            { "1.0", "[16.11.2024] - Initial release." },
            { "1.1", "[01.12.2024] - Refactoring and code cleanup." },
            { "1.2", "[01.12.2024] - Minor cleanup in Restart logic." },
            { "2.0", "[01.12.2024] - Implement task creation, display, and removal." },
            { "2.1", "[01.12.2024] - Remove commented-out code." },
            { "2.2", "[01.12.2024] - Add human-readable task indices and cancellation option." },
            { "2.3", "[01.12.2024] - Display task name on removal and update patch notes." }
        };
        var version = releaseNotes.Last().Key;

        Console.Clear();

        var sb = new StringBuilder();
        sb.AppendLine($"Current version: {version}");
        sb.AppendLine($"Release Date: {releaseDate}");
        sb.AppendLine();
        sb.AppendLine("Patch Notes:");

        foreach (var note in releaseNotes.Reverse())
        {
            sb.AppendLine($"* Version {note.Key} {note.Value}");
        }

        Console.WriteLine(sb.ToString());
    }

    private static void DefaultAction()
    {
        Console.Clear();
        ShowAsErrorMessage("Not a valid command. Type /help for a list of options.");
        Console.WriteLine();
    }

    private static void AddTask(List<string> taskList, string user)
    {
        Console.Clear();

        if (!CheckUserIsSet(user)) return;

        var input = InputAndValidate("Please enter a task text", "Task text");
        taskList.Add(input);

        Console.Clear();
        Console.WriteLine($"New task \"{input}\" has been added.");
        Console.WriteLine();
    }

    private static void ShowTasks(List<string> taskList, string user)
    {
        Console.Clear();

        if (!CheckUserIsSet(user)) return;

        if (taskList.Count == 0)
        {
            ShowAsErrorMessage("No tasks have been added.");
            Console.WriteLine();
            return;
        }

        var sb = new StringBuilder();

        sb.AppendLine("Current tasks:").AppendLine();
        for (var index = 0; index < taskList.Count; index++)
        {
            sb.AppendLine($"{index + 1} - {taskList[index]}");
        }

        Console.WriteLine(sb.ToString());
    }

    private static void RemoveTask(List<string> taskList, string user)
    {
        Console.Clear();

        if (!CheckUserIsSet(user)) return;

        if (taskList.Count == 0)
        {
            ShowAsErrorMessage("No tasks have been added.");
            Console.WriteLine();
            return;
        }

        var sb = new StringBuilder();

        sb.AppendLine("Current tasks:").AppendLine();
        for (var index = 0; index < taskList.Count; index++)
        {
            sb.AppendLine($"[{index + 1}] - {taskList[index]}");
        }

        sb.AppendLine().AppendLine("Enter task ID to remove or 0 to cancel:");

        var taskId = TaskIdInputAndValidate(taskList.Count, sb.ToString());
        
        var taskName = taskList[taskId - 1];

        if (taskId == 0)
        {
            Console.WriteLine("Action cancelled.");
            Console.WriteLine();
            return;
        }

        taskList.RemoveAt(taskId - 1);

        Console.WriteLine($"Task \"{taskName}\" has been removed.");
        Console.WriteLine();
    }

    private static int TaskIdInputAndValidate(int taskCount, string message)
    {
        while (true)
        {
            var input = InputAndValidate(message, "Id value");

            if (int.TryParse(input, out var result))
            {
                if (result >= 0 && result <= taskCount && input.Length == result.ToString().Length) return result;
            }

            ShowAsErrorMessage("Invalid task ID. Please enter a valid number within the range. Press any key to retry.");
            Console.ReadKey();
        }
    }

    private static string InputAndValidate(string message, string entity)
    {
        string input;

        do
        {
            Console.Clear();
            Console.WriteLine(message);
            input = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(input)) continue;

            ShowAsErrorMessage($"{entity} cannot be empty. Press any key to retry.");
            Console.ReadKey();
        } while (string.IsNullOrEmpty(input));

        Console.Clear();

        return input;
    }

    private static bool CheckUserIsSet(string user)
    {
        if (!string.IsNullOrEmpty(user)) return true;

        ShowAsErrorMessage("Please introduce yourself first. Execute /start command.");
        Console.WriteLine();
        return false;
    }

    private static void ShowAsErrorMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}