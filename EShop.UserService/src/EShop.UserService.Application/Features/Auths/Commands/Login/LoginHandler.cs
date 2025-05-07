using EShop.UserService.Application.Common.Authentication;
using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Authentication;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Auths.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
    public record LoginResponse(string Token, string RefreshToken, DateTime Expiration);
    public class LoginHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        public Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = _userManager.CheckPasswordAsync(user, request.Password).Result;
            if (!result)
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtTokenGenerator.GenerateJwtToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            return Task.FromResult(new LoginResponse(token, refreshToken, DateTime.UtcNow.AddHours(1)));
        }
    }
}
