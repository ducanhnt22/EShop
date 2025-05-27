namespace EShop.VendorService.Domain.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
