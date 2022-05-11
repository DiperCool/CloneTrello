using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Boards.Command.UpdateBoard
{
    public class UpdateBoardCommandValidator: AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(x=>x.Title)
                .NotNull();
        }
    }
}