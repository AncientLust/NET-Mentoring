namespace UtilsLibrary
{
    public class Utils
    {
        public static string ValidationFailMessage { get; } = "Provided user name is invalid. Name cannot contain spaces or non-alpha symbols!";

        public static string GetGreeting(string userName)
        {
            return $"{DateTime.Now:HH:mm:ss} Hello, {userName}!";
        }

        public static bool ValidateName(string userName)
        {
            return userName.All(char.IsLetter);
        }
    }
}