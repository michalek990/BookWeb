using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Presistence;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context) { }
}