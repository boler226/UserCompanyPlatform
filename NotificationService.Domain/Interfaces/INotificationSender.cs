namespace NotificationService.Domain.Interfaces {
    public interface INotificationSender {
        Task SendNotificationAsync(string email, string message);
    }
}
