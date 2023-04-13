using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<bool> ExistByTitle(string title);
}