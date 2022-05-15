using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Cards.Command.UpdateCard
{
    public class UpdateCardCommandValidator: AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(x=>x.CardId).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
        }
    }
}