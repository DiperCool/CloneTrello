using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Command.MoveListCard
{
    public class MoveListCardCommandValidator: AbstractValidator<MoveListCardCommand>
    {
        public MoveListCardCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();

            RuleFor(x=>x.NextIndexNumber)
                .NotNull()
                .When(x=>x.PrevIndexNumber==null)
                .WithMessage("'Next Index Number' must not be empty when 'Prev Index Number' is empty");
        }
    }
}