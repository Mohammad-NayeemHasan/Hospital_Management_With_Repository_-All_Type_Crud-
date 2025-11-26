using Microsoft.EntityFrameworkCore.Query;

namespace CrudOperation.Repository
{
    public interface IRepository<T> where T : class
    {
        // Get all records with optional Include
        Task<List<T>> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);

        // Get by Id
        Task<T?> GetById(long id);

        // Add
        Task AddAsync(T entity);

        // Update
        Task UpdateAsync(T entity);

        // Delete
        Task DeleteAsync(long id);
    }
}
