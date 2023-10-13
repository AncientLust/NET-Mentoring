using PluginContracts;
using System.Reflection;

namespace FileConfigurationProvider;

public class FileConfigurationProvider : IConfigurationProvider
{
    private const string FilePath = @"ConfigFile.json";

    public void Load(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<ConfigurationAttribute>() is { } configAttr &&
                configAttr.ProviderName == nameof(FileConfigurationProvider))
            {
                LoadAttributeFromFile(property, configAttr, obj);
            }
        }
    }

    public void Save(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<ConfigurationAttribute>() is { } configAttr &&
                configAttr.ProviderName == nameof(FileConfigurationProvider))
            {
                SaveAttributeToFile(property, configAttr, obj);
            }
        }
    }

    private void LoadAttributeFromFile(PropertyInfo property, ConfigurationAttribute fileAttribute, object obj)
    {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException("Cannot find file: " + FilePath);

        var jsonString = File.ReadAllText(FilePath);
        var settingsDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

        if (settingsDict is not null && settingsDict.TryGetValue(fileAttribute.SettingName, out var settingValue))
        {
            if (property.PropertyType == typeof(TimeSpan))
            {
                property.SetValue(obj, TimeSpan.Parse(settingValue as String ?? string.Empty));
            }
            else
            {
                property.SetValue(obj, Convert.ChangeType(settingValue, property.PropertyType));
            }
        }
    }

    private void SaveAttributeToFile(PropertyInfo property, ConfigurationAttribute fileAttribute, object obj)
    {
        var value = property.GetValue(obj);
        string jsonString;
        if (File.Exists(FilePath))
        {
            var existingJsonString = File.ReadAllText(FilePath);
            var existingSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(existingJsonString);

            if (existingSettings is not null)
            {
                existingSettings[fileAttribute.SettingName] = value!;
            }

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(existingSettings);
        }
        else
        {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                { fileAttribute.SettingName, value! }
            });
        }

        File.WriteAllText(FilePath, jsonString);
    }
}