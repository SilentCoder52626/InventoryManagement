using InventoryLibrary.AppDbContext;
using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using InventoryLibrary.Source.Repository.Interface;

namespace InventoryLibrary.Source.Repository
{
    public class SupplierRepository : BaseRepository<Supplier>, SupplierRepositoryInterface
    {
        public SupplierRepository(Testdbcontext context) : base(context)
        {

        }
    }
}
