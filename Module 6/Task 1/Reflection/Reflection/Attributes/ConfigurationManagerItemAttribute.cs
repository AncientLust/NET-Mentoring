namespace Reflection.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationManagerItemAttribute : Attribute
{
    public string SettingName { get; }

    public ConfigurationManagerItemAttribute(string settingName)
    {
        SettingName = settingName;
    }
}
