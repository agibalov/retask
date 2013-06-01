using Service.Infrastructure.Notifiers;
using Service.TransactionScripts.BL;

namespace ServiceTests.Stubs
{
    public class TestSignUpConfirmationNotifier : ISignUpConfirmationNotifier
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