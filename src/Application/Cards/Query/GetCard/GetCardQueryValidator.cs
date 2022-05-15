using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Cards.Query.GetCard
{
    public class GetCardQueryValidator: AbstractValidator<GetCardQuery>
    {
        public GetCardQueryValidator()
        {
            RuleFor(x=>x.CardId).NotEmpty();
        }
    }
}