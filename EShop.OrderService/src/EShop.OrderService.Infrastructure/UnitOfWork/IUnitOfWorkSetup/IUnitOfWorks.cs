using EShop.OrderService.Infrastructure.Repository.IRepositories;

namespace EShop.OrderService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;

public interface IUnitOfWorks : IDisposable
{
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> SaveChangesAsync();
} 