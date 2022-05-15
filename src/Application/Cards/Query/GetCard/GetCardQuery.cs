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

namespace CleanArchitecture.Application.Cards.Query.GetCard;
public class GetCardQuery: IRequest<CardDTO>
{
    public Guid CardId { get; set; }
}

public class GetCardQueryHandler : IRequestHandler<GetCardQuery, CardDTO>
{
    IApplicationDbContext _context;

    IMapper _mapper;

    public GetCardQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CardDTO> Handle(GetCardQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cards
                    .AsNoTracking()
                    .Where(x=>x.Id==request.CardId)
                    .ProjectTo<CardDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                    ?? throw new NotFoundException("A card with this ID doesn't exist");  
    }
}
