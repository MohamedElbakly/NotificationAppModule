using NotificationApp.Interfaces;
using NotificationApp.Services;

namespace NotificationApp.Managers
{
    public class SMSNotificationManager
    {
        private readonly ISMSNotificationService _smsNotificationService;
        public SMSNotificationManager(ISMSNotificationService smsNotificationService)
        {
            _smsNotificationService = smsNotificationService;
        }

        public async Task<bool> SendSMSNotification(string to, string message)
        {
            bool sent = await _smsNotificationService.SendSMS(to, message);

            return sent;
        }
    }
}
