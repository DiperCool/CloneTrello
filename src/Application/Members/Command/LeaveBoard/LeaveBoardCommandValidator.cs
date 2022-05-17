using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Members.Command.LeaveBoard
{
    public class LeaveBoardCommandValidator: AbstractValidator<LeaveBoardCommand>
    {
        public LeaveBoardCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}