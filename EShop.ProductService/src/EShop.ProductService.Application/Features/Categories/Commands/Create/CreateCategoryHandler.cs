using EShop.ProductService.Application.Common.Behaviours;
using EShop.ProductService.Application.Interfaces.UnitOfWorks;
using EShop.ProductService.Domain.Entities;
using MediatR;

namespace EShop.ProductService.Application.Features.Categories.Commands.Create;

public class CreateCategoryHandler(IUnitOfWorks unitOfWorks) : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _unitOfWorks.CategoryRepository.FirstOrDefaultAsync(x => x.Name == request.Name);
        if (existingCategory != null)
        {
            throw new AppExceptions("Category already exists");
        }
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };
        await _unitOfWorks.CategoryRepository.AddAsync(category);
        await _unitOfWorks.SaveChangesAsync();
        return category.Id;
    }
}
