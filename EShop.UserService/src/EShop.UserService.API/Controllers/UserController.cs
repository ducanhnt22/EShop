using EShop.UserService.Application.Features.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.UserService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : DefaultController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> AddNewCustomer([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
