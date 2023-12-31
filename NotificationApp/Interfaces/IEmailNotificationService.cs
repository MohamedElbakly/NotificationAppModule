namespace NotificationApp.Interfaces
{
    public interface IEmailNotificationService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);

    }
}
