using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Domain.Entities;
using UsersService.Domain.Interfaces;
using UsersService.Infrastructure.DbContext;
using UsersService.Infrastructure.Repositories;
using UsersService.Infrastructure.UnitOfWork.Interfaces;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Services;
using Contracts.Requests;

namespace UsersService.Infrastructure.Services
{
    public static class CollectionExtensionsService {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
            Console.WriteLine($"DB CONNECTION: {connectionString}");
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Identity
            services.AddIdentity<User, IdentityRole<Guid>>(options => {
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

            // Repositories & Services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IEntityExistenceChecker, EntityExistenceChecker>();

            // MassTransit
            services.AddMassTransit(x => {
                x.UsingRabbitMq((context, cfg) => {
                    cfg.Host("rabbitmq", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddRequestClient<CheckCompanyExistsRequest>();
            });

            return services;
         }
    }
}