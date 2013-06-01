namespace Service.Infrastructure.Notifiers
{
    public interface IResetPasswordNotifier
    {
        void Notify(string email, string secret);
    }
}