using NotificationApp.Interfaces;
using NotificationApp.Services;

namespace NotificationApp.Managers
{
    public class EmailNotificationManager
    {
        private readonly IEmailNotificationService _emailNotificationService;
        public EmailNotificationManager(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        public async Task<bool> SendEmailNotification(string to, string message)
        {
            bool sent = await _emailNotificationService.SendEmailAsync(to, "Notification", message);
   
            return sent;
        }
    }
}
