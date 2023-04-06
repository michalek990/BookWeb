namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync();
}