using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Users.Query.GetMe
{
    [Authorize]
    public class AuthUserQuery: IRequest<AuthUserPayload>
    {
    
    }

    public class GetMeQueryHandler : IRequestHandler<AuthUserQuery, AuthUserPayload>
    {
        ICurrentUserService _currentUserService;

        public GetMeQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task<AuthUserPayload> Handle(AuthUserQuery request, CancellationToken cancellationToken) => Task.FromResult(new AuthUserPayload() { UserId = _currentUserService.UserId ?? "" });
    }
}