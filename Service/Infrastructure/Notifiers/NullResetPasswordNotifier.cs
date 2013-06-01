namespace Service.Infrastructure.Notifiers
{
    public class NullResetPasswordNotifier : IResetPasswordNotifier
    {
        public void Notify(string email, string secret)
        {
            // Yep, does nothing
        }
    }
}