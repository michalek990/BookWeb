using Domain.Common;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(long id);
    Task<bool> ExistById(long id);
}