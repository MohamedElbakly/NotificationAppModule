using NotificationApp.Interfaces;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using FirebaseAdmin.Messaging;
using NotificationApp.DTO;
using Microsoft.Extensions.Options;

namespace NotificationApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging _firebaseMessaging;
        private readonly MailConfiguration _mailConfiguration;
        private readonly SMSConfiguration _smsConfiguration;
        public NotificationService(FirebaseMessaging firebaseMessaging, IOptions<MailConfiguration> mailConfiguration, IOptions<SMSConfiguration> smsConfiguration)
        {
            _firebaseMessaging = firebaseMessaging;
            _mailConfiguration = mailConfiguration.Value;
            _smsConfiguration = smsConfiguration.Value;
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

        public async Task<bool> SendPushNotification(string deviceId, string message)
        {
            var messageObj = new Message
            {
                Token = deviceId,
                Notification = new Notification
                {
                    Title = "Push Notification",
                    Body = message
                }
            };

            try
            {
                var response = _firebaseMessaging.SendAsync(messageObj).GetAwaiter().GetResult();
                return true;
            }
            catch (FirebaseMessagingException ex)
            {
                return false;
            }
        }

        public async Task<bool> SendSMS(string to, string message)
        {
            TwilioClient.Init(_smsConfiguration.accountSid, _smsConfiguration.authToken);

            var res = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(_smsConfiguration.SenderPhone),
                to: new Twilio.Types.PhoneNumber(to)
            );

            return (!string.IsNullOrEmpty(res.Sid) ? true : false);
        }
    }
}
