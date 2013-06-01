namespace Service.Infrastructure.Notifiers
{
    public interface IWelcomeNotifier
    {
        void Notify(string email);
    }
}