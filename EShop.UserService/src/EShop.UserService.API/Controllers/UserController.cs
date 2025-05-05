using EShop.UserService.Application.Features.Users.Commands.Create;
using EShop.UserService.Application.Features.Users.Commands.Update;
using EShop.UserService.Application.Features.Users.Queries.GetAll;
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
    [HttpPatch("{Id:guid}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] Guid Id, [FromBody] UpdateUserCommand command)
    {
        command.Id = Id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCustomer()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
}
