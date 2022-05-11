using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Boards.Command.CreateBoard
{
    public class CreateBoardCommandValidator: AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {
            RuleFor(x=>x.Title)
                .NotEmpty();
        }
    }
}