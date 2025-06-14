using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Interfaces;

namespace UserService.Application.Queries.GetUserById {
    public class GetUserByIdQueryHandler(
       IUserRepository userRepository,
       IMapper mapper
       ) : IRequestHandler<GetUserByIdQuery, UserDto> {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
            var user = await userRepository.GetByIdAsync(request.id);

            if (user is null)
                throw new Exception("User not found");

            return mapper.Map<UserDto>(user);
        }
    }
}
