namespace Reflection.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FileConfigurationItemAttribute : Attribute
{
    public string SettingName { get; }
    public string FilePath { get; } = @"ConfigFile.json";

    public FileConfigurationItemAttribute(string settingName)
    {
        SettingName = settingName;
    }
}
