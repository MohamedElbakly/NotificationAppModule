namespace NotificationApp.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
        Task<bool> SendSMS(string to, string message);
        Task<bool> SendPushNotification(string deviceId, string message);
    }

}
