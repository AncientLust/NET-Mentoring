using Reflection.Attributes;

namespace Reflection;

public class AppSettings : ConfigurationComponentBase
{
    [FileConfigurationItem("IntValueFromFile")]
    public int IntValueFromFile { get; set; }

    [FileConfigurationItem("StringValueFromFile")]
    public string? StringValueFromFile { get; set; }

    [FileConfigurationItem("FloatValueFromFile")]
    public float FloatValueFromFile { get; set; }

    [FileConfigurationItem("TimeSpanValueFromFile")]
    public TimeSpan TimeSpanValueFromFile { get; set; }

    [ConfigurationManagerItem("IntValueFromConfig")]
    public int IntValueFromConfig { get; set; }

    [ConfigurationManagerItem("StringValueFromConfig")]
    public string? StringValueFromConfig { get; set; }

    [ConfigurationManagerItem("FloatValueFromConfig")]
    public float FloatValueFromConfig { get; set; }

    [ConfigurationManagerItem("TimeSpanValueFromConfig")]
    public TimeSpan TimeSpanValueFromConfig { get; set; }

    public void PrintSettings(string prefixMessage, AppSettings settings)
    {
        Console.WriteLine($"{prefixMessage}");
        Console.WriteLine("\tFile attributes:");
        Console.WriteLine($"\t\tIntValueFromFile: {settings.IntValueFromFile}");
        Console.WriteLine($"\t\tStringValueFromFile: {settings.StringValueFromFile}");
        Console.WriteLine($"\t\tFloatValueFromFile: {settings.FloatValueFromFile}");
        Console.WriteLine($"\t\tTimeSpanValueFromFile: {settings.TimeSpanValueFromFile}");
        Console.WriteLine("\tConfig attributes:");
        Console.WriteLine($"\t\tIntValueFromConfig: {settings.IntValueFromConfig}");
        Console.WriteLine($"\t\tStringValueFromConfig: {settings.StringValueFromConfig}");
        Console.WriteLine($"\t\tFloatValueFromConfig: {settings.FloatValueFromConfig}");
        Console.WriteLine($"\t\tTimeSpanValueFromConfig: {settings.TimeSpanValueFromConfig}");
    }
}
