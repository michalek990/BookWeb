using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Presistence;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IReviewRepository Reviews { get; }
    public IBookRepository Books { get; }
    public UnitOfWork(AppDbContext context,
        IUserRepository user,
        IReviewRepository review,
        IBookRepository book)
    {
        _context = context;
        Users = user;
        Reviews = review;
        Books = book;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}