using Service.Infrastructure.Mail;
using Service.Properties;

namespace Service.Infrastructure.Notifiers
{
    public class MailingSignUpConfirmationNotifier : ISignUpConfirmationNotifier
    {
        private readonly IEmailSender _emailSender;
        private readonly RazorTemplateRenderer _razorTemplateRenderer;
        private readonly string _confirmationUrlTemplate;

        public MailingSignUpConfirmationNotifier(
            IEmailSender emailSender, 
            RazorTemplateRenderer razorTemplateRenderer,
            string confirmationUrlTemplate)
        {
            _emailSender = emailSender;
            _razorTemplateRenderer = razorTemplateRenderer;
            _confirmationUrlTemplate = confirmationUrlTemplate;
        }

        public void Notify(string email, string secret)
        {
            var htmlBody = _razorTemplateRenderer.Render(
                Resources.SignUpConfirmationEmailTemplate, 
                new { 
                    Email = email,
                    ConfirmationUrl = string.Format(_confirmationUrlTemplate, secret)
                });

            _emailSender.SendEmail(
                email, 
                "Please activate your account", 
                htmlBody);
        }
    }
}