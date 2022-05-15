using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.ListsCards.Query.GetListCards
{
    public class GetListCardsQueryValidator: AbstractValidator<GetListCardsQuery>
    {
        public GetListCardsQueryValidator()
        {
            RuleFor(x=>x.ListCardId).NotEmpty();
        }
    }
}