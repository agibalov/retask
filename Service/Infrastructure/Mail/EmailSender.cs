using System.Net;
using System.Net.Mail;

namespace Service.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly string _login;
        private readonly string _passowrd;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _from;

        public EmailSender(
            string host, 
            string login, 
            string password, 
            int port, 
            bool ssl, 
            string from)
        {
            _host = host;
            _login = login;
            _passowrd = password;
            _port = port;
            _ssl = ssl;
            _from = from;
        }

        public void SendEmail(string to, string subject, string htmlBody)
        {
            var smtpClient = new SmtpClient
                {
                    Host = _host,
                    Credentials = new NetworkCredential(_login, _passowrd),
                    EnableSsl = _ssl,
                    Port = _port
                };

            var mailMessage = new MailMessage
                {
                    From = new MailAddress(_from),
                    Subject = subject,
                    IsBodyHtml = true
                };

            mailMessage.To.Add(to);
            mailMessage.Body = htmlBody;
            smtpClient.Send(mailMessage);
        }
    }
}