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

namespace CleanArchitecture.Application.ListsCards.Query.GetListCards;
public class GetListCardsQuery: IRequest<ListCardsDTO>
{
    public Guid ListCardId { get; set; }
}

public class GetListCardsQueryHandler : IRequestHandler<GetListCardsQuery, ListCardsDTO>
{
    IApplicationDbContext _context;

    IMapper _mapper;

    public GetListCardsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListCardsDTO> Handle(GetListCardsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ListsCards
                    .AsNoTracking()
                    .Where(x=>x.Id==request.ListCardId)
                    .ProjectTo<ListCardsDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                    ?? throw new NotFoundException("A list card with this ID doesn't exist");  
    }
}
