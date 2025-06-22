using NotificationService.Worker;
using MassTransit;
using NotificationService.Infrastructure.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x => {
    x.AddConsumer<UserRegisteredConsumer>();

    x.UsingRabbitMq((context, cfg) => {
        cfg.Host("rabbitmq", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("user-registered-queue", e => {
            e.ConfigureConsumer<UserRegisteredConsumer>(context);
        });
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
