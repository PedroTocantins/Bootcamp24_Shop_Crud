namespace Bootcamp24.Domain.Interfaces;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
