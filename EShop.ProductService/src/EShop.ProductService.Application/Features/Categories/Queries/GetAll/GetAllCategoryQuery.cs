using EShop.ProductService.Application.Common.Filters;
using EShop.ProductService.Application.Common.Paginations;
using EShop.ProductService.Application.Features.Categories.Responses;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Domain.Entities;
using EShop.ProductService.Domain.Paginations;
using MediatR;

namespace EShop.ProductService.Application.Features.Categories.Queries.GetAll;

public sealed record GetAllCategoryQuery(PagingRequest? PagingRequest, Guid? Id, string? CategoryName, string? Description, bool? IsDelete) : IRequest<PagedResult<CategoryResponse>>;
public class GetAllCategoryQueryHandler(IUnitOfWorks unitOfWorks) : IRequestHandler<GetAllCategoryQuery, PagedResult<CategoryResponse>>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    public async Task<PagedResult<CategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWorks.CategoryRepository.GetAllAsync();

        var filterEntity = new Category
        {
            Id = request.Id.HasValue ? request.Id.Value : Guid.Empty,
            Name = string.IsNullOrEmpty(request.CategoryName) ? string.Empty : request.CategoryName,
            Description = string.IsNullOrEmpty(request.Description) ? string.Empty : request.Description,
            IsDeleted = request.IsDelete.HasValue ? request.IsDelete.Value : false
        };

        var filterCategory = categories.AsQueryable().FilterEntity(filterEntity);
        var mappedCategories = filterCategory.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedDate = c.CreatedAt
        });

        var (page, pageSize, sortType, sortField) = PaginationUtils.GetPaginationAndSortingValues(request.PagingRequest);
        var sortedResult = PaginationHelper<CategoryResponse>.Sorting(sortType, mappedCategories, sortField);
        var result = PaginationHelper<CategoryResponse>.Paging(sortedResult, page, pageSize);

        return result;
    }
}