using EShop.VendorService.Domain.Common;

namespace EShop.VendorService.Domain.Entities;

public class Store : BaseAuditableEntity, ISoftDelete
{
    //public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public ICollection<StoreAddress> Addresses { get; set; } = new List<StoreAddress>();
}
