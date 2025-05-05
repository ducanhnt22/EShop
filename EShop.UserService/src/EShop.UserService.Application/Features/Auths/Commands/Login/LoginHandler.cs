using EShop.UserService.Domain.Entities;
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
    public class LoginHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager) : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
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
            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            return Task.FromResult(new LoginResponse(token, refreshToken, DateTime.UtcNow.AddHours(1)));
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GenGLenNgoiVoDichChungKetTheGioi"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "GenGLenNgoiVoDich",
                audience: "GenGLenNgoiVoDich",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
