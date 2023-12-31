using NotificationApp.Interfaces;
using System.Net.Mail;
using System.Net;
using NotificationApp.DTO;
using Microsoft.Extensions.Options;

namespace NotificationApp.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly MailConfiguration _mailConfiguration;
        public EmailNotificationService(IOptions<MailConfiguration> mailConfiguration)
        {
            _mailConfiguration = mailConfiguration.Value;
        }
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(_mailConfiguration.SMTP)
            {
                Port = _mailConfiguration.Port,
                Credentials = new NetworkCredential(_mailConfiguration.SenderEmail, _mailConfiguration.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailConfiguration.SenderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
