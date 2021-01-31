using InventoryLibrary.Dto.Purchase;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.ServiceInterface
{
    public interface PurchaseServiceInterface
    {
        Task Create(PurchaseCreateDTO dto);

    }
}
