using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Boards.Query.GetBoards
{
    [Authorize]
    public class GetBoardsQuery: IRequest<PaginatedList<BoardDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetBoardsQueryHandler : IRequestHandler<GetBoardsQuery, PaginatedList<BoardDTO>>
    {
        IApplicationDbContext _context;

        ICurrentUserService _currentUserService;

        IMapper _mapper;

        public GetBoardsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BoardDTO>> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Boards
                    .AsNoTracking()
                    .Where(x=>x.CreatedById==_currentUserService.UserIdGuid)
                    .ProjectTo<BoardDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}