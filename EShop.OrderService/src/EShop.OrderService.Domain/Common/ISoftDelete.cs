namespace EShop.OrderService.Domain.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
