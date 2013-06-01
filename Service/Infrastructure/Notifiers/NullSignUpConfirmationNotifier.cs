namespace Service.Infrastructure.Notifiers
{
    public class NullSignUpConfirmationNotifier : ISignUpConfirmationNotifier
    {
        public void Notify(string email, string secret)
        {
            // Yep, does nothing
        }
    }
}