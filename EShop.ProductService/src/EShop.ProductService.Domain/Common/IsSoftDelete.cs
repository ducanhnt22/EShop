namespace EShop.ProductService.Domain.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}
