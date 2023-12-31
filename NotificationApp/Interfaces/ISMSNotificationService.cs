namespace NotificationApp.Interfaces
{
    public interface ISMSNotificationService
    {
        Task<bool> SendSMS(string to, string message);
    }
}
