using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Authenticate.Register.Command
{
    public class RegisterUserCommand: IRequest<TokenResult>
    {
        public string Login { get ;set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public string ConfirmPassword { get ;set; } = string.Empty;
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResult>
    {
        IJWTService _iJWTService;
        IApplicationDbContext _context;
        IHashPassword _hashPassword;

        public RegisterUserCommandHandler(IJWTService iJWTService, IApplicationDbContext context, IHashPassword hashPassword)
        {
            _iJWTService = iJWTService;
            _context = context;
            _hashPassword = hashPassword;
        }

        public async Task<TokenResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(await _context.Users.AnyAsync(x=>x.Login==request.Login))
            {
                throw new BadRequestException("This login exists");
            }
            User user = new(){Login =  request.Login, Password= _hashPassword.Hash(request.Password)};
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new(){AccessToken=_iJWTService.GenerateJWT(user)};
        }
    }
}