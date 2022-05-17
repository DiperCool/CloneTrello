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

namespace CleanArchitecture.Application.Members.Query.GetMembers;
public class GetMembersQuery: IRequest<PaginatedList<MemberDTO>>
{
    public Guid Id { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10; 
}

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, PaginatedList<MemberDTO>>
{
    IMapper _mapper;

    IApplicationDbContext _applicationDbContext;

    public GetMembersQueryHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<PaginatedList<MemberDTO>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Members
                        .Where(x=>x.BoardId==request.Id)
                        .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
