using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context) { }
    public async Task<bool> ExistByTitle(string title)
    {
        return await Context.Books
            .Where(b => b.Title == title)
            .AnyAsync();
    }
}