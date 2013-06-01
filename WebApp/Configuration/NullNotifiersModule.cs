using Ninject.Modules;
using Service.Infrastructure.Notifiers;

namespace WebApp.Configuration
{
    public class NullNotifiersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISignUpConfirmationNotifier, NullSignUpConfirmationNotifier>()
                .To<NullSignUpConfirmationNotifier>()
                .InSingletonScope();

            Bind<IWelcomeNotifier, NullWelcomeNotifier>()
                .To<NullWelcomeNotifier>()
                .InSingletonScope();

            Bind<IResetPasswordNotifier, NullResetPasswordNotifier>()
                .To<NullResetPasswordNotifier>()
                .InSingletonScope();
        }
    }
}