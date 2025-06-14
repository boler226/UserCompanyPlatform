using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.GetAllUsers {
    public record GetAllUsersQuery : IRequest<List<UserDto>>;
}
