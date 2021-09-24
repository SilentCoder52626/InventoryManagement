using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Repository
{
    public class CustomerTransactionRepository : BaseRepository<CustomerTransaction>, CustomerTansactionRepositoryInterface
    {
        public CustomerTransactionRepository(Testdbcontext context) : base(context)
        {
        }

        public async Task<List<CustomerTransaction>> GetAllTransactionOfCustomer(long customerId)
        {
            return await this.GetQueryable().Where(a => a.CustomerId == customerId).ToListAsync().ConfigureAwait(false);
        }
    }
}
