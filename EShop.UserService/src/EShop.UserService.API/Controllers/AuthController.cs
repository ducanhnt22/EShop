using EShop.UserService.Application.Features.Auths.Commands.Login;
using EShop.UserService.Application.Features.Auths.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.UserService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : DefaultController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
    {
        var command = new RefreshTokenCommand(token, refreshToken);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
