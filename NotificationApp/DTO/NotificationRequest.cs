using NotificationApp.Managers;

namespace NotificationApp.DTO
{
    public class NotificationRequest
    {
        public NotificationType Type { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}
