using EShop.UserService.Domain.Entities;
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
    public class RefreshTokenHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        public Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Validate the refresh token and get the user
            //var user = _unitOfWorks.UserRepository.GetByRefreshToken(request.RefreshToken);
            //if (user == null)
            //{
            //    throw new Exception("Invalid refresh token");
            //}
            //// Generate new tokens
            //var newToken = GenerateToken(user);
            //var newRefreshToken = GenerateRefreshToken();
            //// Update the user's refresh token in the database
            //user.RefreshToken = newRefreshToken;
            //_unitOfWorks.UserRepository.Update(user);
            //_unitOfWorks.SaveChangeAsync();
            //return Task.FromResult(new RefreshTokenResponse(newToken, newRefreshToken, DateTime.UtcNow.AddHours(1)));
        }
    }
}
