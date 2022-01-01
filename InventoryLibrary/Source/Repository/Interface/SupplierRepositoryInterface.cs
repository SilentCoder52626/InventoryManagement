using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Repository.Interface
{
    public interface SupplierRepositoryInterface : BaseRepositoryInterface<Supplier>
    {
        Task<Supplier> GetByNumber(string number);
    }
}
