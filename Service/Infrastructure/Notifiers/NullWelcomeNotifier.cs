namespace Service.Infrastructure.Notifiers
{
    public class NullWelcomeNotifier : IWelcomeNotifier
    {
        public void Notify(string email)
        {
            // Yep, does nothing
        }
    }
}