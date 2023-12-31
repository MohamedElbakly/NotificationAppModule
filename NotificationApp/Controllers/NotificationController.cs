using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApp.DTO;
using NotificationApp.Managers;

namespace NotificationApp.Controllers
{
    /// <summary>
    /// Notification Controller
    /// </summary>
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
        /// <summary>
        /// Send Notification as per type
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        [HttpPost("SendNotificationWithType")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            var type = request.Type;
            var to = request.To;
            var message = request.Message;

            bool res = await _notificationManager.SendNotification(type, to, message);
            return Ok(res);
        }

        /// <summary>
        /// Send Email Notification method
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] Request request)
        {
            try
            {
                _logger.LogInformation("Test log for SendEmail method");
                var to = request.To;
                var message = request.Message;

                bool res = await _emailNotificationManager.SendEmailNotification(to, message);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Send SMS Notification method
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        [HttpPost("SendSMS")]
        public async Task<IActionResult> SendSMS([FromBody] Request request)
        {
            var to = request.To;
            var message = request.Message;

            bool res = await _smsNotificationManager.SendSMSNotification(to, message);
            return Ok(res);
        }
       
        /// <summary>
        /// Push Notification to device this is supposed to be used from frontend side to complete cycle
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        [HttpPost("PushNotification")]
        public async Task<IActionResult> PushNotification([FromBody] Request request)
        {
            var to = request.To;
            var message = request.Message;

            bool res = await _pushNotificationManager.SendPushNotification(to, message);
            return Ok(res);
        }
    }
}
