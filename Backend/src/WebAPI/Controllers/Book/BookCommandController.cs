using Application.Books.Commands.CreateBook;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common.Authorization;

namespace WebAPI.Controllers.Book;

[ApiController]
[Route("api/v1/books")]
public sealed class BookCommandController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

   [AuthorizeUser]
    [HttpPost]
    public async Task<ActionResult<CreateBookDto>> CreateNewBook([FromBody] CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"api/v1/books/{result.Title}", result);
    }
}