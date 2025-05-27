using EShop.ProductService.Application.Features.Categories.Commands.Create;
using EShop.ProductService.Application.Features.Categories.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ProductService.API.Controllers;

public class CategoryController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoryQuery command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
