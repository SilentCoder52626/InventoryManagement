using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Source.Entity;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.ServiceInterface
{
    public interface SaleServiceInterface
    {
        Task<Sale> Create(SaleCreateDTO dto);
        Task Update(SaleUpdateDTO dto);
        Task Delete(long id);
    }
}
