using InventoryLibrary.Dto.Sale;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.ServiceInterface
{
    public interface SaleServiceInterface
    {
        Task Create(SaleCreateDTO dto);
        Task Update(SaleUpdateDTO dto);
        Task Delete(long id);
    }
}
