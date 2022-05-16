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

namespace CleanArchitecture.Application.ListsCards.Command.RemoveListCard;
[Authorize]
[UserIsMemberBoard(typeof(ListCards))]
public class RemoveListCardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
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
        ListCards cards = await _context.ListsCards.FirstOrDefaultAsync(x=>x.Id==request.Id)?? throw new NotFoundException("List card with this Id not found");
        _context.ListsCards.Remove(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
