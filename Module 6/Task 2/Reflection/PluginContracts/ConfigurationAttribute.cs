namespace PluginContracts
{
    public class ConfigurationAttribute : Attribute
    {
        public string SettingName { get; }
        public string ProviderName { get; }

        public ConfigurationAttribute(string settingName, string providerName)
        {
            SettingName = settingName;
            ProviderName = providerName;
        }
    }
}