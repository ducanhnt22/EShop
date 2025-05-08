using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Authentication;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Auths.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<RefreshTokenResponse>;
    public record RefreshTokenResponse(string Token, string RefreshToken, DateTime Expiration);
    public class RefreshTokenHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWorks.UserRepository.GetByRefreshToken(request.RefreshToken);
            if (user == null)
            {
                throw new Exception("Invalid refresh token");
            }

            var newToken = _jwtTokenGenerator.GenerateJwtToken(user);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _unitOfWorks.UserRepository.Update(user);
            await _unitOfWorks.SaveChangeAsync();
            return await Task.FromResult(new RefreshTokenResponse(newToken, newRefreshToken, DateTime.UtcNow.AddHours(1)));
        }
    }
}
