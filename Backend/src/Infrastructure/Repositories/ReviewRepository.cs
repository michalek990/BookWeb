using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context) { }
   
    public async Task<Review?> GetByBookIdAndUsername(long bookId, string username)
    {
        return await Context.Reviews
            .Include(r => r.User)
            .Where(r => r.BookId == bookId && r.User!.Username == username)
            .FirstOrDefaultAsync();
    }

    public async Task RemoveAllByBook(long bookId)
    {
        var toDelete = await Context.Reviews
            .Where(r => r.BookId == bookId)
            .ToListAsync();
        Context.Reviews.RemoveRange(toDelete);
    }
    
}