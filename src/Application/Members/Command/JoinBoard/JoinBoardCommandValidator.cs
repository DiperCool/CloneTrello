using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Members.Command.JoinBoard
{
    public class JoinGroupCommandValidator: AbstractValidator<JoinBoardCommand>
    {
        public JoinGroupCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UserId).NotEmpty();
        }
    }
}