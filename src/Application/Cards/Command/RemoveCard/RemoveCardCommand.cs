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

namespace CleanArchitecture.Application.Cards.Command.RemoveCard;
[Authorize]
[UserIsMemberBoard(typeof(Card))]

public class RemoveCardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
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
        Card card = await _context.Cards.FirstOrDefaultAsync(x=>x.Id==request.Id)?? throw new NotFoundException("List card with this Id not found"); 
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
