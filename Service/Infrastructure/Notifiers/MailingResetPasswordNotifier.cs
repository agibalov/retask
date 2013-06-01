using Service.Infrastructure.Mail;
using Service.Properties;

namespace Service.Infrastructure.Notifiers
{
    public class MailingResetPasswordNotifier : IResetPasswordNotifier
    {
        private readonly IEmailSender _emailSender;
        private readonly RazorTemplateRenderer _razorTemplateRenderer;
        private readonly string _resetPasswordUrlTemplate;

        public MailingResetPasswordNotifier(
            IEmailSender emailSender, 
            RazorTemplateRenderer razorTemplateRenderer,
            string resetPasswordUrlTemplate)
        {
            _emailSender = emailSender;
            _razorTemplateRenderer = razorTemplateRenderer;
            _resetPasswordUrlTemplate = resetPasswordUrlTemplate;
        }

        public void Notify(string email, string secret)
        {
            var htmlBody = _razorTemplateRenderer.Render(
                Resources.ResetPasswordEmailTemplate, 
                new {
                    Email = email,
                    PasswordResetUrl = string.Format(_resetPasswordUrlTemplate, secret)
                });
            _emailSender.SendEmail(
                email, 
                "Password reset", 
                htmlBody);
        }
    }
}