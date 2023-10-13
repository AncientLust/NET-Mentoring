using Reflection;
using Reflection.Attributes;
using System.Reflection;

namespace FileConfigurationProvider;

public class FileConfigurationProvider : IConfigurationProvider
{
    public void Load(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<FileConfigurationItemAttribute>()
                is FileConfigurationItemAttribute fileAttr)
            {
                LoadAttributeFromFile(property, fileAttr, obj);
            }
        }
    }

    public void Save(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<FileConfigurationItemAttribute>()
                is FileConfigurationItemAttribute fileAttr)
            {
                SaveAttributeToFile(property, fileAttr, obj);
            }
        }
    }

    private void LoadAttributeFromFile(PropertyInfo property, FileConfigurationItemAttribute fileAttribute, object obj)
    {
        if (!File.Exists(fileAttribute.FilePath))
            throw new FileNotFoundException("Cannot find file: " + fileAttribute.FilePath);

        var jsonString = File.ReadAllText(fileAttribute.FilePath);
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

    private void SaveAttributeToFile(PropertyInfo property, FileConfigurationItemAttribute fileAttribute, object obj)
    {
        var value = property.GetValue(obj);
        string jsonString;
        if (File.Exists(fileAttribute.FilePath))
        {
            var existingJsonString = File.ReadAllText(fileAttribute.FilePath);
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

        File.WriteAllText(fileAttribute.FilePath, jsonString);
    }
}