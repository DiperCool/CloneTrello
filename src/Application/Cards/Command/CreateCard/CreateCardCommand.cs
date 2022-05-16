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

namespace CleanArchitecture.Application.Cards.Command.CreateCard;
[Authorize]
[UserIsMemberBoard(typeof(ListCards))]

public class CreateCardCommand: IRequest<Guid>, IUserIsMemberBoard
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}


public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Guid>
{
    IApplicationDbContext _context;
    ICurrentUserService _currentUser;

    public CreateCardCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        int maxIndexNumber = await _context.Cards
                    .AsNoTracking()
                    .Where(x=>x.ListCardsId==request.Id)
                    .MaxAsync(x=>(int?)x.IndexNumber)??0;
        Card card = new(){ Title=request.Title, ListCardsId=request.Id, IndexNumber=maxIndexNumber+1024 };
        await _context.Cards.AddAsync(card);
        await _context.SaveChangesAsync(cancellationToken);
        return card.Id;
    }
}
