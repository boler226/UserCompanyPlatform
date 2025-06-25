
using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Infrastructure.Consumers {
    public class CompanyCreateConsumer(
        ILogger<CompanyCreateConsumer> logger
        ) : IConsumer<CompanyCreateEvent> {
        public Task Consume(ConsumeContext<CompanyCreateEvent> context) {
            var message = context.Message;
            logger.LogInformation($" Отримано подію CompanyCreate: Email={message.Email}");

            //надіслати повідомлення (наприклад Email)

            return Task.CompletedTask;
        }
    }
}
