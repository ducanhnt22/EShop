//using EShop.OrderService.Application.Common.Interfaces;
//using EShop.OrderService.Infrastructure.Persistence;
//using EShop.OrderService.Infrastructure.Repository;
//using Microsoft.Extensions.Configuration;

//namespace EShop.OrderService.Infrastructure.UnitOfWork;

//public class UnitOfWork : IUnitOfWork
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IConfiguration _configuration;
//    private readonly HttpClient _httpClient;
//    private IOrderRepository _orderRepository;
//    private IOrderItemRepository _orderItemRepository;
//    private IProductRepository _productRepository;

//    public UnitOfWork(
//        ApplicationDbContext context,
//        IConfiguration configuration,
//        HttpClient httpClient)
//    {
//        _context = context;
//        _configuration = configuration;
//        _httpClient = httpClient;
//    }

//    public IOrderRepository OrderRepository
//    {
//        get
//        {
//            _orderRepository ??= new OrderRepository(_context);
//            return _orderRepository;
//        }
//    }

//    public IOrderItemRepository OrderItemRepository
//    {
//        get
//        {
//            _orderItemRepository ??= new OrderItemRepository(_context);
//            return _orderItemRepository;
//        }
//    }

//    public IProductRepository ProductRepository
//    {
//        get
//        {
//            _productRepository ??= new ProductRepository(_httpClient, _configuration);
//            return _productRepository;
//        }
//    }

//    public async Task<int> SaveChangesAsync()
//    {
//        return await _context.SaveChangesAsync();
//    }

//    private bool disposed = false;

//    protected virtual void Dispose(bool disposing)
//    {
//        if (!disposed)
//        {
//            if (disposing)
//            {
//                _context.Dispose();
//            }
//        }
//        disposed = true;
//    }

//    public void Dispose()
//    {
//        Dispose(true);
//        GC.SuppressFinalize(this);
//    }
//} 