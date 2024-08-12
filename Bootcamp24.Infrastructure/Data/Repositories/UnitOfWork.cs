using Bootcamp24.Domain.Interfaces;
using Bootcamp24.Infrastructure.Data.Context;

namespace Bootcamp24.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDataContext _dataContext;

    public UnitOfWork(ApplicationDataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public void Dispose() => _dataContext.Dispose();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dataContext.SaveChangesAsync(cancellationToken);
    }
}
