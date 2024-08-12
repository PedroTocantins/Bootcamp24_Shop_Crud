using Bootcamp24.Domain.Entities;
using Bootcamp24.Domain.Interfaces;
using Bootcamp24.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp24.Infrastructure.Data.Repositories;
public class CategoryRepository : ICategoryRepository
{
    public readonly ApplicationDataContext _dataContext;
    public IUnitOfWork UnitOfWork => _dataContext;

    public CategoryRepository(ApplicationDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Category> AddAsync(Category entity)
    {
        await _dataContext.Category.AddAsync(entity);
        return entity;
    }

    public void DeleteAsync(Category entity)
    {
        _dataContext.Category.Remove(entity);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dataContext.Category.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _dataContext.Category.FindAsync(id);
    }

    public void UpdateAsync(Category entity)
    {
        _dataContext.Category.Update(entity);
    }
}
