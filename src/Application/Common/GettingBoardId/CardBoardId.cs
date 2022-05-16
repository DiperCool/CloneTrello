using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.GettingBoardId
{
    public class CardBoardId : IBoardId
    {
        IApplicationDbContext _context;

        public CardBoardId(IApplicationDbContext context)
        {
            _context = context;
        }

        public Type Key => typeof(Card);

        public async Task<Guid> GetBoardId(Guid guid)
        {
            return (await _context.Cards
                        .Select(x=>new{
                            Id= x.Id,
                            BoardId = x.ListCards.BoardId
                        })
                        .FirstOrDefaultAsync(x=>x.Id==guid)??throw new NotFoundException("Card with this id not found")).BoardId;
        }
    }
}