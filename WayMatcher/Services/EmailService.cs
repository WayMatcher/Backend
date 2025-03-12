using System.Net;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace WayMatcherBL.Services
{
    public class EmailService : IEmailService, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailServerDto _emailServer;

        public EmailService(EmailServerDto emailServerDto)
        {
            _emailServer = emailServerDto;
            _smtpClient = new SmtpClient();
            _smtpClient.Connect(_emailServer.Host, _emailServer.Port, MailKit.Security.SecureSocketOptions.StartTls);
            _smtpClient.Authenticate(_emailServer.Username, _emailServer.Password);
        }

        public void SendEmail(EmailDto email)
        {
            var mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress("WayMatcher", "NoReply@hobedere.com"));
            mailMessage.To.Add(new MailboxAddress(email.Username, email.To));
            mailMessage.Subject = email.Subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = email.Body
            };

            _smtpClient.Send(mailMessage);
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
