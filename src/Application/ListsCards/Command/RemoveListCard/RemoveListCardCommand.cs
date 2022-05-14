using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.ListsCards.Command.RemoveListCard;

public class RemoveListCardCommand: IRequest<Unit>
{
    public Guid ListCardId { get; set; }
}

public class RemoveListCardCommandHandler : IRequestHandler<RemoveListCardCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _userService;

    public RemoveListCardCommandHandler(IApplicationDbContext context, ICurrentUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<Unit> Handle(RemoveListCardCommand request, CancellationToken cancellationToken)
    {
        ListCards cards = await _context.ListsCards.FirstOrDefaultAsync(x=>x.Board.OwnerId==_userService.UserIdGuid&&x.Id==request.ListCardId);
        if(cards==null)
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist");
        }
        _context.ListsCards.Remove(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
