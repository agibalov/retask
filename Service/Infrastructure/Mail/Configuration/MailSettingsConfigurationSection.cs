using System.Configuration;

namespace Service.Infrastructure.Mail.Configuration
{
    public class MailSettingsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("senders")]
        public SenderConfigurationCollection Senders
        {
            get { return (SenderConfigurationCollection) base["senders"]; }
        }
    }
}