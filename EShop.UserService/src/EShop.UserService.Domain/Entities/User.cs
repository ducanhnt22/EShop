using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public override string UserName
        {
            get => base.UserName!;
            set
            {
                base.UserName = value;
                base.NormalizedUserName = value?.ToUpper();
            }
        }

        public override string Email
        {
            get => base.Email!;
            set
            {
                base.Email = value;
                base.NormalizedEmail = value?.ToUpper();
            }
        }

        public User()
        {
            SecurityStamp = Guid.NewGuid().ToString();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
    }
}
