using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.GettingBoardId;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Members.Command.LeaveBoard;
[Authorize]
[UserIsMemberBoard(typeof(Board))]
public class LeaveBoardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
}
public class LeaveBoardCommandHandler : IRequestHandler<LeaveBoardCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;

    public LeaveBoardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(LeaveBoardCommand request, CancellationToken cancellationToken)
    {
        Member member = await _context.Members.FirstOrDefaultAsync(x=>x.BoardId==request.Id&&x.UserId==_currentUser.UserIdGuid)?? throw new NotFoundException("You're not in this board"); 
        _context.Members.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
