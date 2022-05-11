using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Boards.Command.RemoveBoard
{
    public class RemoveBoardCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class RemoveBoardCommandHandler : IRequestHandler<RemoveBoardCommand, Unit>
    {
        ICurrentUserService _currentUserService;
        IApplicationDbContext _context;

        public RemoveBoardCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Unit> Handle(RemoveBoardCommand request, CancellationToken cancellationToken)
        {
            Board? board =await _context.Boards.FirstOrDefaultAsync(x=>x.OwnerId==_currentUserService.UserIdGuid&&x.Id==request.Id);
            if(board==null)
            {
                throw new ForbiddenAccessException("You're not an owner of this board");
            }
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}