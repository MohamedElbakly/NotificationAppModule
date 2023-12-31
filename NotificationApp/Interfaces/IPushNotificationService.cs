namespace NotificationApp.Interfaces
{
    public interface IPushNotificationService
    {
        Task<bool> SendPushNotification(string deviceId, string message);

    }
}
