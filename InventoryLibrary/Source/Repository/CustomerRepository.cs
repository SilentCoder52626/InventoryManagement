using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;

namespace Inventory.Repository.Implementation
{
    public class CustomerRepository : BaseRepository<Customer>, CustomerRepositoryInterface
    {
        public CustomerRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
