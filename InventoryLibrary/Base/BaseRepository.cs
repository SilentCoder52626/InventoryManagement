using InventoryLibrary.AppDbContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryLibrary.Base
{
    public class BaseRepository<T> : BaseRepositoryInterface<T> where T : class
    {
        private readonly Testdbcontext _context;
        public BaseRepository(Testdbcontext context)
        {
            _context = context;
            var _set = _context.Set<T>();
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Query<T>();
        }

        public async Task InsertAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
