using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IReviewRepository Reviews { get; }
    IBookRepository Books { get; }
    Task SaveAsync();
}