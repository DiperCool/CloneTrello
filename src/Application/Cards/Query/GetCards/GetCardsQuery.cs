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

namespace CleanArchitecture.Application.Cards.Query.GetCards;
public class GetCardsQuery: IRequest<PaginatedList<CardDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid ListCardId { get; set; }
}


public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, PaginatedList<CardDTO>>
{
    ICurrentUserService _currentUser;
    IApplicationDbContext _applicationDbContext;
    IMapper _mapper;

    public GetCardsQueryHandler(ICurrentUserService currentUser, IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _currentUser = currentUser;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CardDTO>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Cards
                        .Where(x=>x.ListCardsId==request.ListCardId)
                        .ProjectTo<CardDTO>(_mapper.ConfigurationProvider)
                        .OrderBy(x=>x.IndexNumber)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
