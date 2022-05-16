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

namespace CleanArchitecture.Application.ListsCards.Command.CreateListCard;
[Authorize]
[UserIsMemberBoard(typeof(Board))]
public class CreateListCardsCommand: IRequest<Guid>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
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
        int maxIndexNumber = await _context.ListsCards
                    .AsNoTracking()
                    .Where(x=>x.BoardId==request.Id)
                    .MaxAsync(x=>(int?)x.IndexNumber)??0;
        ListCards cards = new(){ Title=request.Title, BoardId=request.Id, IndexNumber=maxIndexNumber+1024 };
        await _context.ListsCards.AddAsync(cards);
        await _context.SaveChangesAsync(cancellationToken);
        return cards.Id;

    }
}