using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure.DTOs;

namespace NotificationService.Infrastructure.Services {
    public class NotificationSender(
        ILogger<NotificationSender> logger,
        IOptions<EmailSettings> emailOptions
        ) : INotificationSender {
        private readonly EmailSettings settings = emailOptions.Value;
        public async Task<bool> SendNotificationAsync(string email, string message) {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(settings.SenderName, settings.SenderEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = "Вітаємо, ви успішно зареєструвались!";

            mimeMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient()) {
                try {
                    await client.ConnectAsync(settings.SmtpServer, settings.SmtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(settings.Username, settings.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);

                    logger.LogInformation($"[EMAIL] To: {email} | Msg: {message}");
                    return true;
                }
                catch (Exception ex) {
                    logger.LogError(ex, $"[EMAIL ERROR] Не вдалося надіслати листа до {email}");
                    return false;
                }
            }
        }
    }
}
