using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Interfaces;

namespace NotificationService.Infrastructure.Consumers
{
    public class UserRegisteredConsumer(
        ILogger<UserRegisteredConsumer> logger,
        IUserNotificationRepository repository
        ) : IConsumer<UserRegisteredEvent>{
        public async Task Consume(ConsumeContext<UserRegisteredEvent> context) {
            var message = context.Message;
            logger.LogInformation($" Отримано подію UserRegistered: Email={message.Email}, Дата={message.RegisteredAt}");

            var entity = new UserNotificationSchedule {
                Email = message.Email,
                RegisteredAt = message.RegisteredAt
            };

            await repository.AddAsync(entity);
        }
    }
}
