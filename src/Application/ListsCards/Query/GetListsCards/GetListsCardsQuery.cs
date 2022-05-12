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
using MediatR;

namespace CleanArchitecture.Application.ListsCards.Query.GetListsCards;
public class GetListsCardsQuery: IRequest<PaginatedList<ListCardsDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid BoardId { get; set; }
}


public class GetListsCardsQueryHandler : IRequestHandler<GetListsCardsQuery, PaginatedList<ListCardsDTO>>
{
    ICurrentUserService _currentUser;
    IApplicationDbContext _applicationDbContext;
    IMapper _mapper;

    public GetListsCardsQueryHandler(ICurrentUserService currentUser, IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _currentUser = currentUser;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListCardsDTO>> Handle(GetListsCardsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.ListsCards
                        .Where(x=>x.BoardId==request.BoardId)
                        .ProjectTo<ListCardsDTO>(_mapper.ConfigurationProvider)
                        .OrderBy(x=>x.IndexNumber)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
