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

namespace CleanArchitecture.Application.Members.Command.JoinBoard;
[Authorize]
[UserIsMemberBoard(typeof(Board), NoAllowedGuest = true)]
public class JoinBoardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}

public class JoinBoardCommandHandler : IRequestHandler<JoinBoardCommand, Unit>
{
    ICurrentUserService _currentUser;
    IApplicationDbContext _context;

    public JoinBoardCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context)
    {
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Unit> Handle(JoinBoardCommand request, CancellationToken cancellationToken)
    {
        if(!await _context.Users.AnyAsync(x=>x.Id==request.UserId))
        {
            throw new NotFoundException("User not found"); 
        }
        if(await _context.Members.AnyAsync(x=>x.BoardId==request.Id&&x.UserId==request.UserId))
        {
            throw new BadRequestException("User is already a member of this group");
        }
        Member member = new Member(){ UserId=request.UserId, BoardId=request.Id, MemberType = MemberType.Guest };
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
