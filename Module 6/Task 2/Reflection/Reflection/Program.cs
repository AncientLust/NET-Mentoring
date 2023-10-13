using System.Reflection;

namespace Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var fileConfigurationProvider = LoadConfigurationProvider("Plugins/FileConfigurationProvider.dll");
        var configurationManagerProvider = LoadConfigurationProvider("Plugins/ConfigurationManagerProvider.dll");

        if (fileConfigurationProvider is not null && configurationManagerProvider is not null) 
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

            fileConfigurationProvider.Save(settings);
            configurationManagerProvider.Save(settings);
            settings.PrintSettings("Saved with values", settings);

            settings = new AppSettings();

            fileConfigurationProvider.Load(settings);
            configurationManagerProvider.Load(settings);
            settings.PrintSettings("\nLoaded values", settings);
        }
    }

    private static IConfigurationProvider? LoadConfigurationProvider(string assemblyPath)
    {
        var assembly = Assembly.LoadFrom(assemblyPath);
        var providerType = assembly.GetTypes().First(t => t.GetInterface(nameof(IConfigurationProvider)) != null);

        return Activator.CreateInstance(providerType) as IConfigurationProvider; ;
    }
}