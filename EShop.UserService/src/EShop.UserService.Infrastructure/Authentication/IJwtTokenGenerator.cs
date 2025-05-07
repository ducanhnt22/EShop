using EShop.UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
