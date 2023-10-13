using UtilsLibrary;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("This application requires one command line argument: User name.");
            return;
        }

        string userName = args[0];
        if (Utils.ValidateName(userName))
        {
            Console.WriteLine(Utils.GetGreeting(userName));
        }
        else
        {
            Console.WriteLine(Utils.ValidationFailMessage);
        }
    }
}