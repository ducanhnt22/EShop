using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; } = DateTime.UtcNow;

        public string? LastModifiedBy { get; set; }
    }
}
