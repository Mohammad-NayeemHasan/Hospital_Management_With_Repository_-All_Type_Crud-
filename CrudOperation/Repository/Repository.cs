using CrudOperation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CrudOperation.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        // GetAll with optional Include
        public async Task<List<T>> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = includes(query);

            return await query.ToListAsync();
        }

        public async Task<T?> GetById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            if (data != null)
            {
                _context.Set<T>().Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
