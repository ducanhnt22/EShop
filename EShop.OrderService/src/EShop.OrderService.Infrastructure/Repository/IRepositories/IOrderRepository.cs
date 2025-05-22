using EShop.OrderService.Domain.Entities;

namespace EShop.OrderService.Infrastructure.Repository.IRepositories;

public interface IOrderRepository
{
    Task<Order> GetById(Guid id);
    Task<IEnumerable<Order>> GetAll();
    Task<IEnumerable<Order>> GetByUserId(Guid userId);
    Task Add(Order order);
    void Update(Order order);
    void Delete(Order order);
    Task<bool> Exists(Guid id);
} 