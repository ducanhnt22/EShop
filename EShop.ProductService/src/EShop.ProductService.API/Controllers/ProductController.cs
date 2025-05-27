using EShop.ProductService.Application.Features.Products.Commands.Create;
using EShop.ProductService.Application.Features.Products.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ProductService.API.Controllers
{
    public class ProductController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
