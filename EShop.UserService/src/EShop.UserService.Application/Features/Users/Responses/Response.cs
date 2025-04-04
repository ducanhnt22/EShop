using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Users.Responses;
public sealed record RegisterUserResponse(Guid Id, string Message, string FullName, string PhoneNumber);