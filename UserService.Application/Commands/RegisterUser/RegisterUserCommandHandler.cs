using FluentValidation;
using MediatR;
using UserService.Domain.Entities;
using UserService.Infrastructure.UnitOfWork.Interfaces;


namespace UserService.Application.Commands.CreateUser {
    public class RegisterUserCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<RegisterUserCommand> validator
        ) : IRequestHandler<RegisterUserCommand, Guid> {
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var user = new User {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RegisteredAt = DateTime.UtcNow
            };

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
