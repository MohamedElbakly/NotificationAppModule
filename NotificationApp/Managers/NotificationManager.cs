using NotificationApp.Interfaces;

namespace NotificationApp.Managers
{
    public class NotificationManager
    {
        private readonly INotificationService _notificationService;

        public NotificationManager(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> SendNotification(NotificationType type, string to, string message)
        {
            bool sent = false;
            switch (type)
            {
                case NotificationType.Email:
                    sent = await _notificationService.SendEmailAsync(to, "Notification", message);
                    break;
                case NotificationType.SMS:
                    sent = await _notificationService.SendSMS(to, message);
                    break;
                case NotificationType.Push:
                    sent = await _notificationService.SendPushNotification(to, message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return sent;
        }
    }

    public enum NotificationType
    {
        Email,
        SMS,
        Push
    }

}
