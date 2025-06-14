using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.CreateUser;
using UserService.Application.DTOs;
using UserService.Application.Queries.GetAllUsers;
using UserService.Application.Queries.GetUserById;
using UserService.Application.Queries.LoginUser;

namespace UserService.API.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController(IMediator mediator) : ControllerBase {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var result = await mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command) {
            var userId = await mediator.Send(command);
            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            var token = await mediator.Send(new LoginUserQuery(loginDto.Email, loginDto.Password));
            return Ok(token);
        }
    }
}
