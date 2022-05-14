using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Command.RemoveListCard
{
    public class RemoveListCardCommandValidator: AbstractValidator<RemoveListCardCommand>
    {
        public RemoveListCardCommandValidator()
        {
            RuleFor(x=>x.ListCardId).NotEmpty();
        }
    }
}