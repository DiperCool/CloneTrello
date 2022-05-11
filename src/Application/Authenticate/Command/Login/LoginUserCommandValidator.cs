using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Authenticate.Command.Login
{
    public class LoginUserCommandValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x=>x.Login)
                .NotEmpty();
            RuleFor(x=>x.Password)
                .NotEmpty();
        }
    }
}