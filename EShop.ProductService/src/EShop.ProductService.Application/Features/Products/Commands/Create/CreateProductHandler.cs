using EShop.ProductService.Application.Common.Behaviours;
using EShop.ProductService.Application.Common.Exceptions;
using EShop.ProductService.Application.Features.Products.Responses;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Domain.Entities;
using MediatR;

namespace EShop.ProductService.Application.Features.Products.Commands.Create;

public class CreateProductHandler(IUnitOfWorks unitOfWorks) : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWorks.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
        {
            throw new AppExceptions("Category not found");
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId,
            IsDeleted = false
        };

        await _unitOfWorks.ProductRepository.AddAsync(product);
        await _unitOfWorks.SaveChangesAsync();

        return product.Id;
    }
} 