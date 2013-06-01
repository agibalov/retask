namespace Service.Infrastructure.Mail
{
    public interface IEmailSender
    {
        void SendEmail(string to, string subject, string htmlBody);
    }
}