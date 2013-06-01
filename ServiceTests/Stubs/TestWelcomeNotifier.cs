using NUnit.Framework;
using Service.Infrastructure.Notifiers;

namespace ServiceTests.Stubs
{
    public class TestWelcomeNotifier : IWelcomeNotifier
    {
        public string LastEmail { get; private set; }

        public void Notify(string email)
        {
            LastEmail = email;
        }

        public void Reset()
        {
            LastEmail = null;
        }
    }
}