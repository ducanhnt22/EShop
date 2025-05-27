using EShop.ProductService.Application.Features.Categories.Responses;
using MediatR;

namespace EShop.ProductService.Application.Features.Categories.Commands.Create;

public sealed record CreateCategoryCommand(
    string Name,
    string Description
) : IRequest<Guid>;
