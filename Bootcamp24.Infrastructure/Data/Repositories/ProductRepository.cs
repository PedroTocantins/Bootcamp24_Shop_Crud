using Bootcamp24.Domain.Entities;
using Bootcamp24.Domain.Interfaces;
using Bootcamp24.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp24.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public readonly ApplicationDataContext _dataContext;

    public IUnitOfWork UnitOfWork => _dataContext;

    public ProductRepository(ApplicationDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dataContext.Product.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _dataContext.Product.FindAsync(id);
    }
    public async Task<Product> AddAsync(Product product)
    {
        await _dataContext.AddAsync(product);
        return product;
    }
    public void UpdateAsync(Product product)
    {
        _dataContext.Product.Update(product);
    }

    public void DeleteAsync(Product product)
    {
        _dataContext.Product.Remove(product);
    }


}
