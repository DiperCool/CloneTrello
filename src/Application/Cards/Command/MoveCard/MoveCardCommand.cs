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

namespace CleanArchitecture.Application.Cards.Command.MoveCard;
[Authorize]

public class MoveCardCommand: IRequest<Unit>
{
    public Guid CardId { get; set; }
    public int? PrevIndexNumber { get; set; }=null;
    public int? NextIndexNumber { get; set; }=null;
}


public class MoveCardCommandHandler : IRequestHandler<MoveCardCommand, Unit>
{
    ICurrentUserService _currentUserService;
    IApplicationDbContext _context;

    public MoveCardCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }
    public async Task<Unit> Handle(MoveCardCommand request, CancellationToken cancellationToken)
    {
        Card card = await _context.Cards.FirstOrDefaultAsync(x=>x.ListCards.Board.OwnerId==_currentUserService.UserIdGuid&&x.Id==request.CardId);
        if(card==null)
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist"); 
        }
        if(request.PrevIndexNumber==null) 
        {
            card.IndexNumber = (int)request.NextIndexNumber - 512;
        } 
        else if (request.NextIndexNumber == null) 
        {
            card.IndexNumber = (int)request.PrevIndexNumber + 512;
        } 
        else 
        {
            card.IndexNumber = ((int)request.NextIndexNumber+ (int)request.PrevIndexNumber ) /2;
        }
        _context.Cards.Update(card);
        if(
            request.PrevIndexNumber!=null && Math.Abs(card.IndexNumber-(int)request.PrevIndexNumber) <=1
            ||
            request.NextIndexNumber!=null && Math.Abs(card.IndexNumber-(int)request.NextIndexNumber) <=1
        )
        {
            int indexNumber = 0;
            List<Card> cards = await _context.Cards.Where(x=>x.ListCardsId==card.ListCardsId).ToListAsync();
            foreach(Card item in cards)
            {
                indexNumber+=1024;
                item.IndexNumber=indexNumber;
            }
            _context.Cards.UpdateRange(cards);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
