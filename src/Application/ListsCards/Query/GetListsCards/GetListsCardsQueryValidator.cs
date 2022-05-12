using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Query.GetListsCards
{
    public class GetListsCardsQueryValidator: AbstractValidator<GetListsCardsQuery>
    {
        public GetListsCardsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
            RuleFor(x=>x.BoardId)
                .NotNull();
        }
    }
}