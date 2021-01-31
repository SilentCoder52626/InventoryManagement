using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;

namespace InventoryLibrary.Source.Repository
{
    public class PurchaseRepository : BaseRepository<Purchase>, PurchaseRepositoryInterface
    {
        public PurchaseRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
