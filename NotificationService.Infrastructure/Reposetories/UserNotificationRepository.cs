using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure.DbContext;

namespace NotificationService.Infrastructure.Reposetories {
    public class UserNotificationRepository(
        NotificationDbContext context
        ) : IUserNotificationRepository {
        public async Task AddAsync(UserNotificationSchedule schedule) {
            await context.UserNotificationSchedules.AddAsync(schedule);
            await context.SaveChangesAsync();
        }

        public async Task<List<UserNotificationSchedule>> GetUsersToNotifyAsync() {
            var targetDate = DateTime.UtcNow.Date.AddDays(-2);
            return await context.UserNotificationSchedules
                    .Where(n => !n.NotificationSent && n.RegisteredAt.Date <= targetDate)
                    .ToListAsync();
        }

        public async Task MarkAsNotifiedAsync(Guid id) {
            var entity = await context.UserNotificationSchedules.FindAsync(id);
            if (entity != null) { 
                entity.NotificationSent = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
