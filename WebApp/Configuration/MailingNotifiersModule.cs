using Ninject.Modules;
using Service.Infrastructure.Notifiers;

namespace WebApp.Configuration
{
    public class MailingNotifiersModule : NinjectModule
    {
        private readonly string _rootUrl;
        private readonly string _signUpConfirmationUrlTemplate;
        private readonly string _resetPasswordUrlTemplate;

        public MailingNotifiersModule(
            string rootUrl, 
            string signUpConfirmationUrlTemplate,
            string resetPasswordUrlTemplate)
        {
            _rootUrl = rootUrl;
            _signUpConfirmationUrlTemplate = signUpConfirmationUrlTemplate;
            _resetPasswordUrlTemplate = resetPasswordUrlTemplate;
        }

        public override void Load()
        {
            Bind<ISignUpConfirmationNotifier, MailingSignUpConfirmationNotifier>()
                .To<MailingSignUpConfirmationNotifier>()
                .InSingletonScope()
                .WithConstructorArgument("confirmationUrlTemplate", _signUpConfirmationUrlTemplate);

            Bind<IWelcomeNotifier, MailingWelcomeNotifier>()
                .To<MailingWelcomeNotifier>()
                .InSingletonScope()
                .WithConstructorArgument("rootUrl", _rootUrl);

            Bind<IResetPasswordNotifier, MailingResetPasswordNotifier>()
                .To<MailingResetPasswordNotifier>()
                .InSingletonScope()
                .WithConstructorArgument("resetPasswordUrlTemplate", _resetPasswordUrlTemplate);
        }
    }
}