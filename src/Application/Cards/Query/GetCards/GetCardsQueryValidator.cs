using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Cards.Query.GetCards
{
    public class GetCardsQueryValidator: AbstractValidator<GetCardsQuery>
    {
        public GetCardsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
            RuleFor(x=>x.ListCardId)
                .NotEmpty();
        }
    }
}