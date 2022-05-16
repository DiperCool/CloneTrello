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
    public class BoardBoardId : IBoardId
    {
        IApplicationDbContext _context;

        public BoardBoardId(IApplicationDbContext context)
        {
            _context = context;
        }
        public Type Key => typeof(Board);

        public async Task<Guid> GetBoardId(Guid guid)
        {
            return (await _context.Boards.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==guid)??throw new NotFoundException("Board with this id not found")).Id;
        }
    }
}