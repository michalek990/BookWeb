using Application.Models;
using Application.Users.Commands.AuthenticateUser;
using Application.Users.Commands.DeactivateUser;
using Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Account;

[ApiController]
[Route("api/v1/users")]
public sealed class AccountCommandController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AccountCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDetailsDto>> RegisterAsync([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<JwtTokenDto>> LoginAsync([FromBody] AuthenticateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{username}")]
    public async Task<ActionResult> DeactivateUser([FromRoute] string username)
    {
        await _mediator.Send(new DeactivateUserCommand(username));
        return NoContent();
    }
}