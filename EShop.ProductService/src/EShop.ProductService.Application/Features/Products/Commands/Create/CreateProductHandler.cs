using EShop.ProductService.Application.Common.Behaviours;
using EShop.ProductService.Application.Common.Exceptions;
using EShop.ProductService.Application.Features.Products.Responses;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Domain.Entities;
using MediatR;

namespace EShop.ProductService.Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IUnitOfWorks _unitOfWorks;

    public CreateProductHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWorks.CategoryRepository.GetById(request.CategoryId);
        if (category is null)
        {
            throw new AppExceptions("Category not found");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _unitOfWorks.ProductRepository.Add(product);
        await _unitOfWorks.SaveChangesAsync();

        return new ProductResponse(
            product.Id,
            "Product created successfully",
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.ImageUrl,
            product.CategoryId);
    }
} 