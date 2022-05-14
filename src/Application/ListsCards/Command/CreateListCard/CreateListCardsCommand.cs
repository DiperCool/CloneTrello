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

namespace CleanArchitecture.Application.ListsCards.Command.CreateListCard;
[Authorize]
public class CreateListCardsCommand: IRequest<Guid>
{
    public Guid BoardId { get; set; }
    public string Title { get; set; }= String.Empty;
}

public class CreateListCardsCommandHandler : IRequestHandler<CreateListCardsCommand, Guid>
{
    ICurrentUserService _currentUser;
    IApplicationDbContext _context;
    public CreateListCardsCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context)
    {
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Guid> Handle(CreateListCardsCommand request, CancellationToken cancellationToken)
    {
        if(!await _context.Boards.AnyAsync(x=>x.OwnerId==_currentUser.UserIdGuid&&x.Id==request.BoardId))
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist");
        }
        int maxIndexNumber = await _context.ListsCards
                    .AsNoTracking()
                    .Where(x=>x.BoardId==request.BoardId)
                    .MaxAsync(x=>(int?)x.IndexNumber)??0;
        ListCards cards = new(){ Title=request.Title, BoardId=request.BoardId, IndexNumber=maxIndexNumber+1024 };
        await _context.ListsCards.AddAsync(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return cards.Id;

    }
}