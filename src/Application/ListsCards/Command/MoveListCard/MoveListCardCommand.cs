using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.ListsCards.Command.MoveListCard;
public class MoveListCardCommand: IRequest<Unit>
{    
    public Guid ListCardId { get; set; }
    public int? PrevIndexNumber { get; set; }=null;
    public int? NextIndexNumber { get; set; }=null;
}

public class MoveListCardCommandHandler : IRequestHandler<MoveListCardCommand, Unit>
{
    ICurrentUserService _currentUserService;
    IApplicationDbContext _context;

    public MoveListCardCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Unit> Handle(MoveListCardCommand request, CancellationToken cancellationToken)
    {
        ListCards cards = await _context.ListsCards.FirstOrDefaultAsync(x=>x.Board.OwnerId==_currentUserService.UserIdGuid&&x.Id==request.ListCardId);
        if(cards==null)
        {
            throw new ForbiddenAccessException("You're not owner of this board or this board with this ID doesn't exist"); 
        }
        if(request.PrevIndexNumber==null) 
        {
            cards.IndexNumber = (int)request.NextIndexNumber - 512;
        } 
        else if (request.NextIndexNumber == null) 
        {
            cards.IndexNumber = (int)request.PrevIndexNumber + 512;
        } 
        else 
        {
            cards.IndexNumber = ((int)request.NextIndexNumber+ (int)request.PrevIndexNumber ) /2;
        }
        _context.ListsCards.Update(cards);
        if(
            request.PrevIndexNumber!=null && Math.Abs(cards.IndexNumber-(int)request.PrevIndexNumber) <=1
            ||
            request.NextIndexNumber!=null && Math.Abs(cards.IndexNumber-(int)request.NextIndexNumber) <=1
        )
        {
            int indexNumber = 0;
            List<ListCards> listscards = await _context.ListsCards.Where(x=>x.BoardId==cards.BoardId).ToListAsync();
            foreach(ListCards listCard in listscards)
            {
                indexNumber+=1024;
                listCard.IndexNumber=indexNumber;
            }
            _context.ListsCards.UpdateRange(listscards);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
