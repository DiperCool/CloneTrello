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

namespace CleanArchitecture.Application.Cards.Command.UpdateCard;
[Authorize]
[UserIsMemberBoard(typeof(Card))]

public class UpdateCardCommand: IRequest<Unit>,IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}


public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, Unit>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUserService;

    public UpdateCardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        Card card = await _context.Cards.FirstOrDefaultAsync(x=>x.Id==request.Id)?? throw new NotFoundException("List card with this Id not found"); 

        card.Title = request.Title;
        _context.Cards.Update(card);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
