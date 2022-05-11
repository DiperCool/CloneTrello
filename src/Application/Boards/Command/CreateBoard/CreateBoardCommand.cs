using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Boards.Command.CreateBoard;
[Authorize]
public class CreateBoardCommand: IRequest<Guid>
{
    public string Title { get; set; } = String.Empty;
    public Visibility Visibility = Visibility.Public; 
}


public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Guid>
{
    ICurrentUserService _currentUserService;

    IApplicationDbContext _applicationDbContext;

    public CreateBoardCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        Board board = new (){Title = request.Title, Visibility = request.Visibility, OwnerId = new Guid(_currentUserService.UserId)};
        await _applicationDbContext.Boards.AddAsync(board);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return board.Id;
    }
}
