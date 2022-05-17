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

namespace CleanArchitecture.Application.Members.Command.KickUser;
[Authorize]
[UserIsMemberBoard(typeof(Board), NoAllowedGuest =true)]
public class KickUserCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public Guid UserId { get; set;}
}

public class KickUserCommandHandler : IRequestHandler<KickUserCommand, Unit>
{
    IApplicationDbContext _context;

    public KickUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(KickUserCommand request, CancellationToken cancellationToken)
    {
        Member member = await _context.Members.FirstOrDefaultAsync(x=>x.BoardId==request.Id&&x.UserId==request.UserId)?? throw new NotFoundException("User isn't in this board");
        if(member.MemberType==MemberType.Owner)
        {
            throw new ForbiddenAccessException("You can't kick owner");
        }  
        _context.Members.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
