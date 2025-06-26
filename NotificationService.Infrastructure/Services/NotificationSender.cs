using Microsoft.Extensions.Logging;
using NotificationService.Domain.Interfaces;

namespace NotificationService.Infrastructure.Services {
    public class NotificationSender(
        ILogger<NotificationSender> logger
        ) : INotificationSender {
        public Task SendNotificationAsync(string email, string message) {
            logger.LogInformation($"[EMAIL] To: {email} | Msg: {message}");
            return Task.CompletedTask;
        }
    }
}
