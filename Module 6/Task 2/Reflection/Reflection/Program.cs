using System.Reflection;
using PluginContracts;

namespace Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var providers = LoadConfigurationProvider().ToList();
        
        var settings = new AppSettings
        {
            IntValueFromFile = 123,
            StringValueFromFile = "File",
            FloatValueFromFile = 123.4f,
            TimeSpanValueFromFile = new TimeSpan(1, 2, 3),
            IntValueFromConfig = 321,
            StringValueFromConfig = "Config",
            FloatValueFromConfig = 432.1f,
            TimeSpanValueFromConfig = new TimeSpan(3, 2, 1)
        };

        providers.ForEach(provider => provider.Save(settings));
        settings.PrintSettings("Saved with values", settings);

        settings = new AppSettings();
        providers.ForEach(provider => provider.Load(settings));
        settings.PrintSettings("\nLoaded values", settings);
    }

    private static IEnumerable<IConfigurationProvider> LoadConfigurationProvider()
    {
        var buildPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
        ArgumentNullException.ThrowIfNull(buildPath);
        
        var pluginsPath = Path.Combine(buildPath, "Plugins");
        var files = Directory.GetFiles(pluginsPath);

        foreach (var file in files)
        {
            var assembly = Assembly.LoadFrom(file);
            var providerTypes = assembly.GetTypes();

            foreach (var type in providerTypes)
            {
                if (type.IsAssignableTo(typeof(IConfigurationProvider)) && 
                    Activator.CreateInstance(type) is IConfigurationProvider configProviderInstance)
                {
                    yield return configProviderInstance;
                }
            }
        }
    }
}