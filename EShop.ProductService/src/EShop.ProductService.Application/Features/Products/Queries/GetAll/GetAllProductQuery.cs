using EShop.ProductService.Application.Common.Filters;
using EShop.ProductService.Application.Common.Paginations;
using EShop.ProductService.Application.Features.Products.Responses;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Infrastructure.Cachings.ICachingService;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Domain.Paginations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Application.Features.Products.Queries.GetAll;

public sealed record GetAllProductQuery(PagingRequest? PagingRequest, Guid? Id, string? ProductName, Guid? CategoryId) : IRequest<PagedResult<GetProductResponse>>;

public class GetAllProductQueryHandler(IUnitOfWorks unitOfWorks, ICacheService cacheService) : IRequestHandler<GetAllProductQuery, PagedResult<GetProductResponse>>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    private readonly ICacheService _cacheService = cacheService;
    
    public async Task<PagedResult<GetProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var (page, pageSize, sortType, sortField) = PaginationUtils.GetPaginationAndSortingValues(request.PagingRequest);
        
        // Create cache key based on query parameters
        var cacheKey = $"products_page_{page}_size_{pageSize}_sort_{sortType}_{sortField}_id_{request.Id}_name_{request.ProductName}_category_{request.CategoryId}";
        
        // Try to get cached result
        var cachedResult = await _cacheService.GetCacheAsync<PagedResult<GetProductResponse>>(cacheKey);
        if (cachedResult != null)
        {
            return cachedResult;
        }
        
        // Build query with database-level filtering
        var query = await _unitOfWorks.ProductRepository.GetAllAsync(
            predicate: p => 
                (!request.Id.HasValue || p.Id == request.Id.Value) &&
                (string.IsNullOrEmpty(request.ProductName) || p.Name.Contains(request.ProductName)) &&
                (!request.CategoryId.HasValue || p.CategoryId == request.CategoryId.Value),
            include: q => q.Include(p => p.Category).AsNoTracking()
        );
        
        // Apply sorting at database level
        IQueryable<Product> sortedQuery = sortField?.ToLower() switch
        {
            "name" => sortType == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "price" => sortType == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            "stockquantity" => sortType == "asc" ? query.OrderBy(p => p.StockQuantity) : query.OrderByDescending(p => p.StockQuantity),
            "createdat" => sortType == "asc" ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };
        
        // Get total count for pagination
        var totalCount = await sortedQuery.CountAsync(cancellationToken);
        
        // Apply pagination at database level
        var products = await sortedQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new GetProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name,
                CreatedDate = p.CreatedAt
            })
            .ToListAsync(cancellationToken);
        
        // Create paginated result
        var result = new PagedResult<GetProductResponse>
        {
            Items = products,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
        
        // Cache result for 5 minutes
        await _cacheService.SetCacheAsync(cacheKey, result, TimeSpan.FromMinutes(5));
        
        return result;
    }
}
