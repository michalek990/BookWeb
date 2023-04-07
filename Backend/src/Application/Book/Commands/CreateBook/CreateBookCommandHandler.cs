using Application.Common.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookDto>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _bookRepository = unitOfWork.Books;
    }
    
    public async Task<CreateBookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        if (await _bookRepository.ExistByTitle(request.Title))
        {
            throw new ConflictException("Book with this title already exist");
        }

        var book = new Domain.Entities.Book
        {
            Title = request.Title,
            Author = request.Author,
            Description = request.Description,
            Cover = request.Cover
        };
        
        _bookRepository.Add(book);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CreateBookDto>(book);
    }
}