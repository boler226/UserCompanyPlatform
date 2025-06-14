using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.DbContext;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.UnitOfWork.Interfaces;

namespace UserService.Infrastructure.Services {
    public static class CollectionExtensionsService {
         public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddIdentity<User, IdentityRole<Guid>>(options => {
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
         }
    }
}