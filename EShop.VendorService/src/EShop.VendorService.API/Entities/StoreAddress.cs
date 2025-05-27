using EShop.VendorService.Domain.Common;

namespace EShop.VendorService.Domain.Entities;

public class StoreAddress : BaseAuditableEntity, ISoftDelete
{
    public required string StreetAddress { get; set; }
    public string WardCode { get; set; } = string.Empty;
    public int DistrictId { get; set; }
    public string WardName { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    public string ProvinceName { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public Store Store { get; set; }
    public bool IsDeleted { get; set; } = false;
}
