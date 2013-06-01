using System;
using System.Configuration;

namespace Service.Infrastructure.Mail.Configuration
{
    public class SenderConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("host")]
        public string Host
        {
            get { return (string)base["host"]; }
            set { base["host"] = value; }
        }

        [ConfigurationProperty("port")]
        public int Port
        {
            get { return Convert.ToInt32(base["port"]); }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("login")]
        public string Login
        {
            get { return (string)base["login"]; }
            set { base["login"] = value; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("ssl")]
        public bool Ssl
        {
            get { return Convert.ToBoolean(base["ssl"]); }
            set { base["ssl"] = value; }
        }

        [ConfigurationProperty("from")]
        public string From
        {
            get { return (string)base["from"]; }
            set { base["from"] = value; }
        }
    }
}