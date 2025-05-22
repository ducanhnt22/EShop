using EShop.OrderService.Domain.Entities;
using EShop.OrderService.Infrastructure.Persistence;
using EShop.OrderService.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.OrderService.Infrastructure.Repository.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _context;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> GetById(Guid id)
    {
        return await _context.OrderItems
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderId(Guid orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();
    }

    public async Task Add(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
    }

    public void Update(OrderItem orderItem)
    {
        orderItem.UpdatedAt = DateTime.UtcNow;
        _context.OrderItems.Update(orderItem);
    }

    public void Delete(OrderItem orderItem)
    {
        _context.OrderItems.Remove(orderItem);
    }
} 