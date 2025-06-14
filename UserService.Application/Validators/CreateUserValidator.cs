using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Commands.CreateUser;

namespace UserService.Application.Validators {
    public class CreateUserValidator : AbstractValidator<RegisterUserCommand> {
        public CreateUserValidator() {
            RuleFor(u => u.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(u => u.LastName).NotEmpty().MaximumLength(100);
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6);
        }
    }
}
