using EShop.UserService.Application.Common.Exceptions;
using EShop.UserService.Application.Features.Users.Responses;
using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Users.Commands.Update
{
    public sealed record UpdateUserCommand(string? PhoneNumber, string? FullName, string? Email, string? Address) : IRequest<UpdateUserResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
    public class UpdateUserHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager) : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        private readonly UserManager<User> _userManager = userManager;
        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWorks.UserRepository.GetById(request.Id);
            if (user is null)
            {   
                throw new AppExceptions("User not found");
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var checkPhoneExist = await _unitOfWorks.UserRepository.GetByPhone(request.PhoneNumber);
                if (checkPhoneExist is not null)
                {
                    throw new AppExceptions("Phone number already exists");
                }
                user.PhoneNumber = request.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(request.FullName)) user.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Email))
            {
                var checkEmailExist = await _userManager.FindByEmailAsync(request.Email);
                if (checkEmailExist is not null && checkEmailExist.Id != request.Id)
                {
                    throw new AppExceptions("Email already exists");
                }
                user.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Address)) user.Address = request.Address;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new UpdateUserResponse(user.Id, "User updated successfully", user.FullName, user.PhoneNumber);
            }
            else
            {
                throw new AppExceptions("User update failed");
            }
        }
    }
}
