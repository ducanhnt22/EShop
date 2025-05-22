using EShop.UserService.Application.Common.Exceptions;
using EShop.UserService.Application.Features.Users.Responses;
using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Emails;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Users.Commands.Create;
public sealed record CreateUserCommand(string PhoneNumber, string Password, string FullName, string? Email, string? Address) : IRequest<RegisterUserResponse>;
public class CreateUserHandler(IUnitOfWorks unitOfWorks, UserManager<User> userManager, IEmailService emailService) : IRequestHandler<CreateUserCommand, RegisterUserResponse>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IEmailService _emailService = emailService;
    public async Task<RegisterUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var checkPhoneExist = await _unitOfWorks.UserRepository.GetByPhone(request.PhoneNumber);
        if (checkPhoneExist is not null)
        {
            throw new AppExceptions("Phone number already exists");
        }
        if (!IsValidPassword(request.Password))
        {
            throw new AppExceptions("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            UserName = request.PhoneNumber,
            Address = request.Address,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded)
            {
                throw new AppExceptions("Failed to assign default role to the user.");
            }

            var subject = "Welcome to EShop";
            var body = $"<h1>Hello {user.FullName},</h1><p>Your Otp is: </p>";
            await _emailService.SendEmailAsync(user.Email, subject, body);

            return new RegisterUserResponse(user.Id, "User created successfully", user.FullName, user.PhoneNumber);
        }
        else
        {
            throw new AppExceptions("User creation failed");
        }
    }
    private bool IsValidPassword(string password)
    {
        var hasUpperCase = password.Any(char.IsUpper);
        var hasLowerCase = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }
}
