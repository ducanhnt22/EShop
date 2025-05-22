using EShop.OrderService.Domain.Entities;
using EShop.OrderService.Infrastructure.Persistence;
using EShop.OrderService.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.OrderService.Infrastructure.Repository.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => !o.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserId(Guid userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.UserId == userId && !o.IsDeleted)
            .ToListAsync();
    }

    public async Task Add(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public void Update(Order order)
    {
        order.UpdatedAt = DateTime.UtcNow;
        _context.Orders.Update(order);
    }

    public void Delete(Order order)
    {
        order.IsDeleted = true;
        order.UpdatedAt = DateTime.UtcNow;
        _context.Orders.Update(order);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.Orders.AnyAsync(o => o.Id == id && !o.IsDeleted);
    }
} 