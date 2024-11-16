// See https://aka.ms/new-console-template for more information

internal class Program
{
    public static void Main(string[] args)
    {
        var work = true;
        string? user = null;
        string? echoMessage = null;

        Console.WriteLine("Welcome to home work bot.");
        Console.WriteLine("Available options are: /start, /help, /info, /exit.");
        Console.WriteLine();

        while (work)
        {
            if (user != null) Console.WriteLine($"Hey, {user}.");
            Console.WriteLine("Enter your command:");

            var input = Console.ReadLine();

            if (input != null && input.StartsWith("/echo") && input.IndexOf(" ") != -1)
            {
                echoMessage = input.Substring(input.IndexOf(" ") + 1);
                input = "/echo";
            }

            switch (input)
            {
                case "/start":
                    if (user != null)
                    {
                        Console.Clear();

                        var correctInputFlag = false;
                        var exit = false;

                        while (correctInputFlag == false)
                        {
                            Console.WriteLine("Do you want to reintroduce yourself? Press 'y' for yes or 'n' for no.");
                            var key = Console.ReadKey();

                            if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                            {
                                user = null;
                                Console.Clear();
                                correctInputFlag = true;
                            }
                            else if (key.KeyChar == 'n' || key.KeyChar == 'N')
                            {
                                Console.Clear();
                                correctInputFlag = true;
                                exit = true;
                            }
                        }
                        
                        if (exit) break;
                    }

                    while (user == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter your name:");
                        user = Console.ReadLine();
                        Console.Clear();
                    }

                    break;
                case "/help":
                    Console.Clear();
                    AvailableOptions(user);
                    break;
                case "/echo":
                    Console.Clear();

                    if (user == null)
                    {
                        Console.WriteLine("Please introduce yourself first. Execute /start command.");
                        Console.WriteLine();
                        break;
                    }

                    Console.WriteLine($"Echo message: {echoMessage}");
                    Console.WriteLine();
                    break;
                case "/info":
                    Console.Clear();
                    Console.WriteLine("Version 1.0");
                    Console.WriteLine("16.11.2024");
                    Console.WriteLine();
                    break;
                case "/exit":
                    work = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Not a valid command. Type /help for a list of options.");
                    Console.WriteLine();
                    break;
            }
        }
    }

    private static void AvailableOptions(string? user)
    {
        Console.WriteLine("Available options are:");
        Console.WriteLine("/start - Starts or restarts a dialogue.");
        Console.WriteLine("/help - Display this help message.");
        if (user != null) Console.WriteLine("/echo - Echoes a message.");
        Console.WriteLine("/info - Display the information about this application.");
        Console.WriteLine("/exit - Exit the program.");
        Console.WriteLine();
    }
}