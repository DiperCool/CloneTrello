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

namespace CleanArchitecture.Application.ListsCards.Command.UpdateListCard;
[Authorize]
public class UpdateListCardCommand: IRequest<Unit>
{
    public Guid ListCardId { get; set; }
    public string Title { get; set; }
}

public class UpdateListCardCommandHandler : IRequestHandler<UpdateListCardCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _userService;

    public UpdateListCardCommandHandler(IApplicationDbContext context, ICurrentUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<Unit> Handle(UpdateListCardCommand request, CancellationToken cancellationToken)
    {
        ListCards cards = await _context.ListsCards.FirstOrDefaultAsync(x=>x.Board.OwnerId==_userService.UserIdGuid&&x.Id==request.ListCardId);
        if(cards==null)
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist");
        }
        cards.Title=request.Title;
        _context.ListsCards.Update(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
