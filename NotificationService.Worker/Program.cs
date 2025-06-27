using NotificationService.Worker;
using MassTransit;
using NotificationService.Infrastructure.Consumers;
using NotificationService.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure.Reposetories;
using NotificationService.Infrastructure.Services;
using NotificationService.Infrastructure.DTOs;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

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

builder.Services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
builder.Services.AddScoped<INotificationSender, NotificationSender>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();