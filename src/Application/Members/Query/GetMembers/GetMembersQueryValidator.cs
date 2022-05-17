using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Members.Query.GetMembers
{
    public class GetMembersQueryValidator: AbstractValidator<GetMembersQuery>
    {
        public GetMembersQueryValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        }
    }
}