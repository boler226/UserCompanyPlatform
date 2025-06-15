using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Infrastructure.Consumers {
    public class UserRegisteredConsumer(
        ILogger<UserRegisteredConsumer> logger
        ) : IConsumer<UserRegisteredEvent>{
        public Task Consume(ConsumeContext<UserRegisteredEvent> context) {
            var message = context.Message;
            logger.LogInformation($" Отримано подію UserRegistered: Email={message.Email}, Дата={message.RegisteredAt}");

            //надіслати повідомлення (наприклад Email)

            return Task.CompletedTask;
        }
    }
}
