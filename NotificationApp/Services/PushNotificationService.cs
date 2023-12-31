using FirebaseAdmin.Messaging;
using NotificationApp.Interfaces;

namespace NotificationApp.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly FirebaseMessaging _firebaseMessaging;

        public PushNotificationService(FirebaseMessaging firebaseMessaging)
        {
            _firebaseMessaging = firebaseMessaging;
        }
        public async Task<bool> SendPushNotification(string deviceId, string message)
        {
            var messageObj = new Message
            {
                Token = deviceId,
                Notification = new Notification
                {
                    Title = "Push Notification",
                    Body = message
                }
            };

            try
            {
                var response = _firebaseMessaging.SendAsync(messageObj).GetAwaiter().GetResult();
                return true;
            }
            catch (FirebaseMessagingException ex)
            {
                return false;
            }
        }
    }
}
