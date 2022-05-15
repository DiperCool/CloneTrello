using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Cards.Command.RemoveCard;
[Authorize]

public class RemoveCardCommand: IRequest<Unit>
{
    public Guid CardId { get; set; }
}


public class RemoveCardCommandHandler : IRequestHandler<RemoveCardCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUserService;

    public RemoveCardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(RemoveCardCommand request, CancellationToken cancellationToken)
    {
        Card card = await _context.Cards.FirstOrDefaultAsync(x=>x.ListCards.Board.OwnerId==_currentUserService.UserIdGuid&&x.Id==request.CardId);
        if(card==null)
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist"); 
        }
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
