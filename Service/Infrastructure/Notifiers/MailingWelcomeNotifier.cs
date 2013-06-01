using Service.Infrastructure.Mail;
using Service.Properties;

namespace Service.Infrastructure.Notifiers
{
    public class MailingWelcomeNotifier : IWelcomeNotifier
    {
        private readonly IEmailSender _emailSender;
        private readonly RazorTemplateRenderer _razorTemplateRenderer;
        private readonly string _rootUrl;

        public MailingWelcomeNotifier(
            IEmailSender emailSender, 
            RazorTemplateRenderer razorTemplateRenderer,
            string rootUrl)
        {
            _emailSender = emailSender;
            _razorTemplateRenderer = razorTemplateRenderer;
            _rootUrl = rootUrl;
        }

        public void Notify(string email)
        {
            var htmlBody = _razorTemplateRenderer.Render(
                Resources.WelcomeEmailTemplate, 
                new {
                    Email = email,
                    RootUrl = _rootUrl
                });
            _emailSender.SendEmail(
                email, 
                "Welcome!", 
                htmlBody);
        }
    }
}