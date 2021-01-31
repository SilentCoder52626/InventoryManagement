using InventoryLibrary.Source.DTO.Item;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.ServiceInterface
{
    public interface ItemServiceInterface
    {
        Task Create(ItemCreateDTO dto);

        Task Update(ItemUpdateDTO dto);

        Task Deactivate(long id);

        Task Activate(long id);

        Task Delete(long id);
    }
}
