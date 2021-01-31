using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;

namespace Inventory.Repository.Implementation
{

    public class ItemRepository : BaseRepository<Item>, ItemRepositoryInterface
    {
        public ItemRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
