using PluginContracts;

namespace Reflection;

public class AppSettings
{
    [Configuration("IntValueFromFile", "FileConfigurationProvider")]
    public int IntValueFromFile { get; set; }

    [Configuration("StringValueFromFile", "FileConfigurationProvider")]
    public string? StringValueFromFile { get; set; }

    [Configuration("FloatValueFromFile", "FileConfigurationProvider")]
    public float FloatValueFromFile { get; set; }

    [Configuration("TimeSpanValueFromFile", "FileConfigurationProvider")]
    public TimeSpan TimeSpanValueFromFile { get; set; }

    [Configuration("IntValueFromConfig", "ConfigurationManagerProvider")]
    public int IntValueFromConfig { get; set; }

    [Configuration("StringValueFromConfig", "ConfigurationManagerProvider")]
    public string? StringValueFromConfig { get; set; }

    [Configuration("FloatValueFromConfig", "ConfigurationManagerProvider")]
    public float FloatValueFromConfig { get; set; }

    [Configuration("TimeSpanValueFromConfig", "ConfigurationManagerProvider")]
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
        Console.WriteLine($"\t\tRefreshIntervalConfig: {settings.TimeSpanValueFromConfig}");
    }
}
