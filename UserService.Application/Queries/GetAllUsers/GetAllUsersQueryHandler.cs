using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Interfaces;

namespace UserService.Application.Queries.GetAllUsers {
    internal class GetAllUsersQueryHandler(
        IUserRepository userRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllUsersQuery, List<UserDto>> {
        public async Task<List<UserDto>> Handle(GetAllUsersQuery requst, CancellationToken cancellationToken) {
            var users = await userRepository.GetAllAsync();
            return mapper.Map<List<UserDto>>(users);
        }
    }
}
