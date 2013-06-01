using System;
using NUnit.Framework;
using Ninject;
using Service;
using Service.Infrastructure.Notifiers;
using Service.TransactionScripts.BL;
using ServiceTests.Stubs;

namespace ServiceTests
{
    public abstract class AbstractTest
    {
        protected readonly RetaskService Service;
        protected readonly TestSignUpConfirmationNotifier SignUpConfirmationNotifier;
        protected readonly TestWelcomeNotifier WelcomeNotifier;
        protected readonly TestResetPasswordNotifier ResetPasswordNotifier;
        protected readonly TestTimeProvider TimeProvider;
        protected const int SessionTtlSeconds = 60;
        protected const int SessionTimeToExpire = SessionTtlSeconds + 2;

        protected AbstractTest()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ISignUpConfirmationNotifier, TestSignUpConfirmationNotifier>()
                .To<TestSignUpConfirmationNotifier>()
                .InSingletonScope();

            kernel.Bind<IWelcomeNotifier, TestWelcomeNotifier>()
                .To<TestWelcomeNotifier>()
                .InSingletonScope();

            kernel.Bind<IResetPasswordNotifier, TestResetPasswordNotifier>()
                .To<TestResetPasswordNotifier>()
                .InSingletonScope();

            kernel.Bind<ITimeProvider, TestTimeProvider>()
                .To<TestTimeProvider>()
                .InSingletonScope();

            kernel.Bind<AuthenticationService>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("sessionTtlSeconds", SessionTtlSeconds);

            Service = kernel.Get<RetaskService>();
            SignUpConfirmationNotifier = kernel.Get<TestSignUpConfirmationNotifier>();
            WelcomeNotifier = kernel.Get<TestWelcomeNotifier>();
            ResetPasswordNotifier = kernel.Get<TestResetPasswordNotifier>();
            TimeProvider = kernel.Get<TestTimeProvider>();
        }

        [SetUp]
        public void CleanUp()
        {
            Service.RebuildDatabase();
        }

        [SetUp]
        public void SetCurrentTime()
        {
            TimeProvider.SetCurrentTime(DateTime.UtcNow);
        }
    }
}