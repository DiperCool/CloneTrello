using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Boards.Command.RemoveBoard
{
    public class RemoveBoardCommandValidator: AbstractValidator<RemoveBoardCommand>
    {
        public RemoveBoardCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}