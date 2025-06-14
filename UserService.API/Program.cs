using UserService.Application.Commands.CreateUser;
using UserService.Application.Validators;
using FluentValidation;
using UserService.Application.Mappings;
using UserService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("PostgreSQLConnection")!);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();