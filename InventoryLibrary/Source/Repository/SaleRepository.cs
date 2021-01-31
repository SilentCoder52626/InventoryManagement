using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Source.Repository.Interface;

namespace InventoryLibrary.Source.Repository
{
    public class SaleRepository : BaseRepository<Sale>, SaleRepositoryInterface
    {
        public SaleRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
