using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Repository.Interface;

namespace InventoryLibrary.Source.Repository
{
    public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, PurchaseDetailRepositoryInterface
    {
        public PurchaseDetailRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
