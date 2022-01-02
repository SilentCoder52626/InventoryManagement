using InventoryLibrary.Dto.Supplier;
using InventoryLibrary.Source.Dto.Supplier;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.ServiceInterface
{
    public interface SupplierServiceInterface
    {
        Task Create(SupplierCreateDTO dto);

        Task Update(SupplierUpdateDTO dto);

        Task Activate(long id);

        Task Deactivate(long id);

    }
}
