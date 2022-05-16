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
    public class ListCardsBoardId : IBoardId
    {
        IApplicationDbContext _context;

        public ListCardsBoardId(IApplicationDbContext context)
        {
            _context = context;
        }

        public Type Key { get => typeof(ListCards);}
        public async Task<Guid> GetBoardId(Guid guid)
        {
            return (await _context.ListsCards.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==guid)??throw new NotFoundException("List card with this id not found")).BoardId;
        }
    }
}