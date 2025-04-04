using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Constant
{
    static class CurrentTime
    {
        public static readonly DateTime RecentTime = DateTime.UtcNow.AddHours(7);
    }
}
