using EShop.ProductService.Application.Common.Filters;
using EShop.ProductService.Application.Common.Paginations;
using EShop.ProductService.Application.Features.Products.Responses;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Domain.Paginations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Application.Features.Products.Queries.GetAll;

public sealed record GetAllProductQuery(PagingRequest? PagingRequest, Guid? Id, string? ProductName, Guid? CategoryId) : IRequest<PagedResult<GetProductResponse>>;
public class GetAllProductQueryHandler(IUnitOfWorks unitOfWorks) : IRequestHandler<GetAllProductQuery, PagedResult<GetProductResponse>>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    public async Task<PagedResult<GetProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWorks.ProductRepository.GetAllAsync(
            predicate: _ => true,
            include: x => x.Include(x => x.Category)
        );

        var filterEntity = new Product
        {
            Id = request.Id.HasValue ? request.Id.Value : Guid.Empty,
            Name = string.IsNullOrEmpty(request.ProductName) ? string.Empty : request.ProductName,
            CategoryId = request.CategoryId.HasValue ? request.CategoryId.Value : Guid.Empty
        };
        var filterProduct = products.AsQueryable().FilterEntity(filterEntity);
        var mappedProduct = filterProduct.Select(p => new GetProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            ImageUrl = p.ImageUrl,
            CategoryName = p.Category.Name,
            CreatedDate = p.CreatedAt
        });

        var (page, pageSize, sortType, sortField) = PaginationUtils.GetPaginationAndSortingValues(request.PagingRequest);
        var sortedResult = PaginationHelper<GetProductResponse>.Sorting(sortType, mappedProduct, sortField);
        var result = PaginationHelper<GetProductResponse>.Paging(sortedResult, page, pageSize);
        return result;
    }
}
