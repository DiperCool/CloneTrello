using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Boards.Query.GetBoard
{
    public class GetBoardQueryValidator: AbstractValidator<GetBoardQuery>
    {
        public GetBoardQueryValidator()
        {
            RuleFor(x=>x.BoardId)
                .NotEmpty();
        }
    }
}