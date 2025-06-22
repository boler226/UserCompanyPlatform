using UsersService.Application.Commands.CreateUser;
using UsersService.Application.Validators;
using FluentValidation;
using UsersService.Application.Mappings;
using UsersService.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UsersService.Infrastructure.DbContext;
using NotificationService.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("PostgreSQLConnection")!);

builder.WebHost.ConfigureKestrel(options => {
    options.ListenAnyIP(5000); // HTTP 
    //options.ListenAnyIP(5000, listenOptions => listenOptions.UseHttps()); // HTTPS
});



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

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "USER SERVICE V1");
    c.RoutePrefix = "";
});

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();