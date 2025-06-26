using NotificationService.Domain.Interfaces;

namespace NotificationService.Worker {
    public class Worker(
        ILogger<Worker> logger,
        IServiceScopeFactory scopeFactory
        ) : BackgroundService {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                using var scope = scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IUserNotificationRepository>();
                var sender = scope.ServiceProvider.GetRequiredService<INotificationSender>();

                var usersToNotify = await repository.GetUsersToNotifyAsync();

                foreach (var user in usersToNotify) {
                    logger.LogInformation($"��������� ����������� ��� {user.Email}");
                    await sender.SendNotificationAsync(user.Email, "³����! �� � ���� ��� 2 ��!");
                    await repository.MarkAsNotifiedAsync(user.Id);
                }

                await Task.Delay(TimeSpan.FromHours(24), cancellationToken);
            }
        }
    }
}
