using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Queries.LoginUser {
    public record LoginUserQuery(string Email, string Password) : IRequest<string>;
}
