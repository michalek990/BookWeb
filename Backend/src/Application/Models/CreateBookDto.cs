using AutoMapper;

namespace Application.Models;

public sealed class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
}

public sealed class CreateBookDtoProfile : Profile
{
    public CreateBookDtoProfile()
    {
        CreateMap<Domain.Entities.Book, CreateBookDto>();
    }
}