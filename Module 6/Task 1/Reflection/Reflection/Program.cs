namespace Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var settings = new AppSettings();
        settings.IntValueFromFile = 123;
        settings.StringValueFromFile = "File";
        settings.FloatValueFromFile = 123.4f;
        settings.TimeSpanValueFromFile = new TimeSpan(1, 2, 3);

        settings.IntValueFromConfig = 321;
        settings.StringValueFromConfig = "Config";
        settings.FloatValueFromConfig = 432.1f;
        settings.TimeSpanValueFromConfig = new TimeSpan(3, 2, 1);

        settings.SaveSettings();
        settings.PrintSettings("Saved with values", settings);

        settings = new AppSettings();
        settings.LoadSettings();
        settings.PrintSettings("\nLoaded values", settings);
    }
}