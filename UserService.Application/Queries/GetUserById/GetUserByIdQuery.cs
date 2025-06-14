using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.GetUserById {
    public record GetUserByIdQuery(Guid id) : IRequest<UserDto>;
}
