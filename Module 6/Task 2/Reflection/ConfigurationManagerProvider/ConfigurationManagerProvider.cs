using Reflection;
using Reflection.Attributes;
using System.Reflection;
using System.Configuration;

namespace ConfigurationManagerProvider;

public class ConfigurationManagerProvider : IConfigurationProvider
{
    public void Load(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<ConfigurationManagerItemAttribute>()
                is ConfigurationManagerItemAttribute configAttr)
            {
                LoadAttributeFromConfig(property, configAttr, obj);
            }
        }
    }

    public void Save(object obj)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetCustomAttribute<ConfigurationManagerItemAttribute>() is ConfigurationManagerItemAttribute configAttr)
            {
                SaveAttributeToConfig(property, configAttr, obj);
            }
        }
    }

    private void LoadAttributeFromConfig(PropertyInfo property, ConfigurationManagerItemAttribute configAttribute, object obj)
    {
        var settingValue = ConfigurationManager.AppSettings[configAttribute.SettingName];
        if (settingValue is not null)
        {
            if (property.PropertyType == typeof(TimeSpan))
            {
                property.SetValue(obj, TimeSpan.Parse(settingValue));
            }
            else
            {
                property.SetValue(obj, Convert.ChangeType(settingValue, property.PropertyType));
            }
        }
    }

    private void SaveAttributeToConfig(PropertyInfo property, ConfigurationManagerItemAttribute configAttribute, object obj)
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (configuration.AppSettings.Settings[configAttribute.SettingName] is not null)
        {
            configuration.AppSettings.Settings[configAttribute.SettingName].Value = property.GetValue(obj)?.ToString();
        }
        else
        {
            configuration.AppSettings.Settings.Add(configAttribute.SettingName, property.GetValue(obj)?.ToString());
        }

        configuration.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}