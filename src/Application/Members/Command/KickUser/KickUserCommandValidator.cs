using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Members.Command.KickUser
{
    public class KickUserCommandValidator: AbstractValidator<KickUserCommand>
    {
        public KickUserCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UserId).NotEmpty();
        }
    }
}