using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Cards.Command.MoveCard
{
    public class MoveCardCommandValidator: AbstractValidator<MoveCardCommand>
    {
        public MoveCardCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.MoveTo).NotEmpty();
            RuleFor(x=>x.NextIndexNumber)
                .NotNull()
                .When(x=>x.PrevIndexNumber==null)
                .WithMessage("'Next Index Number' must not be empty when 'Prev Index Number' is empty");
        }
    }
}