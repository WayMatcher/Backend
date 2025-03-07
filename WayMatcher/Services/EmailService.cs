using System.Net;
using System.Net.Mail;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;

namespace WayMatcherBL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailServerDto _emailServer;

        public EmailService(EmailServerDto emailServerDto)
        {
            _emailServer = emailServerDto;
            _smtpClient = new SmtpClient(_emailServer.Host)
            {
                Port = _emailServer.Port,
                Credentials = new NetworkCredential(_emailServer.Username, _emailServer.Password),
                EnableSsl = true,
            };
        }

        public void SendEmail(EmailDto email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailServer.Username),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.IsHtml,
            };

            mailMessage.To.Add(email.To);

            _smtpClient.Send(mailMessage);
        }
    }
}
