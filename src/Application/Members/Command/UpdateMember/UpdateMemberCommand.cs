using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.GettingBoardId;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Members.Command.UpdateMember;
[Authorize]
[UserIsMemberBoard(typeof(Board), NoAllowedGuest = true)]
public class UpdateMemberCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public MemberType MemberType { get; set; }
}
public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _userService;

    public UpdateMemberCommandHandler(IApplicationDbContext context, ICurrentUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<Unit> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        if(_userService.UserIdGuid==request.UserId)
        {
            throw new ForbiddenAccessException("You can't change your member type");
        }
        Member member = await _context.Members.FirstOrDefaultAsync(x=>x.BoardId==request.Id&&x.UserId==request.UserId)?? throw new NotFoundException("User isn't in this board");
        if(member.MemberType==MemberType.Owner)
        {
            throw new ForbiddenAccessException("You can't change owner's member type");
        } 
        member.MemberType= request.MemberType;
        _context.Members.Update(member);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
