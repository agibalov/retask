namespace Service.Infrastructure.Notifiers
{
    public interface ISignUpConfirmationNotifier
    {
        void Notify(string email, string secret);
    }
}