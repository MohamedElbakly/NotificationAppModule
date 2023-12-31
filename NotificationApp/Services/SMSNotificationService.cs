using Microsoft.Extensions.Options;
using NotificationApp.DTO;
using NotificationApp.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace NotificationApp.Services
{
    public class SMSNotificationService : ISMSNotificationService
    {
        private readonly SMSConfiguration _smsConfiguration;
        public SMSNotificationService(IOptions<SMSConfiguration> smsConfiguration)
        {
            _smsConfiguration = smsConfiguration.Value;
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
