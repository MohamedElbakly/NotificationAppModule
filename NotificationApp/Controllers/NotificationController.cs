using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApp.DTO;
using NotificationApp.Managers;

namespace NotificationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationManager _notificationManager;
        private readonly EmailNotificationManager _emailNotificationManager;
        private readonly SMSNotificationManager _smsNotificationManager;
        private readonly PushNotificationManager _pushNotificationManager;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(NotificationManager notificationManager, EmailNotificationManager emailNotificationManager, 
                                      SMSNotificationManager smsNotificationManager, PushNotificationManager pushNotificationManager, ILogger<NotificationController> logger)
        {
            _notificationManager = notificationManager;
            _emailNotificationManager = emailNotificationManager;
            _smsNotificationManager = smsNotificationManager;
            _pushNotificationManager = pushNotificationManager;
            _logger = logger;
        }

        [HttpPost("SendNotificationWithType")]
        public async Task<bool> SendNotification([FromBody] NotificationRequest request)
        {
            var type = request.Type;
            var to = request.To;
            var message = request.Message;

            bool res = await _notificationManager.SendNotification(type, to, message);
            return res;
        }

        [HttpPost("SendEmail")]
        public async Task<bool> SendEmail([FromBody] NotificationDTO request)
        {
            try
            {
                _logger.LogInformation("Test log for SendEmail method");
                var to = request.To;
                var message = request.Message;

                bool res = await _emailNotificationManager.SendEmailNotification(to, message);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                throw;
            }
        }
        [HttpPost("SendSMS")]
        public async Task<bool> SendSMS([FromBody] NotificationDTO request)
        {
            var to = request.To;
            var message = request.Message;

            bool res = await _smsNotificationManager.SendSMSNotification(to, message);
            return res;
        }

        [HttpPost("PushNotification")]
        public async Task<bool> PushNotification([FromBody] NotificationDTO request)
        {
            var to = request.To;
            var message = request.Message;

            bool res = await _pushNotificationManager.SendPushNotification(to, message);
            return res;
        }
    }
}
