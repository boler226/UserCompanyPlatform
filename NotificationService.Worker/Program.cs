using NotificationService.Worker;
using MassTransit;
using NotificationService.Infrastructure.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x => {
    x.AddConsumer<UserRegisteredConsumer>();
    x.AddConsumer<CompanyCreateConsumer>();

    x.UsingRabbitMq((context, cfg) => {
        cfg.Host("rabbitmq", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();