using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Source.Repository.Interface;

namespace InventoryLibrary.Source.Repository
{
    public class SaleDetailRepository : BaseRepository<SaleDetails>, SaleDetailRepositoryInterface
    {
        public SaleDetailRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
