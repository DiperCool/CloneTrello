using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Cards.Command.RemoveCard
{
    public class RemoveCardCommandValidator: AbstractValidator<RemoveCardCommand>
    {
        public RemoveCardCommandValidator()
        {
            RuleFor(x=>x.CardId).NotEmpty();
        }
    }
}