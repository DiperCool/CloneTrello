using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.GettingBoardId;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Behaviours
{

    public class UserIsMemberBoardBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IUserIsMemberBoard
    {
        IGettingBoardId _gettingBoardId;
        IApplicationDbContext _context;
        ICurrentUserService _currentUser;

        public UserIsMemberBoardBehaviour(IGettingBoardId gettingBoardId, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _gettingBoardId = gettingBoardId;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var attr = request.GetType().GetCustomAttributes<UserIsMemberBoardAttribute>().FirstOrDefault();
            if(attr==null)
            {
                throw new Exception("UserIsMemberBoardAttribute not used");
            }
            if(_currentUser.UserIdGuid==Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }
            var type = attr.Type;
            Guid boardId= await _gettingBoardId.GetBoardId(type, request.Id);
            Member member = await _context.Members.AsNoTracking().FirstOrDefaultAsync(x=>x.BoardId==boardId && x.UserId==_currentUser.UserIdGuid);
            if(member==null)
            {
                throw new ForbiddenAccessException("You're not a member to do this action");
            }
            if(attr.NoAllowedGuest && member.MemberType==MemberType.Guest)
            {
                throw new ForbiddenAccessException("Guests can't do this action");
            }
            else if(attr.NoAllowedAdmin&&member.MemberType==MemberType.Admin)
            {
                throw new ForbiddenAccessException("Admins can't do this action");
            }
            else if(attr.NoAllowedOwner&&member.MemberType==MemberType.Owner)
            {
                throw new ForbiddenAccessException("Owner can't do this action");
            }
            return await next();

        }
    }
}