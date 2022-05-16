using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Command.CreateListCard
{
    public class CreateListCardsCommandValidator: AbstractValidator<CreateListCardsCommand>
    {
        public CreateListCardsCommandValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty();
            RuleFor(x=>x.Title)
                .NotEmpty();
        }
    }
}