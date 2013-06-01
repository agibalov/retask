using System.Linq;
using Ninject.Modules;
using Service.Infrastructure.Mail;
using Service.Infrastructure.Mail.Configuration;
using Service.Infrastructure.Notifiers;

namespace WebApp.Configuration
{
    public class MailModule : NinjectModule
    {
        private readonly MailSettingsConfigurationSection _mailSettingsConfigurationSection;

        public MailModule(MailSettingsConfigurationSection mailSettingsConfigurationSection)
        {
            _mailSettingsConfigurationSection = mailSettingsConfigurationSection;
        }

        public override void Load()
        {
            Configure<MailingSignUpConfirmationNotifier>("SignUpConfirmationEmailSender");
            Configure<MailingWelcomeNotifier>("WelcomeEmailSender");
            Configure<MailingResetPasswordNotifier>("ResetPasswordEmailSender");
        }

        private void Configure<TEmailSenderUser>(string emailSenderName)
            where TEmailSenderUser : class
        {
            var emailSenderConfiguration = _mailSettingsConfigurationSection
                .Senders
                .OfType<SenderConfigurationElement>()
                .Single(sender => sender.Name == emailSenderName);

            Bind<IEmailSender>()
                .To<EmailSender>()
                .WhenInjectedInto<TEmailSenderUser>()
                .InSingletonScope()
                .WithConstructorArgument("host", emailSenderConfiguration.Host)
                .WithConstructorArgument("port", emailSenderConfiguration.Port)
                .WithConstructorArgument("login", emailSenderConfiguration.Login)
                .WithConstructorArgument("password", emailSenderConfiguration.Password)
                .WithConstructorArgument("ssl", emailSenderConfiguration.Ssl)
                .WithConstructorArgument("from", emailSenderConfiguration.From);
        }
    }
}