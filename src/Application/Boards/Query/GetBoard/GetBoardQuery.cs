using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Boards.Query.GetBoard;

public class GetBoardQuery: IRequest<BoardDTO>
{
    public Guid BoardId { get; set; }
}

public class GetBoardQueryHandler : IRequestHandler<GetBoardQuery, BoardDTO>
{
    IApplicationDbContext _context;

    IMapper _mapper;

    public GetBoardQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BoardDTO> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        return (await _context.Boards
                    .Where(x=>x.Id==request.BoardId)
                    .ProjectTo<BoardDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync())?? throw new NotFoundException("A board with this ID doesn't exist"); 
    }
}
