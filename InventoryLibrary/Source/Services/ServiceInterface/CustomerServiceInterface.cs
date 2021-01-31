using InventoryLibrary.Source.DTO.Customer;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.ServiceInterface
{
    public interface CustomerServiceInterface
    {
        Task Create(CustomerCreateDTO dto);
        Task Update(CustomerUpdateDTO dto);
        Task Delete(long id);

    }
}
