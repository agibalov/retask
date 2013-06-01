using Service.Infrastructure.Notifiers;

namespace ServiceTests.Stubs
{
    public class TestResetPasswordNotifier : IResetPasswordNotifier
    {
        public string LastEmail { get; private set; }
        public string LastSecret { get; private set; }

        public void Notify(string email, string secret)
        {
            LastEmail = email;
            LastSecret = secret;
        }

        public void Reset()
        {
            LastEmail = null;
            LastSecret = null;
        }
    }
}