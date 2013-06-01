using System.Configuration;

namespace Service.Infrastructure.Mail.Configuration
{
    [ConfigurationCollection(typeof(SenderConfigurationElement))]
    public class SenderConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SenderConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SenderConfigurationElement)element).Name;
        }
    }
}