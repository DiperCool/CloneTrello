using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Command.UpdateListCard
{
    public class UpdateListCardCommandValidator: AbstractValidator<UpdateListCardCommand>
    {
        public UpdateListCardCommandValidator()
        {
            RuleFor(x=>x.ListCardId).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
        }
    }
}