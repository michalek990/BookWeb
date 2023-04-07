using Application.Models;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public sealed record CreateBookCommand(string Title,
         string Author, 
         string Description, 
         string Cover) : IRequest<CreateBookDto>;