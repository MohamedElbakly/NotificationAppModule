using NotificationApp.Interfaces;
using NotificationApp.Services;

namespace NotificationApp.Managers
{
    public class PushNotificationManager
    {
        private readonly IPushNotificationService _pushNotificationService;
        public PushNotificationManager(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public async Task<bool> SendPushNotification(string to, string message)
        {
            bool sent = await _pushNotificationService.SendPushNotification(to, message);

            return sent;
        }
    }
}
