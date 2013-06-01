namespace Service.Infrastructure.Mail
{
    public class NullEmailSender : IEmailSender
    {
        public void SendEmail(string to, string subject, string htmlBody)
        {
            // Yep, does nothing
        }
    }
}