using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using FluentValidation;

namespace CleanArchitecture.Application.Members.Command.UpdateMember
{
    public class UpdateMemberCommandValidator: AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UserId).NotEmpty();
            RuleFor(x=>x.MemberType).NotEqual(MemberType.Owner);
        }
    }
}