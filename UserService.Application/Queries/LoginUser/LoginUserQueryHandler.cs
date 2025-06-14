using MediatR;
using Microsoft.Extensions.Configuration;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.UnitOfWork.Interfaces;

namespace UserService.Application.Queries.LoginUser {
    public class LoginUserQueryHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService
        ) : IRequestHandler<LoginUserQuery, string> {
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken) {
            var user = await unitOfWork.Users.GetByEmailAsync(request.Email)
                    ?? throw new Exception("Invalid credentials");
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

           return await jwtTokenService.GenerateTokenAsync(user);
        }
    }
}
