using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryLibrary.Base
{
    public interface BaseRepositoryInterface<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
        Task<T> GetById(long id);
    }
}
