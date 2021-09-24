using InventoryLibrary.Source.Dto.CustomerTransaction;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.ServiceInterface
{
    public interface CustomerTransactionServiceInterface
    {
        Task Create(CustomerTransactionCreateDto dto);
    }
}
