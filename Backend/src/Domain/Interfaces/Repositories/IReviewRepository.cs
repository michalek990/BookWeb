using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<Review?> GetByBookIdAndUsername(long bookId, string username);
    Task RemoveAllByBook(long bookId);
}