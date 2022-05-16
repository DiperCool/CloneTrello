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

namespace CleanArchitecture.Application.ListsCards.Command.UpdateListCard;
[Authorize]
[UserIsMemberBoard(typeof(ListCards))]
public class UpdateListCardCommand: IRequest<Unit>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
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
        ListCards cards = await _context.ListsCards.FirstOrDefaultAsync(x=>x.Id==request.Id)?? throw new NotFoundException("List card with this Id not found");        
        cards.Title=request.Title;
        _context.ListsCards.Update(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
