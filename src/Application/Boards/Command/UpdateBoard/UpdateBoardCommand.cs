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

namespace CleanArchitecture.Application.Boards.Command.UpdateBoard;
[Authorize]
[UserIsMemberBoard(typeof(Board), NoAllowedGuest =true)]

public class UpdateBoardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public Visibility Visibility { get ;set; }
}
public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Unit>
{
    ICurrentUserService _currentUserService;
    IApplicationDbContext _context;

    public UpdateBoardCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        Board board =await _context.Boards.FirstOrDefaultAsync(x=>x.CreatedById==_currentUserService.UserIdGuid&&x.Id==request.Id);
        if(board==null)
        {
            throw new ForbiddenAccessException("You're not an owner of this board");
        }
        board.Title = request.Title;
        board.Visibility=request.Visibility;
        _context.Boards.Update(board);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
