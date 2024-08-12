using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp24.Domain.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    IUnitOfWork UnitOfWork { get; }
}
