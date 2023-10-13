using System.Configuration;
using System.Reflection;
using Reflection.Attributes;

namespace Reflection;

public abstract class ConfigurationComponentBase
{
    public void LoadSettings()
    {
        foreach (var property in GetType().GetProperties())
        {
            if (property.GetCustomAttribute<FileConfigurationItemAttribute>()
                is FileConfigurationItemAttribute fileAttr)
            {
                LoadAttributeFromFile(property, fileAttr);
            }
            else if (property.GetCustomAttribute<ConfigurationManagerItemAttribute>()
                is ConfigurationManagerItemAttribute configAttr)
            {
                LoadAttributeFromConfig(property, configAttr);
            }
        }
    }

    private void LoadAttributeFromFile(PropertyInfo property, FileConfigurationItemAttribute fileAttribute)
    {
        if (!File.Exists(fileAttribute.FilePath))
            throw new FileNotFoundException("Cannot find file: " + fileAttribute.FilePath);

        var jsonString = File.ReadAllText(fileAttribute.FilePath);
        var settingsDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

        if (settingsDict is not null && settingsDict.TryGetValue(fileAttribute.SettingName, out var settingValue))
        {
            if (property.PropertyType == typeof(TimeSpan))
            {
                property.SetValue(this, TimeSpan.Parse(settingValue as String ?? string.Empty));
            }
            else
            {
                property.SetValue(this, Convert.ChangeType(settingValue, property.PropertyType));
            }
        }
    }

    private void LoadAttributeFromConfig(PropertyInfo property, ConfigurationManagerItemAttribute configAttribute)
    {
        var settingValue = ConfigurationManager.AppSettings[configAttribute.SettingName];
        if (settingValue is not null)
        {
            if (property.PropertyType == typeof(TimeSpan))
            {
                property.SetValue(this, TimeSpan.Parse(settingValue));
            }
            else
            {
                property.SetValue(this, Convert.ChangeType(settingValue, property.PropertyType));
            }
        }
    }

    public void SaveSettings()
    {
        foreach (var property in GetType().GetProperties())
        {
            if (property.GetCustomAttribute<FileConfigurationItemAttribute>() is FileConfigurationItemAttribute fileAttr)
            {
                SaveAttributeToFile(property, fileAttr);
            }
            else if (property.GetCustomAttribute<ConfigurationManagerItemAttribute>() is ConfigurationManagerItemAttribute configAttr)
            {
                SaveAttributeToConfig(property, configAttr);
            }
        }
    }

    private void SaveAttributeToFile(PropertyInfo property, FileConfigurationItemAttribute fileAttribute)
    {
        var value = property.GetValue(this);
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

    private void SaveAttributeToConfig(PropertyInfo property, ConfigurationManagerItemAttribute configAttribute)
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (configuration.AppSettings.Settings[configAttribute.SettingName] is not null)
        {
            configuration.AppSettings.Settings[configAttribute.SettingName].Value = property.GetValue(this)?.ToString();
        }
        else
        {
            configuration.AppSettings.Settings.Add(configAttribute.SettingName, property.GetValue(this)?.ToString());
        }

        configuration.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }

}
